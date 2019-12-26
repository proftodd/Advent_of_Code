import sys
import time
from queue import Queue
from threading import Thread
from intcode.intcode import Intcode


EMPTY = 0
WALL = 1
BLOCK = 2
PADDLE = 3
BALL = 4
SCORE = 5
LEFT = -1
NEUTRAL = 0
RIGHT = 1


def read_display(buffer):
    my_display = {}
    while not buffer.empty():
        x = buffer.get()
        y = buffer.get()
        sprite = buffer.get()
        if x == -1 and y == 0:
            my_display[SCORE] = sprite
        else:
            my_display[sprite] = my_display.get(sprite, []) + [(x, y)]
    return my_display


if __name__ == '__main__':
    program = Intcode.read_program(sys.argv[1])
    program[0] = 2
    input_buffer = Queue()
    input_buffer.put(NEUTRAL)
    output_buffer = Queue()
    ss = Intcode(input_device=input_buffer, output_device=output_buffer)
    ss.load_program(program)
    t = Thread(target=ss.run_program)
    t.start()
    display = {}
    while t.is_alive():
        time.sleep(0.1)
        display = read_display(output_buffer)
        if BALL in display and PADDLE in display:
            ball_position = display[BALL][0][0]
            paddle_position = display[PADDLE][0][0]
            if ball_position < paddle_position:
                input_buffer.put(LEFT)
            elif ball_position > paddle_position:
                input_buffer.put(RIGHT)
            else:
                input_buffer.put(NEUTRAL)
        else:
            input_buffer.put(NEUTRAL)
    t.join()
    print(f"Final score: {display[SCORE]}")
