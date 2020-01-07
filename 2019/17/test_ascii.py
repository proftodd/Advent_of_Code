import ascii

scaffold_map = '''
    ..#..........
    ..#..........
    #######...###
    #.#...#...#.#
    #############
    ..#...#...#..
    ..#####...^..
'''


def get_test_scaffold():
    x = 0
    y = -1
    scaffold = {}
    for c in scaffold_map:
        if c == '\n':
            x = 0
            y = y + 1
        elif c == ' ':
            pass
        else:
            scaffold[(x, y)] = c
            x = x + 1
    return scaffold


def test_get_intersections():
    scaffold = get_test_scaffold()
    i = ascii.get_intersections(scaffold)
    assert (2, 2) in i


def test_get_alignment_parameters():
    scaffold = get_test_scaffold()
    i = ascii.get_intersections(scaffold)
    ap = ascii.get_alignment_parameters(i)
    ap.sort()
    assert ap == [4, 8, 24, 40]
