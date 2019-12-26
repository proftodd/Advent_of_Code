import sys
from queue import Queue
from intcode.intcode import Intcode


EMPTY = 0
WALL = 1
BLOCK = 2
PADDLE = 3
BALL = 4


if __name__ == '__main__':
    program = Intcode.read_program(sys.argv[1])
    output_buffer = Queue()
    ss = Intcode(output_device=output_buffer)
    ss.load_program(program)
    ss.run_program()
    display = {}
    while not output_buffer.empty():
        x = output_buffer.get()
        y = output_buffer.get()
        sprite = output_buffer.get()
        display[sprite] = display.get(sprite, 0) + 1
    print(f"Number of blocks: {display[BLOCK]}\n")
