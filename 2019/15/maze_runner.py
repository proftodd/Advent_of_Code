import sys
from queue import LifoQueue, Queue
from intcode.intcode import Intcode

HALT = 0
NORTH = 1
SOUTH = 2
WEST = 3
EAST = 4

directions = [NORTH, SOUTH, WEST, EAST]

WALL = 0
CLEAR = 1
TANK = 2


MAX_DEPTH = None


def copy_path(path, next_step=None):
    path_copy = Queue()
    path_copy.queue.extend(list(path.queue))
    if next_step is not None:
        path_copy.put(next_step)
    return path_copy


def try_direction(my_program, path_to_try):
    this_result_buffer = LifoQueue()
    this_runner = Intcode(input_device=path_to_try, output_device=this_result_buffer)
    this_runner.load_program(my_program)
    this_runner.run_program()
    return this_result_buffer.get()


def opposite_of(step):
    if step == NORTH:
        return SOUTH
    elif step == SOUTH:
        return NORTH
    elif step == EAST:
        return WEST
    elif step == WEST:
        return EAST


def try_a_step(the_program, path_so_far):
    last_step = path_so_far.queue[-1]
    path_copy = copy_path(path_so_far, HALT)
    this_result = try_direction(list(the_program), path_copy)
    if this_result == TANK:
        return [path_so_far]
    elif this_result == WALL:
        return []
    elif MAX_DEPTH is not None and len(list(path_so_far.queue)) >= MAX_DEPTH:
        return [path_so_far]
    candidate_directions = [d for d in directions if d != opposite_of(last_step)]
    candidate_paths = [copy_path(path_so_far, d) for d in candidate_directions]
    next_steps = [try_a_step(the_program, p) for p in candidate_paths]
    possible_solutions = [p for pl in next_steps for p in pl]
    return possible_solutions


if __name__ == '__main__':
    program = Intcode.read_program(sys.argv[1])
    starting_paths = [copy_path(Queue(), d) for d in directions]
    solutions = [try_a_step(list(program), p) for p in starting_paths]
    true_solutions = [p for pl in solutions for p in pl]
    for the_solution in true_solutions:
        print(f"Minimum number of steps: {len(list(the_solution.queue))}")
        print(f"The solution is : {','.join(map(str, the_solution.queue))}")
