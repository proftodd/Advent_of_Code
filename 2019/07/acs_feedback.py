import itertools
import sys
import intcode.intcode


def try_permutation(program, perm):
    input_buffers = [
        [perm[0], 0], [perm[1]], [perm[2]], [perm[3]], [perm[4]]
    ]
    ss = []
    for i in range(len(perm)):
        ss.append(intcode.intcode.Intcode(input_buffers[i], input_buffers[(i + 1) % len(perm)]))
        ss[i].load_memory(program)
    while len(input_buffers[0]) > 1:
        for i in range(len(perm)):
            ss[i].run_program()
    return input_buffers[0][0]


def maximize_thrust(program, settings):
    max_thrust = 0
    best_settings = None
    for perm in itertools.permutations(settings):
        this_thrust = try_permutation(program, perm)
        if this_thrust > max_thrust:
            best_settings = perm
            max_thrust = this_thrust
    return max_thrust, best_settings


def main():
    program = intcode.intcode.Intcode.read_memory(sys.argv[1])
    settings = [5, 6, 7, 8, 9]
    max_thrust, best_settings = maximize_thrust(program, settings)
    print(f"maximum thrust of {max_thrust} achieved with {best_settings}")


if __name__ == '__main__':
    main()
