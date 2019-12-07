import sys
import intcode

def main():
    input_file = sys.argv[1]
    program = intcode.read_program(input_file)
    for i in range(100):
        for j in range(100):
            prog = program.copy()
            prog[1] = str(i)
            prog[2] = str(j)
            intcode.run_program(prog)
            if prog[0] == '19690720':
                noun = int(prog[1])
                verb = int(prog[2])
                print(f"noun: {noun}, verb: {verb}")
                print(f"value = {100 * noun + verb}")
                return
    print("No solution found")

if __name__ == '__main__':
    main()