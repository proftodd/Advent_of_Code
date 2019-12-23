import sys
from queue import Empty, Queue
from threading import Thread
from intcode.intcode import Intcode

# TODO: I don't know yet if it will work best for
# TODO: increasing y directions to be up or down
# TODO: Right now it's coded for UP = decreasing y
UP = (0, -1)
RIGHT = (1, 0)
DOWN = (0, 1)
LEFT = (-1, 0)
directions = [UP, RIGHT, DOWN, LEFT]

BLACK = 0
WHITE = 1
colors = [BLACK, WHITE]


class EHPR():

    def __init__(self, input_buffer, output_buffer):
        self.input_buffer = input_buffer
        self.output_buffer = output_buffer
        self.grid = {}
        self.x = 0
        self.y = 0
        self.direction_index = 0
        self.painted_squares = {}

    def act(self):
        self.output_buffer.put(self.scan_camera())
        color = self.input_buffer.get(timeout=5)
        direction = self.input_buffer.get(timeout=5)
        self.paint(color)
        self.turn_and_move(direction)

    def turn_and_move(self, direction):
        if direction:
            self.direction_index = (self.direction_index + 1) % len(directions)
        else:
            self.direction_index = (self.direction_index - 1) % len(directions)
        self.x = self.x + directions[self.direction_index][0]
        self.y = self.y + directions[self.direction_index][1]

    def scan_camera(self):
        return self.grid.get((self.x, self.y), 0)

    def paint(self, color):
        self.grid[(self.x, self.y)] = color
        self.painted_squares[(self.y, self.x)] = self.painted_squares.get((self.y, self.x), 0) + 1

    def run(self):
        try:
            while True:
                self.act()
        except Empty:
            exit(0)


if __name__ == '__main__':
    program = Intcode.read_program(sys.argv[1])
    control = Queue()
    sensors = Queue()
    ehpr = EHPR(input_buffer=control, output_buffer=sensors)
    ss = Intcode(input_device=sensors, output_device=control)
    ss.load_program(program)
    ehpr_thread = Thread(target=ehpr.run, name="EHPR")
    intcode_thread = Thread(target=ss.run_program, name="Intcode")
    intcode_thread.start()
    ehpr_thread.start()
    intcode_thread.join()
    print(f"{len(ehpr.painted_squares)} squares painted")
