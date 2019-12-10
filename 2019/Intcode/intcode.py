import sys

from abc import ABC, abstractmethod

POSITION_MODE = 0
IMMEDIATE_MODE = 1

class Instruction(ABC):

    def __init__(self, modes=[], parameters=[]):
        self.modes = modes
        self.parameters = parameters

    @abstractmethod
    def expected_parameters():
        pass
    
    @abstractmethod
    def act(self, instr_ptr, memory):
        pass

    @staticmethod
    def create_instruction(instr_ptr, memory):
        opcode = memory[instr_ptr]
        instr_code = opcode % 100
        modes = list(map(int, str(opcode // 100)))
        parameter_start = instr_ptr + 1
        if instr_code == 99:
            return Stop()
        elif instr_code == 1:
            parameter_end = parameter_start + Addition.expected_parameters()
            while len(modes) < Addition.expected_parameters():
                modes.insert(0, POSITION_MODE)
            return Addition(modes, memory[parameter_start : parameter_end])
        elif instr_code == 2:
            parameter_end = parameter_start + Multiplication.expected_parameters()
            while len(modes) < Multiplication.expected_parameters():
                modes.insert(0, POSITION_MODE)
            return Multiplication(modes, memory[parameter_start : parameter_end])
        elif instr_code == 3:
            parameter_end = parameter_start + Input.expected_parameters()
            return Input([POSITION_MODE], memory[parameter_start : parameter_end])
        elif instr_code == 4:
            parameter_end = parameter_start + Output.expected_parameters()
            return Output([POSITION_MODE], memory[parameter_start : parameter_end])
        elif instr_code == 5:
            parameter_end = parameter_start + JumpIfFalse.expected_parameters()
            while len(modes) < JumpIfFalse.expected_parameters():
                modes.insert(0, POSITION_MODE)
            return JumpIfFalse(modes, memory[parameter_start : parameter_end])
        elif instr_code == 6:
            parameter_end = parameter_start + JumpIfFalse.expected_parameters()
            while len(modes) < JumpIfFalse.expected_parameters():
                modes.insert(0, POSITION_MODE)
            return JumpIfFalse(modes, memory[parameter_start : parameter_end])
        elif instr_code == 7:
            parameter_end = parameter_start + LessThan.expected_parameters()
            while len(modes) < LessThan.expected_parameters():
                modes.insert(0, POSITION_MODE)
            return LessThan(modes, memory[parameter_start : parameter_end])
        elif instr_code == 8:
            parameter_end = parameter_start + Equals.expected_parameters()
            while len(modes) < Equals.expected_parameters():
                modes.insert(0, POSITION_MODE)
            return Equals(modes, memory[parameter_start : parameter_end])

class Stop(Instruction):

    @staticmethod
    def expected_parameters():
        return 0

    def act(self, instr_ptr, memory):
        return -1

class Addition(Instruction):

    def name(self):
        return 'Addition'

    @staticmethod
    def expected_parameters():
        return 3

    def act(self, instr_ptr, memory):
        arg1 = self.parameters[0] if self.modes[-1] else memory[self.parameters[0]]
        arg2 = self.parameters[1] if self.modes[-2] else memory[self.parameters[1]]
        memory[self.parameters[2]] = arg1 + arg2
        return instr_ptr + 1 + self.expected_parameters()

class Multiplication(Instruction):

    @staticmethod
    def expected_parameters():
        return 3

    def act(self, instr_ptr, memory):
        arg1 = self.parameters[0] if self.modes[-1] else memory[self.parameters[0]]
        arg2 = self.parameters[1] if self.modes[-2] else memory[self.parameters[1]]
        memory[self.parameters[2]] = arg1 * arg2
        return instr_ptr + 1 + self.expected_parameters()

class Input(Instruction):

    @staticmethod
    def expected_parameters():
        return 1
    
    def act(self, instr_ptr, memory):
        dest = self.parameters[0]
        print('Enter value: ')
        memory[dest] = int(input().strip())
        return instr_ptr + 1 + self.expected_parameters()


class Output(Instruction):

    @staticmethod
    def expected_parameters():
        return 1
    
    def act(self, instr_ptr, memory):
        source = self.parameters[0]
        print(memory[source])
        return instr_ptr + 1 + self.expected_parameters()

class JumpIfTrue(Instruction):

    @staticmethod
    def expected_parameters():
        return 2

    def act(self, instr_ptr, memory):
        condition =   self.parameters[0] if self.modes[-1] else memory[self.parameters[0]]
        destination = self.parameters[1] if self.modes[-2] else memory[self.parameters[1]]
        if condition:
            return destination
        else:
            return instr_ptr + 1 + self.expected_parameters()

class JumpIfFalse(Instruction):

    @staticmethod
    def expected_parameters():
        return 2

    def act(self, instr_ptr, memory):
        condition =   self.parameters[0] if self.modes[-1] else memory[self.parameters[0]]
        destination = self.parameters[1] if self.modes[-2] else memory[self.parameters[1]]
        if condition:
            return instr_ptr + 1 + self.expected_parameters()
        else:
            return destination

class LessThan(Instruction):

    @staticmethod
    def expected_parameters():
        return 3
    
    def act(self, instr_ptr, memory):
        arg1 = self.parameters[0] if self.modes[-1] else memory[self.parameters[0]]
        arg2 = self.parameters[1] if self.modes[-2] else memory[self.parameters[1]]
        dest = self.parameters[2]
        if arg1 < arg2:
            memory[dest] = 1
        else:
            memory[dest] = 0
        return instr_ptr + 1 + self.expected_parameters()

class Equals(Instruction):

    @staticmethod
    def expected_parameters():
        return 3

    def act(self, instr_ptr, memory):
        arg1 = self.parameters[0] if self.modes[-1] else memory[self.parameters[0]]
        arg2 = self.parameters[1] if self.modes[-2] else memory[self.parameters[1]]
        dest = self.parameters[2]
        if arg1 == arg2:
            memory[dest] = 1
        else:
            memory[dest] = 0
        return instr_ptr + 1 + self.expected_parameters()

class Intcode():

    def __init__(self):
        self.memory = []

    def read_memory(self, filename):
        fp = open(filename, 'r')
        line = fp.readline().strip()
        fp.close()
        self.memory = list(map(int, line.split(',')))
    
    def load_memory(self, memory):
        self.memory = memory

    def run_program(self):
        self.instr_ptr = 0
        while self.instr_ptr >= 0:
            current_instr = Instruction.create_instruction(self.instr_ptr, self.memory)
            self.instr_ptr = current_instr.act(self.instr_ptr, self.memory)

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