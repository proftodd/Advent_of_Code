import sys
from fractions import Fraction
import chemistry as ch


def is_basic_reactant(rct, rxn_set):
    rxn = rxn_set[rct]
    if len(rxn.rcts) == 1 and 'ORE' in rxn.rcts:
        return True
    else:
        return False


def get_next_reactant(rxn_set, target_rxn):
    exact_candidate = ch.get_next_reactant_basic(rxn_set, target_rxn)
    if exact_candidate is not None:
        return exact_candidate
    else:
        possibles = [r for r in target_rxn.rcts if not is_basic_reactant(r, rxn_set)]
        return possibles[0] if len(possibles) > 0 else None


def simplify_reaction_set(rxn_set):
    the_target = rxn_set['FUEL']
    while not all([is_basic_reactant(r, rxn_set) for r in the_target.rcts]):
        next_rct = get_next_reactant(rxn_set, the_target)
        if next_rct is None:
            break
        the_target = the_target.substitute_fractional(rxn_set[next_rct])
    return the_target


def max_fuel(simplified_rxn, rxn_map, ore):
    ore_requirements = []
    for r, coef in simplified_rxn.rcts.items():
        producing_rxn = rxn_map[r]
        ore_requirements.append(coef * producing_rxn.rcts['ORE'] / producing_rxn.prd[1])
    print(simplified_rxn)
    for o in ore_requirements:
        print(f"{o}")
    ore_per_fuel = sum(ore_requirements)
    print(ore_per_fuel)
    fuel = (ore / ore_per_fuel).__floor__()
    return fuel


if __name__ == '__main__':
    filename = sys.argv[1]
    max_ore = int(sys.argv[1]) if len(sys.argv) > 2 else 1_000_000_000_000
    rxns = ch.read_file(filename)
    target = simplify_reaction_set(rxns)
    print("The simplified reaction is:")
    print(target)
    fuel_that_can_be_produced = max_fuel(target, rxns, max_ore)
    print(f"{fuel_that_can_be_produced:,} fuel can be produced from {max_ore:,} units of ore")
