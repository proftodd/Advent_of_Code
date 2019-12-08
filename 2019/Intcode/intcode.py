import sys

from abc import ABC, abstractmethod

POSITION_MODE = 0
IMMEDIATE_MODE = 1

class Instruction(ABC):
    def __init__(self, modes=[], parameters=[]):
        self.modes = modes
        self.parameters = parameters

    @abstractmethod
    def name(self):
        pass

    @abstractmethod
    def expected_parameters():
        pass
    
    @abstractmethod
    def act(self, memory):
        pass

    @staticmethod
    def create_instruction(instr_ptr, memory):
        opcode = memory[instr_ptr]
        instr_code = opcode % 100
        modes = list(map(int, str(opcode // 100)))
        parameter_start = instr_ptr + 1
        if instr_code == 99:
            return (parameter_start, Stop())
        elif instr_code == 1:
            new_instr_ptr = parameter_start + Addition.expected_parameters()
            while len(modes) < Addition.expected_parameters():
                modes.insert(0, POSITION_MODE)
            return (new_instr_ptr, Addition(modes, memory[parameter_start : new_instr_ptr]))
        elif instr_code == 2:
            new_instr_ptr = parameter_start + Multiplication.expected_parameters()
            while len(modes) < Multiplication.expected_parameters():
                modes.insert(0, POSITION_MODE)
            return (new_instr_ptr, Multiplication(modes, memory[parameter_start : new_instr_ptr]))
        elif instr_code == 3:
            new_instr_ptr = parameter_start + Input.expected_parameters()
            return (new_instr_ptr, Input([POSITION_MODE], memory[parameter_start : new_instr_ptr]))
        elif instr_code == 4:
            new_instr_ptr = parameter_start + Output.expected_parameters()
            return (new_instr_ptr, Output([POSITION_MODE], memory[parameter_start : new_instr_ptr]))

class Stop(Instruction):
    def name(self):
        return 'Stop'

    @staticmethod
    def expected_parameters():
        return 0

    def act(self, memory):
        pass

class Addition(Instruction):
    def name(self):
        return 'Addition'

    @staticmethod
    def expected_parameters():
        return 3

    def act(self, memory):
        arg1 = self.parameters[0] if self.modes[-1] else memory[self.parameters[0]]
        arg2 = self.parameters[1] if self.modes[-2] else memory[self.parameters[1]]
        memory[self.parameters[2]] = arg1 + arg2

class Multiplication(Instruction):
    def name(self):
        return 'Multiplication'

    @staticmethod
    def expected_parameters():
        return 3

    def act(self, memory):
        arg1 = self.parameters[0] if self.modes[-1] else memory[self.parameters[0]]
        arg2 = self.parameters[1] if self.modes[-2] else memory[self.parameters[1]]
        memory[self.parameters[2]] = arg1 * arg2

class Input(Instruction):
    def name(self):
        return 'Input'
    
    @staticmethod
    def expected_parameters():
        return 1
    
    def act(self, memory):
        dest = self.parameters[0]
        print('Enter value: ')
        memory[dest] = int(input().strip())

class Output(Instruction):
    def name(self):
        return 'Output'

    @staticmethod
    def expected_parameters():
        return 1
    
    def act(self, memory):
        source = self.parameters[0]
        print(memory[source])

class Intcode():

    def __init__(self):
        self.memory = []

    def read_memory(self, filename):
        fp = open(filename, 'r')
        line = fp.readline().strip()
        fp.close()
        self.memory = list(map(int, line.split(',')))

    def run_program(self):
        self.instr_ptr = 0
        current_instr = None
        while current_instr == None or current_instr.name() is not 'Stop':
            self.instr_ptr, current_instr = Instruction.create_instruction(self.instr_ptr, self.memory)
            current_instr.act(self.memory)

    def write_memory(self):
        print(','.join([str(i) for i in self.memory]))

def main():
    intcode = Intcode()
    input_file = sys.argv[1]
    print('Booting up Intcode')
    print(f"Reading {input_file} into memory")
    intcode.read_memory(input_file)
    print(f"Running {input_file}")
    intcode.run_program()
    print(f"Writing results to stdout")
    intcode.write_memory()

if __name__ == '__main__':
    main()