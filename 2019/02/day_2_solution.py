import sys
import intcode.intcode

def main():
    ss = intcode.intcode.Intcode()
    input_file = sys.argv[1]
    print('Booting up Intcode')
    print(f"Reading {input_file} into memory")
    program = intcode.intcode.Intcode.read_memory(input_file)
    print(f"Running {input_file}")
    ss.load_memory(program)
    ss.run_program()
    print(f"Writing results to stdout")
    ss.write_memory()


if __name__ == '__main__':
    main()
