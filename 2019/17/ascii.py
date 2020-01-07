import sys
from queue import Queue
from intcode.intcode import Intcode


icons = {
    'SCAFFOLD': '#',
    'EMPTY': '.',
    'D_UP': '^',
    'D_DOWN': 'v',
    'D_RIGHT': '>',
    'D_LEFT': '<'
}
position_change = [
    (0, 1), (0, -1), (-1, 0), (1, 0)
]


def map_scaffold(filename):
    program = Intcode.read_program(filename)
    output_buffer = Queue()
    ss = Intcode(output_device=output_buffer)
    ss.load_program(program)
    ss.run_program()
    the_scaffold = {}
    x = 0
    y = 0
    while not output_buffer.empty():
        v = output_buffer.get()
        the_char = str(chr(v))
        print(the_char, end='')
        if the_char == '\n':
            x = 0
            y = y + 1
        else:
            the_scaffold[(x, y)] = the_char
            x = x + 1
    return the_scaffold


def get_intersections(the_scaffold):
    the_intersections = []
    for coord, icon in the_scaffold.items():
        if icon == icons['SCAFFOLD']:
            neighbors = [(coord[0] + p[0], coord[1] + p[1]) for p in position_change]
            if all([the_scaffold.get(n, ' ') == icons['SCAFFOLD'] for n in neighbors]):
                the_intersections.append(coord)
    return the_intersections


def get_alignment_parameters(the_intersections):
    return [i[0] * i[1] for i in the_intersections]


if __name__ == '__main__':
    scaffold = map_scaffold(sys.argv[1])
    intersections = get_intersections(scaffold)
    alignment_parameters = get_alignment_parameters(intersections)
    ap_sum = sum(alignment_parameters)
    print(f"The sum of the alignment parameters is {ap_sum}")
