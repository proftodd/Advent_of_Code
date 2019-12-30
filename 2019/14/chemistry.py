import sys


class Reaction:

    def __init__(self, rcts, prd, byps=None):
        if byps is None:
            byps = {}
        self.rcts = rcts
        self.prd = prd
        self.byps = byps

    def __repr__(self):
        rct_array = [f"{self.rcts[r]} {r}" for r in self.rcts]
        rct_string = ', '.join(rct_array)
        byp_array = [f"{self.byps[b]} {b}" for b in self.byps]
        byp_string = ', '.join(byp_array)
        me = f"{rct_string} => {self.prd[1]} {self.prd[0]}"
        if byp_string != '':
            me = me + ', ' + byp_string
        return me

    @staticmethod
    def from_string(line):
        (rct_string, prd_string) = line.split(' => ')
        (prd_coef, prd) = prd_string.split()
        rct_array = rct_string.split(', ')
        rct_tuples = [r.split() for r in rct_array]
        rct_map = {rt[1]: int(rt[0]) for rt in rct_tuples}
        return Reaction(rct_map, (prd, int(prd_coef)))

    @staticmethod
    def combine_terms(rcts_1, rcts_2, byps_1=None):
        new_rcts = {}
        new_byps = {} if byps_1 is None else {b: byps_1[b] for b in byps_1}
        for rct in rcts_1:
            new_rcts[rct] = rcts_1[rct] + rcts_2.get(rct, 0)
        for rct in rcts_2:
            if rct not in new_rcts:
                new_rcts[rct] = rcts_2[rct]
        for r in list(iter(new_rcts)):
            if r in list(iter(new_byps)):
                if new_rcts[r] > new_byps[r]:
                    new_rcts[r] = new_rcts[r] - new_byps[r]
                    del(new_byps[r])
                elif new_rcts[r] < new_byps[r]:
                    new_byps[r] = new_byps[r] - new_rcts[r]
                    del(new_rcts[r])
                else:
                    del(new_rcts[r])
                    del(new_byps[r])
        return new_rcts, new_byps

    def substitute(self, other):
        if other.prd[0] not in self.rcts:
            print(f"{other} not a reactant in this reaction")
            return None
        elif self.rcts[other.prd[0]] != other.prd[1]:
            print(f"Coefficients don't match: {self.rcts[other.prod[0]]} != {other.prd[1]}")
            return None
        else:
            this_rcts = {rct: self.rcts[rct] for rct in self.rcts if rct != other.prd[0]}
            new_rcts, new_byps = Reaction.combine_terms(this_rcts, other.rcts, self.byps)
            return Reaction(new_rcts, self.prd, new_byps)

    def add_term(self, rct, coefficient):
        new_rcts = {rct: self.rcts[rct] for rct in self.rcts}
        new_rcts[rct] = new_rcts.get(rct, 0) + coefficient
        new_byps = {byp: self.byps[byp] for byp in self.byps}
        new_byps[rct] = new_byps.get(rct, 0) + coefficient
        return Reaction(new_rcts, self.prd, new_byps)

    def multiply_by(self, factor):
        new_rcts = {rct: factor * self.rcts[rct] for rct in self.rcts}
        new_prd = (self.prd[0], self.prd[1] * factor)
        new_byps = {byp: factor * self.byps[byp] for byp in self.byps}
        return Reaction(new_rcts, new_prd, new_byps)

    def complex_substitute(self, other):
        if self.rcts[other.prd[0]] % other.prd[1] == 0:
            factor = self.rcts[other.prd[0]] // other.prd[1]
            new_other = other.multiply_by(factor)
            return self.substitute(new_other)
        else:
            if self.rcts[other.prd[0]] == 1:
                new_other = other
                difference = other.prd[1] - self.rcts[other.prd[0]]
            else:
                factor = self.rcts[other.prd[0]] // other.prd[1] + 1
                new_other = other.multiply_by(factor)
                difference = new_other.prd[1] - self.rcts[new_other.prd[0]]
            new_me = self.add_term(new_other.prd[0], difference)
            return new_me.substitute(new_other)


def read_file(filename):
    rxns = {}
    fp = open(filename, 'r')
    for line in fp:
        line = line.strip()
        rxn = Reaction.from_string(line)
        rxns[rxn.prd[0]] = rxn
    fp.close()
    return rxns


def get_next_reactant(rxn_map, target_rxn):
    waste_list = []
    for candidate, coef in list(target_rxn.rcts.items()):
        if candidate == 'ORE':
            continue
        candidate_rxn = rxn_map[candidate]
        prd_coef = candidate_rxn.prd[1]
        if prd_coef == 1:
            return candidate
        if coef % prd_coef == 0:
            return candidate
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
    target = rxn_set['FUEL']
    while len(target.rcts) > 1:
        next_rct = get_next_reactant(rxn_set, target)
        target = target.complex_substitute(rxn_set[next_rct])
    return target


if __name__ == '__main__':
    filename = sys.argv[1]
    rxns = read_file(filename)
    target = simplify_reaction_set(rxns)
    print("The simplified reaction is:")
    print(target)
