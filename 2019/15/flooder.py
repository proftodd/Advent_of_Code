from sys import argv
import maze_mapper as mm
import maze_runner as mr


def print_maze(this_time, this_maze):
    this_max_x = 0
    this_min_x = 0
    this_max_y = 0
    this_min_y = 0
    for this_p in this_maze.keys():
        this_max_x = this_p[0] if this_p[0] > this_max_x else this_max_x
        this_min_x = this_p[0] if this_p[0] < this_min_x else this_min_x
        this_max_y = this_p[1] if this_p[1] > this_max_y else this_max_y
        this_min_y = this_p[1] if this_p[1] < this_min_y else this_min_y

    print(f"Maze after {this_time} minutes:")
    for this_j in range(this_min_y, this_max_y + 1):
        for this_i in range(this_min_x, this_max_x + 1):
            print(this_maze.get((this_i, this_j), mm.map_icons[mr.WALL]), end='')
        print()
    print()


if __name__ == '__main__':
    filename = argv[1]
    maze = {}
    y = 0
    with open(filename, 'r') as fp:
        for line in fp.readlines():
            line = line.strip()
            x = 0
            for c in line:
                maze[(x, y)] = c
                x = x + 1
            y = y + 1

    time = 0
    print_maze(time, maze)
    while any([v == '.' or v == 'd' for v in maze.values()]):
        time = time + 1
        for position in maze:
            if maze[position] == mm.map_icons[mr.TANK]:
                for direction in mm.position_change.values():
                    adjacent = (
                        position[0] + direction[0],
                        position[1] + direction[1]
                    )
                    adjacent_condition = maze.get(adjacent, mm.map_icons[mr.WALL])
                    if adjacent_condition == mm.map_icons[mr.CLEAR] or adjacent_condition == mm.map_icons[-1]:
                        maze[adjacent] = mm.map_icons[mr.TANK]
        if time % 20 == 0:
            print_maze(time, maze)
    print(f"It took {time} minutes to fill the ship with oxygen")
    print_maze(time, maze)
