import itertools
import sys
import intcode.intcode

def try_permutation(program, perm):
    value = 0
    for i in perm:
        input_buffer = [i, value]
        output_buffer = []
        ss = intcode.intcode.Intcode(input_device=input_buffer, output_device=output_buffer)
        ss.load_memory(program)
        ss.run_program()
        value = output_buffer[0]
    return value


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
    settings = [0, 1, 2, 3, 4]
    max_thrust, best_settings = maximize_thrust(program, settings)
    print(f"maximum thrust of {max_thrust} achieved with {best_settings}")

if __name__ == '__main__':
    main()
