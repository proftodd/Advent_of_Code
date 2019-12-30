import sys


class Reaction:

    def __init__(self, rcts, prd):
        self.rcts = rcts
        self.prd = prd

    def __repr__(self):
        rct_array = [f"{self.rcts[r]} {r}" for r in self.rcts]
        rct_string = ', '.join(rct_array)
        return f"{rct_string} => {self.prd[1]} {self.prd[0]}"

    @staticmethod
    def combine_terms(rcts_1, rcts_2):
        new_rcts = {}
        for rct in rcts_1:
            new_rcts[rct] = rcts_1[rct] + rcts_2.get(rct, 0)
        for rct in rcts_2:
            if rct not in new_rcts:
                new_rcts[rct] = rcts_2[rct]
        return new_rcts

    def substitute(self, other):
        if other.prd[0] not in self.rcts:
            print(f"{other} not a reactant in this reaction")
            return None
        elif self.rcts[other.prd[0]] != other.prd[1]:
            print(f"Coefficients don't match: {self.rcts[other.prod[0]]} != {other.prd[1]}")
            return None
        else:
            this_rcts = {rct: self.rcts[rct] for rct in self.rcts if rct != other.prd[0]}
            return Reaction(Reaction.combine_terms(this_rcts, other.rcts), self.prd)

    def add_term(self, rct, coefficient):
        new_rcts = {rct: self.rcts[rct] for rct in self.rcts}
        new_rcts[rct] = self.rcts.get(rct, 0) + coefficient
        return Reaction(new_rcts, self.prd)

    def multiply_by(self, factor):
        new_rcts = {rct: factor * self.rcts[rct] for rct in self.rcts}
        new_prd = (self.prd[0], self.prd[1] * factor)
        return Reaction(new_rcts, new_prd)

    def complex_substitute(self, other):
        if self.rcts[other.prd[0]] % other.prd[1] == 0:
            factor = self.rcts[other.prd[0]] // other.prd[1]
            new_other = other.multiply_by(factor)
            return self.substitute(new_other)
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
        (rct_string, prd_string) = line.split(' => ')
        (prd_coef, prd) = prd_string.split()
        rct_array = rct_string.split(', ')
        rct_tuples = [r.split() for r in rct_array]
        rct_map = {rt[1]: int(rt[0]) for rt in rct_tuples}
        rxns[prd] = Reaction(rct_map, (prd, int(prd_coef)))
    fp.close()
    return rxns


def get_next_reactant(rxn):
    rct_list = list(rxn.rcts.items())
    sorted_list = sorted(rct_list, key=lambda t: t[1], reverse=False)
    if sorted_list[0][0] == 'ORE':
        sorted_list = sorted_list[1:]
    return sorted_list[0][0]


def simplify_reaction_set(rxns):
    target = rxns['FUEL']
    while len(target.rcts) > 1:
        next_rct = get_next_reactant(target)
        target = target.complex_substitute(rxns[next_rct])
    return target


if __name__ == '__main__':
    filename = sys.argv[1]
    rxns = read_file(filename)
    target = simplify_reaction_set(rxns)
    repr(target)
