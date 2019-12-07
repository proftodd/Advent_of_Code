import sys

def read_memory(filename):
    fp = open(filename, 'r')
    line = fp.readline().strip()
    fp.close()
    return line.split(',')

def run_program(memory):
    instr_ptr = 0
    while memory[instr_ptr] != '99':
        instruction = memory[instr_ptr : instr_ptr + 4]
        instr_ptr = process_instruction(instr_ptr, instruction, memory)

def process_instruction(instr_ptr, instruction, memory):
    opcode = int(instruction[0])
    parm_1 = int(instruction[1])
    parm_2 = int(instruction[2])
    parm_3 = int(instruction[3])
    arg1 = int(memory[parm_1])
    arg2 = int(memory[parm_2])
    if opcode == 1:
        memory[parm_3] = str(arg1 + arg2)
    elif opcode == 2:
        memory[parm_3] = str(arg1 * arg2)
    return instr_ptr + 4
    
def write_memory(memory):
    print(','.join(memory))

def main():
    input_file = sys.argv[1]
    print('Booting up Intcode')
    print(f"Reading {input_file} into memory")
    memory = read_memory(input_file)
    print(f"Running {input_file}")
    run_program(memory)
    print(f"Writing results to stdout")
    write_memory(memory)

if __name__ == '__main__':
    main()