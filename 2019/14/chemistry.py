class Reaction:

    def __init__(self, rcts, prd):
        self.rcts = rcts
        self.prd = prd

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
