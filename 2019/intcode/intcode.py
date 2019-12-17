from abc import ABC, abstractmethod
from queue import Queue

POSITION_MODE = 0
IMMEDIATE_MODE = 1
RELATIVE_MODE = 2


class Instruction(ABC):

    def __init__(self, computer):
        self.computer = computer
        opcode = self.computer.program[self.computer.instr_ptr]
        modes = list(map(int, str(opcode // 100)))
        while len(modes) < self.expected_parameters():
            modes.insert(0, POSITION_MODE)
        self.modes = modes
        self.modes.reverse()
        parameter_start = self.computer.instr_ptr + 1
        parameter_end = parameter_start + self.expected_parameters()
        self.parameters = self.computer.program[parameter_start:parameter_end]

    @staticmethod
    @abstractmethod
    def expected_parameters():
        pass

    def get_memory_position(self, index):
        mode = self.modes[index]
        if mode == POSITION_MODE:
            return self.parameters[index]
        if mode == RELATIVE_MODE:
            return self.parameters[index] + self.computer.relative_base

    def get_parameter(self, index):
        mode = self.modes[index]
        if mode == POSITION_MODE:
            return self.computer.read_memory(self.get_memory_position(index))
        elif mode == IMMEDIATE_MODE:
            return self.parameters[index]
        elif mode == RELATIVE_MODE:
            return self.computer.read_memory(self.get_memory_position(index))

    @abstractmethod
    def act(self):
        pass

    def advance_pointer(self):
        self.computer.instr_ptr = self.computer.instr_ptr + 1 + self.expected_parameters()

    @staticmethod
    def create_instruction(ss):
        opcode = ss.program[ss.instr_ptr]
        instr_code = opcode % 100
        if instr_code == 99:
            return Stop(ss)
        elif instr_code == 1:
            return Addition(ss)
        elif instr_code == 2:
            return Multiplication(ss)
        elif instr_code == 3:
            return Input(ss)
        elif instr_code == 4:
            return Output(ss)
        elif instr_code == 5:
            return JumpIfTrue(ss)
        elif instr_code == 6:
            return JumpIfFalse(ss)
        elif instr_code == 7:
            return LessThan(ss)
        elif instr_code == 8:
            return Equals(ss)


class Stop(Instruction):

    def __init__(self, ss):
        super(Stop, self).__init__(ss)

    @staticmethod
    def expected_parameters():
        return 0

    def act(self):
        self.computer.instr_ptr = -1


class Addition(Instruction):

    def __init__(self, ss):
        super(Addition, self).__init__(ss)
        if self.modes[2] == IMMEDIATE_MODE:
            raise ValueError('Addition cannot have a destination parameter in IMMEDIATE mode')

    @staticmethod
    def expected_parameters():
        return 3

    def act(self):
        arg1 = self.get_parameter(0)
        arg2 = self.get_parameter(1)
        dest = self.get_memory_position(2)
        self.computer.write_memory(dest, arg1 + arg2)
        self.advance_pointer()


class Multiplication(Instruction):

    def __init__(self, ss):
        super(Multiplication, self).__init__(ss)
        if self.modes[2] == IMMEDIATE_MODE:
            raise ValueError('Multiplication cannot have a destination parameter in IMMEDIATE mode')

    @staticmethod
    def expected_parameters():
        return 3

    def act(self):
        arg1 = self.get_parameter(0)
        arg2 = self.get_parameter(1)
        dest = self.get_memory_position(2)
        self.computer.write_memory(dest, arg1 * arg2)
        self.advance_pointer()


class Input(Instruction):

    def __init__(self, ss):
        super(Input, self).__init__(ss)
        if self.modes[0] == IMMEDIATE_MODE:
            raise ValueError('Input cannot have a destination parameter in IMMEDIATE mode')

    @staticmethod
    def expected_parameters():
        return 1
    
    def act(self):
        value = self.computer.read_input()
        dest = self.get_memory_position(0)
        self.computer.write_memory(dest, value)
        self.advance_pointer()


class Output(Instruction):

    def __init__(self, ss):
        super(Output, self).__init__(ss)

    @staticmethod
    def expected_parameters():
        return 1
    
    def act(self):
        value = self.get_parameter(0)
        self.computer.write_output(value)
        self.advance_pointer()


class JumpIfTrue(Instruction):

    def __init__(self, ss):
        super(JumpIfTrue, self).__init__(ss)

    @staticmethod
    def expected_parameters():
        return 2

    def act(self):
        condition =   self.get_parameter(0)
        destination = self.get_parameter(1)
        if condition:
            self.computer.instr_ptr = destination
        else:
            self.advance_pointer()


class JumpIfFalse(Instruction):

    def __init__(self, ss):
        super(JumpIfFalse, self).__init__(ss)

    @staticmethod
    def expected_parameters():
        return 2

    def act(self):
        condition =   self.get_parameter(0)
        destination = self.get_parameter(1)
        if condition:
            self.advance_pointer()
        else:
            self.computer.instr_ptr = destination


class LessThan(Instruction):

    def __init__(self, ss):
        super(LessThan, self).__init__(ss)
        if self.modes[2] == IMMEDIATE_MODE:
            raise ValueError('LessThan cannot have a destination parameter in IMMEDIATE mode')

    @staticmethod
    def expected_parameters():
        return 3
    
    def act(self):
        arg1 = self.get_parameter(0)
        arg2 = self.get_parameter(1)
        dest = self.get_memory_position(2)
        if arg1 < arg2:
            self.computer.write_memory(dest, 1)
        else:
            self.computer.write_memory(dest, 0)
        self.advance_pointer()


class Equals(Instruction):

    def __init__(self, ss):
        super(Equals, self).__init__(ss)
        if self.modes[2] == IMMEDIATE_MODE:
            raise ValueError('LessThan cannot have a destination parameter in IMMEDIATE mode')

    @staticmethod
    def expected_parameters():
        return 3

    def act(self):
        arg1 = self.get_parameter(0)
        arg2 = self.get_parameter(1)
        dest = self.get_memory_position(2)
        if arg1 == arg2:
            self.computer.write_memory(dest, 1)
        else:
            self.computer.write_memory(dest, 0)
        self.advance_pointer()


class RelativeBase(Instruction):

    def __init__(self, ss):
        super(RelativeBase, self).__init__(ss)

    @staticmethod
    def expected_parameters():
        return 1

    def act(self):
        arg = self.get_parameter(0)
        self.computer.relative_base = arg
        self.advance_pointer()


class Intcode:

    def __init__(self, input_device=None, output_device=None):
        self.program = []
        self.memory = {}
        self.instr_ptr = 0
        self.relative_base = 0
        self.input_device = input_device
        self.output_device = output_device

    @staticmethod
    def read_program(filename):
        fp = open(filename, 'r')
        line = fp.readline().strip()
        fp.close()
        return list(map(int, line.split(',')))

    def load_program(self, program):
        self.program = program

    def run_program(self):
        self.instr_ptr = 0
        while self.instr_ptr >= 0:
            current_instr = Instruction.create_instruction(self)
            current_instr.act()

    def read_memory(self, position):
        if position < len(self.program):
            return self.program[position]
        else:
            return self.memory[position - len(self.program)]

    def write_memory(self, position, value):
        if position < len(self.program):
            self.program[position] = value
        else:
            self.memory[position - len(self.program)] = value
    
    def read_input(self):
        if self.input_device is None:
            print('Enter value: ')
            value = input()
        elif type(self.input_device) == list:
            value = self.input_device.pop(0)
        elif type(self.input_device) == Queue:
            value = self.input_device.get()
        if type(value) is str:
            value = int(value.strip())
        return value
    
    def write_output(self, value):
        if self.output_device is None:
            print(value)
        elif type(self.output_device) == list:
            self.output_device.append(value)
        elif type(self.output_device) == Queue:
            self.output_device.put(value)

    def write_program(self):
        print(','.join([str(i) for i in self.program]))
