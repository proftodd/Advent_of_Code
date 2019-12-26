import sys
import intcode.intcode


def main():
    input_file = sys.argv[1]
    program = intcode.intcode.Intcode.read_program(input_file)
    for i in range(100):
        for j in range(100):
            prog = program.copy()
            prog[1] = i
            prog[2] = j
            ss = intcode.intcode.Intcode()
            ss.load_program(prog)
            ss.run_program()
            if prog[0] == 19690720:
                noun = prog[1]
                verb = prog[2]
                print(f"noun: {noun}, verb: {verb}")
                print(f"value = {100 * noun + verb}")
                return
    print("No solution found")


if __name__ == '__main__':
    main()
