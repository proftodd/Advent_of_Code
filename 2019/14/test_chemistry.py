from chemistry import Reaction, simplify_reaction_set


def test_combine_terms():
    r1 = {'A': 1, 'B': 2}
    r2 = {'B': 1, 'C': 4}
    r3, _ = Reaction.combine_terms(r1, r2)
    assert r3['A'] == 1
    assert r3['B'] == 3
    assert r3['C'] == 4
    r4, _ = Reaction.combine_terms(r1, r2, {'A': 1})
    assert 'A' not in r4
    r5, _ = Reaction.combine_terms(r1, r2, {'B': 1})
    assert r5['B'] == 2
    r6, b = Reaction.combine_terms(r1, r2, {'C': 5})
    assert 'C' not in r6
    assert b['C'] == 1


def test_substitute():
    r1 = Reaction({'A': 7, 'E': 1}, ('FUEL', 1))
    r2 = Reaction({'A': 7, 'D': 1}, ('E', 1))
    r3 = r1.substitute(r2)
    assert r3.prd == ('FUEL', 1)
    assert r3.rcts == {
        'A': 14,
        'D': 1
    }


def test_add_term():
    r = Reaction({'A': 28, 'B': 1}, ('FUEL', 1))
    new_r = r.add_term('A', 2)
    assert new_r.rcts['A'] == 30
    assert new_r.byps['A'] == 2
    newer_r = r.add_term('C', 5)
    assert newer_r.rcts['C'] == 5
    assert newer_r.byps['C'] == 5


def test_complex_substitute():
    r1 = Reaction({'A': 10, 'B': 24, 'C': 37}, ('FUEL', 1))
    r2 = Reaction({'ORE': 8}, ('B', 3))
    r3 = Reaction({'ORE': 7}, ('C', 5))
    new_r1 = r1.complex_substitute(r2)
    assert new_r1.rcts['ORE'] == 64
    new_r2 = r1.complex_substitute(r3)
    assert new_r2.rcts['ORE'] == 56


def test_substitute_with_byproducts():
    r1 = Reaction({'A': 28, 'B': 1}, ('FUEL', 1))
    r2 = Reaction({'ORE': 10}, ('A', 10))
    r3 = r1.complex_substitute(r2)
    assert r3.rcts['ORE'] == 30
    assert r3.byps['A'] == 2
    r4 = Reaction({'A': 1, 'C': 1}, ('FUEL', 1), {'B': 1})
    r5 = Reaction({'A': 1, 'B': 1}, ('C', 1))
    r6 = r4.complex_substitute(r5)
    assert r6.rcts['A'] == 2
    assert 'B' not in r6.rcts
    assert r6.prd[0] == 'FUEL'


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
    simplified = simplify_reaction_set(rxn_map)
    assert simplified.rcts['ORE'] == 31


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
    simplified = simplify_reaction_set(rxn_map)
    assert simplified.rcts['ORE'] == 165


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
    simplified = simplify_reaction_set(rxn_map)
    assert simplified.rcts['ORE'] == 13312


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
    simplified = simplify_reaction_set(rxn_map)
    assert simplified.rcts['ORE'] == 180697


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
    simplified = simplify_reaction_set(rxn_map)
    assert simplified.rcts['ORE'] == 2_210_736
