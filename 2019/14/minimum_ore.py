import sys
import chemistry as ch


def get_next_reactant(rxn_map, target_rxn):
    exact_candidate = ch.get_next_reactant_basic(rxn_map, target_rxn)
    if exact_candidate is not None:
        return exact_candidate
    waste_list = []
    for candidate, coef in list(target_rxn.rcts.items()):
        if candidate == 'ORE':
            continue
        candidate_rxn = rxn_map[candidate]
        prd_coef = candidate_rxn.prd[1]
        if all([target_rxn.byps.get(r, 0) >= candidate_rxn.rcts[r] for r in candidate_rxn.rcts]):
            return candidate
        if coef == 1:
            waste = prd_coef - coef
        else:
            factor = prd_coef // coef + 1
            waste = coef * factor - prd_coef
        waste_list.append((candidate, waste))
    sorted_list = sorted(waste_list, key=lambda t: t[1], reverse=False)
    return sorted_list[0][0]


def simplify_reaction_set(rxn_set):
    the_target = rxn_set['FUEL']
    while len(the_target.rcts) > 1:
        next_rct = get_next_reactant(rxn_set, the_target)
        the_target = the_target.complex_substitute(rxn_set[next_rct])
    return the_target


if __name__ == '__main__':
    filename = sys.argv[1]
    rxns = ch.read_file(filename)
    target = simplify_reaction_set(rxns)
    print("The simplified reaction is:")
    print(target)
