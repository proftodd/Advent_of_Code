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

    def __init__(self, input_buffer, output_buffer, grid=None):
        self.input_buffer = input_buffer
        self.output_buffer = output_buffer
        self.grid = grid if grid else {}
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
        self.painted_squares[(self.x, self.y)] = self.painted_squares.get((self.x, self.y), 0) + 1

    def run(self):
        try:
            while True:
                self.act()
        except Empty:
            exit(0)


if __name__ == '__main__':
    program = Intcode.read_program(sys.argv[1])
    grid = {}
    if len(sys.argv) > 2:
        for i in range(2, len(sys.argv), 3):
            grid[int(sys.argv[i]), int(sys.argv[i + 1])] = int(sys.argv[i + 2])
    control = Queue()
    sensors = Queue()
    ehpr = EHPR(input_buffer=control, output_buffer=sensors, grid=grid)
    ss = Intcode(input_device=sensors, output_device=control)
    ss.load_program(program)
    ehpr_thread = Thread(target=ehpr.run, name="EHPR")
    intcode_thread = Thread(target=ss.run_program, name="Intcode")
    intcode_thread.start()
    ehpr_thread.start()
    intcode_thread.join()
    print(f"{len(ehpr.painted_squares)} squares painted")
    minx = 0
    maxx = 0
    miny = 0
    maxy = 0
    for x, y in ehpr.grid:
        if x < minx:
            minx = x
        if x > maxx:
            maxx = x
        if y < miny:
            miny = y
        if y > maxy:
            maxy = y
    for y in range(miny, maxy + 1):
        for x in range(minx, maxx + 1):
            char_to_print = '#' if ehpr.grid.get((x, y), 0) else '.'
            print(char_to_print, end='')
        print()
