from fractions import Fraction
import maximum_fuel as mx
from chemistry import Reaction


def test_is_basic_reactant():
    rxns = [
        Reaction({'ORE': 165}, ('DCFZ', 6)),
        Reaction({'DCFZ': 7, 'PSHF': 7}, ('XJWVT', 2))
    ]
    target_1 = Reaction({'DCFZ': 1}, ('ABCD', 1))
    target_2 = Reaction({'DCFZ': 1, 'XJWVT': 1}, ('EFGH', 1))
    rxn_map = {r.prd[0]: r for r in rxns}
    assert     mx.is_basic_reactant(rxns[0].prd[0], rxn_map)
    assert not mx.is_basic_reactant(rxns[1].prd[0], rxn_map)
    assert     all([mx.is_basic_reactant(r, rxn_map) for r in target_1.rcts])
    assert not all([mx.is_basic_reactant(r, rxn_map) for r in target_2.rcts])


def test_example_01():
    lines = [
        '10 ORE => 10 A',
        '1 ORE => 1 B',
        '7 A, 1 B => 1 C',
        '7 A, 1 C => 1 D',
        '7 A, 1 D => 1 E',
        '7 A, 1 E => 1 FUEL'
    ]
    rxns = [Reaction.from_string(line) for line in lines]
    rxn_map = {r.prd[0]: r for r in rxns}
    simplified = mx.simplify_reaction_set(rxn_map)
    assert simplified.rcts['A'] == 28
    assert simplified.rcts['B'] == 1
    assert simplified.prd[1] == 1


def test_example_02():
    lines = [
        '9 ORE => 2 A',
        '8 ORE => 3 B',
        '7 ORE => 5 C',
        '3 A, 4 B => 1 AB',
        '5 B, 7 C => 1 BC',
        '4 C, 1 A => 1 CA',
        '2 AB, 3 BC, 4 CA => 1 FUEL'
    ]
    rxns = [Reaction.from_string(line) for line in lines]
    rxn_map = {r.prd[0]: r for r in rxns}
    simplified = mx.simplify_reaction_set(rxn_map)
    assert simplified.rcts['A'] == 10
    assert simplified.rcts['B'] == 23
    assert simplified.rcts['C'] == 37


def test_example_03():
    lines = [
        '157 ORE => 5 NZVS',
        '165 ORE => 6 DCFZ',
        '44 XJWVT, 5 KHKGT, 1 QDVJ, 29 NZVS, 9 GPVTF, 48 HKGWZ => 1 FUEL',
        '12 HKGWZ, 1 GPVTF, 8 PSHF => 9 QDVJ',
        '179 ORE => 7 PSHF',
        '177 ORE => 5 HKGWZ',
        '7 DCFZ, 7 PSHF => 2 XJWVT',
        '165 ORE => 2 GPVTF',
        '3 DCFZ, 7 NZVS, 5 HKGWZ, 10 PSHF => 8 KHKGT'
    ]
    rxns = [Reaction.from_string(line) for line in lines]
    rxn_map = {r.prd[0]: r for r in rxns}
    simplified = mx.simplify_reaction_set(rxn_map)
    assert simplified.rcts['DCFZ'] == Fraction(1247, 8)
    assert simplified.rcts['GPVTF'] == Fraction(82, 9)
    assert simplified.rcts['HKGWZ'] == Fraction(1259, 24)
    assert simplified.rcts['NZVS'] == Fraction(267, 8)
    assert simplified.rcts['PSHF'] == Fraction(5801, 36)


def test_example_04():
    lines = [
        '2 VPVL, 7 FWMGM, 2 CXFTF, 11 MNCFX => 1 STKFG',
        '17 NVRVD, 3 JNWZP => 8 VPVL',
        '53 STKFG, 6 MNCFX, 46 VJHF, 81 HVMC, 68 CXFTF, 25 GNMV => 1 FUEL',
        '22 VJHF, 37 MNCFX => 5 FWMGM',
        '139 ORE => 4 NVRVD',
        '144 ORE => 7 JNWZP',
        '5 MNCFX, 7 RFSQX, 2 FWMGM, 2 VPVL, 19 CXFTF => 3 HVMC',
        '5 VJHF, 7 MNCFX, 9 VPVL, 37 CXFTF => 6 GNMV',
        '145 ORE => 6 MNCFX',
        '1 NVRVD => 8 CXFTF',
        '1 VJHF, 6 MNCFX => 4 RFSQX',
        '176 ORE => 6 VJHF'
    ]
    rxns = [Reaction.from_string(line) for line in lines]
    rxn_map = {r.prd[0]: r for r in rxns}
    simplified = mx.simplify_reaction_set(rxn_map)
    assert simplified.rcts['JNWZP'] == Fraction(1185, 16)
    assert simplified.rcts['MNCFX'] == Fraction(12545, 3)
    assert simplified.rcts['NVRVD'] == Fraction(3149, 6)
    assert simplified.rcts['VJHF'] == Fraction(23809, 12)


def test_example_05():
    lines = [
        '171 ORE => 8 CNZTR',
        '7 ZLQW, 3 BMBT, 9 XCVML, 26 XMNCP, 1 WPTQ, 2 MZWV, 1 RJRHP => 4 PLWSL',
        '114 ORE => 4 BHXH',
        '14 VRPVC => 6 BMBT',
        '6 BHXH, 18 KTJDG, 12 WPTQ, 7 PLWSL, 31 FHTLT, 37 ZDVW => 1 FUEL',
        '6 WPTQ, 2 BMBT, 8 ZLQW, 18 KTJDG, 1 XMNCP, 6 MZWV, 1 RJRHP => 6 FHTLT',
        '15 XDBXC, 2 LTCX, 1 VRPVC => 6 ZLQW',
        '13 WPTQ, 10 LTCX, 3 RJRHP, 14 XMNCP, 2 MZWV, 1 ZLQW => 1 ZDVW',
        '5 BMBT => 4 WPTQ',
        '189 ORE => 9 KTJDG',
        '1 MZWV, 17 XDBXC, 3 XCVML => 2 XMNCP',
        '12 VRPVC, 27 CNZTR => 2 XDBXC',
        '15 KTJDG, 12 BHXH => 5 XCVML',
        '3 BHXH, 2 VRPVC => 7 MZWV',
        '121 ORE => 7 VRPVC',
        '7 XCVML => 6 RJRHP',
        '5 BHXH, 4 VRPVC => 5 LTCX'
    ]
    rxns = [Reaction.from_string(line) for line in lines]
    rxn_map = {r.prd[0]: r for r in rxns}
    simplified = mx.simplify_reaction_set(rxn_map)
    assert simplified.rcts['BHXH'] == Fraction(753409, 252)
    assert simplified.rcts['CNZTR'] == Fraction(1092987, 16)
    assert simplified.rcts['KTJDG'] == Fraction(75119, 24)
    assert simplified.rcts['VRPVC'] == Fraction(163185319, 5040)
