import itertools
import sys
from queue import Queue
from intcode.intcode import Intcode

if __name__ == '__main__':
    program = Intcode.read_program(sys.argv[1])
    input_buffer = Queue()
    output_buffer = Queue()
    ss = Intcode(input_device=input_buffer, output_device=output_buffer)
    field = {}
    for (i, j) in itertools.product(range(50), range(50)):
        input_buffer.put(i)
        input_buffer.put(j)
        ss.load_program(list(program))
        ss.run_program()
        field[(i, j)] = output_buffer.get()
    for j in range(50):
        for i in range(50):
            print(field.get((i, j), ' '), end='')
        print()
    affected_spaces = len([s for s in field.values() if s == 1])
    print(f"{affected_spaces} spaces are affected by the tractor beam")
