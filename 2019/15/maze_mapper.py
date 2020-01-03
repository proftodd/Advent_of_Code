import sys
from queue import Queue
from intcode.intcode import Intcode
import maze_runner as mr


maze = {}
max_depth = None
position_change = {
    mr.NORTH: (0, 1),
    mr.SOUTH: (0, -1),
    mr.WEST: (-1, 0),
    mr.EAST: (1, 0)
}
map_icons = {
    -1: 'd',
    mr.WALL: '#',
    mr.CLEAR: '.',
    mr.TANK: 'O'
}


def map_maze(the_program, current_location, path_so_far):
    last_step = path_so_far.queue[-1]
    path_copy = mr.copy_path(path_so_far, mr.HALT)
    this_result = mr.try_direction(list(the_program), path_copy)
    maze[current_location] = map_icons[this_result]
    if this_result == mr.WALL:
        return
    if max_depth is not None and len(list(path_so_far.queue)) >= max_depth:
        return
    for next_dir in mr.directions:
        if next_dir == mr.opposite_of(last_step):
            continue
        pc = position_change[next_dir]
        next_location = (
            current_location[0] + pc[0],
            current_location[1] + pc[1]
        )
        extended_path = mr.copy_path(path_so_far, next_dir)
        map_maze(list(the_program), next_location, extended_path)


if __name__ == '__main__':
    program = Intcode.read_program(sys.argv[1])
    output_file = sys.argv[2]
    max_depth = int(sys.argv[3]) if len(sys.argv) > 3 else None

    maze[(0, 0)] = map_icons[-1]
    for d in mr.directions:
        location = position_change[d]
        map_maze(list(program), location, mr.copy_path(Queue(), d))

    max_x = 0
    min_x = 0
    max_y = 0
    min_y = 0
    for p in maze.keys():
        max_x = p[0] if p[0] > max_x else max_x
        min_x = p[0] if p[0] < min_x else min_x
        max_y = p[1] if p[1] > max_y else max_y
        min_y = p[1] if p[1] < min_y else min_y

    with open(output_file, 'w') as op:
        for j in range(max_y, min_y - 1, -1):
            for i in range(min_x, max_x + 1):
                op.write(maze.get((i, j), map_icons[mr.WALL]))
            op.write('\n')
