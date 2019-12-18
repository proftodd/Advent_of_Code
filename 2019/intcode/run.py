import sys
import intcode


def main():
    program = intcode.Intcode.read_program(sys.argv[1])
    ss = intcode.Intcode()
    ss.load_program(program)
    ss.run_program()


if __name__ == '__main__':
    main()