import sys

from abc import ABC, abstractmethod

class Instruction(ABC):
    def __init__(self, parameters=None):
        self.parameters = parameters if parameters else []

    @abstractmethod
    def name(self):
        pass

    @abstractmethod
    def length():
        pass
    
    @abstractmethod
    def act(self, memory):
        pass

    @staticmethod
    def create_instruction(instr_ptr, memory):
        if memory[instr_ptr] == '99':
            new_instr_ptr = instr_ptr + Stop.length()
            return (new_instr_ptr, Stop())
        elif memory[instr_ptr] == '1':
            parameter_start = instr_ptr + 1
            new_instr_ptr = instr_ptr + Addition.length()
            return (new_instr_ptr, Addition(memory[parameter_start : new_instr_ptr]))
        elif memory[instr_ptr] == '2':
            parameter_start = instr_ptr + 1
            new_instr_ptr = instr_ptr + Multiplication.length()
            return (new_instr_ptr, Multiplication(memory[parameter_start : new_instr_ptr]))

class Stop(Instruction):
    def name(self):
        return 'Stop'

    @staticmethod
    def length():
        return 1

    def act(self, memory):
        pass

class Addition(Instruction):
    def name(self):
        return 'Addition'

    @staticmethod
    def length():
        return 4

    def act(self, memory):
        arg1 = int(memory[int(self.parameters[0])])
        arg2 = int(memory[int(self.parameters[1])])
        memory[int(self.parameters[2])] = str(arg1 + arg2)

class Multiplication(Instruction):
    def name(self):
        return 'Multiplication'

    @staticmethod
    def length():
        return 4

    def act(self, memory):
        arg1 = int(memory[int(self.parameters[0])])
        arg2 = int(memory[int(self.parameters[1])])
        memory[int(self.parameters[2])] = str(arg1 * arg2)

class Intcode():

    def __init__(self):
        self.memory = []

    def read_memory(self, filename):
        fp = open(filename, 'r')
        line = fp.readline().strip()
        fp.close()
        self.memory = line.split(',')

    def run_program(self):
        self.instr_ptr = 0
        current_instr = None
        while current_instr == None or current_instr.name() is not 'Stop':
            self.instr_ptr, current_instr = Instruction.create_instruction(self.instr_ptr, self.memory)
            current_instr.act(self.memory)

    def write_memory(self):
        print(','.join(self.memory))

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