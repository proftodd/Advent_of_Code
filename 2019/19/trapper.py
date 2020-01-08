import itertools
import sys
from queue import Queue
from intcode.intcode import Intcode


def fit_ship(tfield, ship):
    f_max_x = max([c[0] for c in tfield])
    f_max_y = max([c[1] for c in tfield])
    for (i, j) in itertools.product(range(f_max_x - ship), range(f_max_y - ship)):
        if all([tfield.get((i + k, j), ' ') == 1 and
                tfield.get((i, j + k), ' ') == 1 for k in range(ship)]):
            return i, j
    return 0, 0


def sample(program, max_val):
    input_buffer = Queue()
    output_buffer = Queue()
    ss = Intcode(input_device=input_buffer, output_device=output_buffer)
    field = {}
    for (i, j) in itertools.product(range(max_val), range(max_val)):
        input_buffer.put(i)
        input_buffer.put(j)
        ss.load_program(list(program))
        ss.run_program()
        if output_buffer.get() == 1:
            field[(i, j)] = 1
        if (i * 10 + j) % 10_000 == 0:
            print(f"field[({i},{j})] = {field.get((i, j), 0)}")
    return field


if __name__ == '__main__':
    program = Intcode.read_program(sys.argv[1])
    side = int(sys.argv[2])
    max_val = 30 * side
    field = sample(program, max_val)
    # for j in range(max_val):
    #     for i in range(max_val):
    #         print(field.get((i, j), ' '), end='')
    #     print()
    closest = fit_ship(field, side)
    print(f"The closest a ship of size {side} can be to the emitter is {closest}")
