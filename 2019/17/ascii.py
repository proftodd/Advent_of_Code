import sys
from queue import Queue
from intcode.intcode import Intcode

if __name__ == '__main__':
    program = Intcode.read_program(sys.argv[1])
    output_buffer = Queue()
    ss = Intcode(output_device=output_buffer)
    ss.load_program(program)
    ss.run_program()
    scaffold = {}
    y = 0
    while not output_buffer.empty():
        x = 0
        v = output_buffer.get()
        the_char = str(chr(v))
        print(the_char, end='')
        scaffold[(x, y)] = the_char
        if the_char == '\n':
            x = 0
            y = y + 1
