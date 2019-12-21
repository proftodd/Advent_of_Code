UP = (0, -1)
RIGHT = (1, 0)
DOWN = (0, 1)
LEFT = (-1, 0)
directions = [UP, RIGHT, DOWN, LEFT]

BLACK = 0
WHITE = 1
colors = [BLACK, WHITE]


class EHPR():

    def __init__(self):
        self.grid = {}
        self.x = 0
        self.y = 0
        self.direction_index = 0
        self.painted_squares = {}

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
