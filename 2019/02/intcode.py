import sys

def read_program(filename):
    fp = open(filename, 'r')
    line = fp.readline().strip()
    fp.close()
    return line.split(',')

def run_program(program):
    position = 0
    while program[position] != '99':
        operation = program[position : position + 4]
        position = process_operation(position, operation, program)

def process_operation(position, operation, program):
    opcode = int(operation[0])
    arg1_addr = int(operation[1])
    arg2_addr = int(operation[2])
    dest = int(operation[3])
    arg1 = int(program[arg1_addr])
    arg2 = int(program[arg2_addr])
    if opcode == 1:
        program[dest] = str(arg1 + arg2)
    elif opcode == 2:
        program[dest] = str(arg1 * arg2)
    return position + 4
    
def write_program(program):
    # fp = open(filename, 'w')
    # fp.write(','.join(program))
    # fp.write("\n")
    # fp.close()
    print(','.join(program))

def main():
    input_file = sys.argv[1]
    print('Booting up Intcode')
    print(f"Reading {input_file}")
    prog = read_program(input_file)
    print(f"Running {input_file}")
    run_program(prog)
    print(f"Writing results to stdout")
    write_program(prog)

if __name__ == '__main__':
    main()