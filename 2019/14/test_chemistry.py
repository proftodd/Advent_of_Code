from chemistry import Reaction


def test_combine_terms():
    r1 = {'A': 1, 'B': 2}
    r2 = {'B': 1, 'C': 4}
    r3 = Reaction.combine_terms(r1, r2)
    assert r3['A'] == 1
    assert r3['B'] == 3
    assert r3['C'] == 4


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
    newer_r = r.add_term('C', 5)
    assert newer_r.rcts['C'] == 5
