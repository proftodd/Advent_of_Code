import sys

class Point:
    def __init__(self, x, y):
        self.x = x
        self.y = y

    def __repr__(self):
        return f"({self.x},{self.y})"
    
    def __eq__(self, other):
        return self.x == other.x and self.y == other.y
    
    @staticmethod
    def manhattan_distance(p1, p2):
        return abs(p1.x - p2.x) + abs(p1.y - p2.y)

class Segment:
    def __init__(self, p1, p2):
        self.p1 = p1
        self.p2 = p2
    
    def __repr__(self):
        return f"[{repr(self.p1)},{repr(self.p2)}]"
    
    def is_horizontal(self):
        return self.p1.y == self.p2.y
    
    def is_vertical(self):
        return self.p1.x == self.p2.x
    
    def contains(self, pt):
        lesser = None
        if self.p1.x < self.p2.x:
            lesser = self.p1
        elif self.p1.x == self.p2.x:
            lesser = self.p1 if self.p1.y < self.p2.y else self.p2
        else:
            lesser = self.p2
        greater = self.p2 if lesser == self.p1 else self.p1
        return lesser.x <= pt.x and pt.x <= greater.x and lesser.y <= pt.y and pt.y <= greater.y

    @staticmethod
    def intersection(s1, s2):
        if (s1.is_horizontal() and s2.is_horizontal()) or (s1.is_vertical() and s2.is_vertical()):
            return None
        the_vertical_one = s1 if s1.is_vertical() else s2
        the_horizontal_one = s1 if s1.is_horizontal() else s2
        leftmost_point = the_horizontal_one.p1 if the_horizontal_one.p1.x < the_horizontal_one.p2.x else the_horizontal_one.p2
        rightmost_point = the_horizontal_one.p1 if the_horizontal_one.p1.x > the_horizontal_one.p2.x else the_horizontal_one.p2
        lowermost_point = the_vertical_one.p1 if the_vertical_one.p1.y < the_vertical_one.p2.y else the_vertical_one.p2
        uppermost_point = the_vertical_one.p1 if the_vertical_one.p1.y > the_vertical_one.p2.y else the_vertical_one.p2
        if leftmost_point.x < the_vertical_one.p1.x and the_vertical_one.p1.x < rightmost_point.x and lowermost_point.y < the_horizontal_one.p1.y and the_horizontal_one.p1.y < uppermost_point.y:
            return Point(the_vertical_one.p1.x, the_horizontal_one.p1.y)
        return None

class Path:
    def __init__(self):
        self.segments = []
    
    def __repr__(self):
        return '{' + ','.join(map(lambda s: repr(s), self.segments)) + '}'
    
    def add_segment(self, segment):
        self.segments.append(segment)
    
    def distance_to(self, pt):
        distance = 0
        for s in self.segments:
            if s.contains(pt):
                distance = distance + Point.manhattan_distance(s.p1, pt)
                break
            else:
                distance = distance + Point.manhattan_distance(s.p1, s.p2)
        return distance

    @staticmethod
    def intersection(p1, p2):
        pairs = [(s1, s2) for s1 in p1.segments for s2 in p2.segments]
        intersections = [Segment.intersection(s1, s2) for (s1, s2) in pairs]
        return [i for i in intersections if i is not None]

def process_wire(segments):
    path = Path()
    current_point = Point(0, 0)
    for coord in segments:
        dir = coord[:1]
        len = int(coord[1:])
        if   dir == 'R':
            next_point = Point(current_point.x + len, current_point.y)
        elif dir == 'L':
            next_point = Point(current_point.x - len, current_point.y)
        elif dir == 'U':
            next_point = Point(current_point.x, current_point.y + len)
        elif dir == 'D':
            next_point = Point(current_point.x, current_point.y - len)
        current_segment = Segment(current_point, next_point)
        path.add_segment(current_segment)
        current_point = next_point
    return path

def read_input(filename):
    lines = [l.strip() for l in open(filename, 'r')]
    moves = map(lambda l: l.split(','), lines)
    return map(lambda m: process_wire(m), moves)

def main():
    (w1, w2) = read_input(sys.argv[1])
    intersection_points = Path.intersection(w1, w2)
    if len(intersection_points) == 0:
        print('no intersections found')
    else:
        origin = Point(0, 0)
        distances = [Point.manhattan_distance(origin, p) for p in intersection_points]
        distances.sort()
        print(f"closest intsersection is {distances[0]}")

if __name__ == '__main__':
    main()
