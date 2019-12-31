import sys
from queue import Queue
from threading import Thread
from intcode.intcode import Intcode

NORTH = 1
SOUTH = 2
WEST = 3
EAST = 4

if __name__ == '__main__':
    program = Intcode.read_program(sys.argv[1])
    input_buffer = Queue()
    input_buffer.put(WEST)
    input_buffer.put(WEST)
    input_buffer.put(WEST)
    output_buffer = Queue()
    ss = Intcode(input_device=input_buffer, output_device=output_buffer)
    ss.load_program(program)
    t = Thread(target=ss.run_program)
    t.start()
    while output_buffer.full():
        print(output_buffer.get())
