from abc import ABC, abstractmethod

POSITION_MODE = 0
IMMEDIATE_MODE = 1


class Instruction(ABC):

    def __init__(self, computer, instr_ptr, memory):
        self.computer = computer
        self.instr_ptr = instr_ptr
        self.memory = memory
        opcode = memory[instr_ptr]
        modes = list(map(int, str(opcode // 100)))
        while len(modes) < self.expected_parameters():
            modes.insert(0, POSITION_MODE)
        self.modes = modes
        parameter_start = instr_ptr + 1
        parameter_end = parameter_start + self.expected_parameters()
        self.parameters = memory[parameter_start:parameter_end]

    @staticmethod
    @abstractmethod
    def expected_parameters():
        pass

    def get_parameter(self, index):
        return self.parameters[index] if self.modes[-1 * (index + 1)] else self.memory[self.parameters[index]]
    
    @abstractmethod
    def act(self):
        pass

    def advance_pointer(self):
        return self.instr_ptr + 1 + self.expected_parameters()

    @staticmethod
    def create_instruction(ss, instr_ptr, memory):
        opcode = memory[instr_ptr]
        instr_code = opcode % 100
        if instr_code == 99:
            return Stop(ss, instr_ptr, memory)
        elif instr_code == 1:
            return Addition(ss, instr_ptr, memory)
        elif instr_code == 2:
            return Multiplication(ss, instr_ptr, memory)
        elif instr_code == 3:
            return Input(ss, instr_ptr, memory)
        elif instr_code == 4:
            return Output(ss, instr_ptr, memory)
        elif instr_code == 5:
            return JumpIfTrue(ss, instr_ptr, memory)
        elif instr_code == 6:
            return JumpIfFalse(ss, instr_ptr, memory)
        elif instr_code == 7:
            return LessThan(ss, instr_ptr, memory)
        elif instr_code == 8:
            return Equals(ss, instr_ptr, memory)


class Stop(Instruction):

    def __init__(self, ss, instr_ptr, memory):
        super(Stop, self).__init__(ss, instr_ptr, memory)

    @staticmethod
    def expected_parameters():
        return 0

    def act(self):
        return -1


class Addition(Instruction):

    def __init__(self, ss, instr_ptr, memory):
        super(Addition, self).__init__(ss, instr_ptr, memory)

    @staticmethod
    def expected_parameters():
        return 3

    def act(self):
        arg1 = self.get_parameter(0)
        arg2 = self.get_parameter(1)
        self.memory[self.parameters[2]] = arg1 + arg2
        return self.advance_pointer()


class Multiplication(Instruction):

    def __init__(self, ss, instr_ptr, memory):
        super(Multiplication, self).__init__(ss, instr_ptr, memory)

    @staticmethod
    def expected_parameters():
        return 3

    def act(self):
        arg1 = self.get_parameter(0)
        arg2 = self.get_parameter(1)
        self.memory[self.parameters[2]] = arg1 * arg2
        return self.advance_pointer()


class Input(Instruction):

    def __init__(self, ss, instr_ptr, memory):
        super(Input, self).__init__(ss, instr_ptr, memory)

    @staticmethod
    def expected_parameters():
        return 1
    
    def act(self):
        dest = self.parameters[0]
        value = self.computer.read_input()
        self.memory[dest] = value
        return self.advance_pointer()


class Output(Instruction):

    def __init__(self, ss, instr_ptr, memory):
        super(Output, self).__init__(ss, instr_ptr, memory)

    @staticmethod
    def expected_parameters():
        return 1
    
    def act(self):
        value = self.get_parameter(0)
        self.computer.write_output(value)
        return self.advance_pointer()


class JumpIfTrue(Instruction):

    def __init__(self, ss, instr_ptr, memory):
        super(JumpIfTrue, self).__init__(ss, instr_ptr, memory)

    @staticmethod
    def expected_parameters():
        return 2

    def act(self):
        condition =   self.get_parameter(0)
        destination = self.get_parameter(1)
        if condition:
            return destination
        else:
            return self.advance_pointer()


class JumpIfFalse(Instruction):

    def __init__(self, ss, instr_ptr, memory):
        super(JumpIfFalse, self).__init__(ss, instr_ptr, memory)

    @staticmethod
    def expected_parameters():
        return 2

    def act(self):
        condition =   self.get_parameter(0)
        destination = self.get_parameter(1)
        if condition:
            return self.advance_pointer()
        else:
            return destination


class LessThan(Instruction):

    def __init__(self, ss, instr_ptr, memory):
        super(LessThan, self).__init__(ss, instr_ptr, memory)

    @staticmethod
    def expected_parameters():
        return 3
    
    def act(self):
        arg1 = self.get_parameter(0)
        arg2 = self.get_parameter(1)
        dest = self.parameters[2]
        if arg1 < arg2:
            self.memory[dest] = 1
        else:
            self.memory[dest] = 0
        return self.advance_pointer()


class Equals(Instruction):

    def __init__(self, ss, instr_ptr, memory):
        super(Equals, self).__init__(ss, instr_ptr, memory)

    @staticmethod
    def expected_parameters():
        return 3

    def act(self):
        arg1 = self.get_parameter(0)
        arg2 = self.get_parameter(1)
        dest = self.parameters[2]
        if arg1 == arg2:
            self.memory[dest] = 1
        else:
            self.memory[dest] = 0
        return self.advance_pointer()


class Intcode:

    def __init__(self, input_device=None, output_device=None):
        self.memory = []
        self.input_device = input_device
        self.output_device = output_device

    @staticmethod
    def read_memory(filename):
        fp = open(filename, 'r')
        line = fp.readline().strip()
        fp.close()
        return list(map(int, line.split(',')))

    def load_memory(self, memory):
        self.memory = memory

    def run_program(self):
        instr_ptr = 0
        while instr_ptr >= 0:
            current_instr = Instruction.create_instruction(self, instr_ptr, self.memory)
            instr_ptr = current_instr.act()
    
    def read_input(self):
        if self.input_device is None:
            print('Enter value: ')
            value = input()
        else:
            value = self.input_device.pop(0)
        if type(value) is str:
            value = int(value.strip())
        return value
    
    def write_output(self, value):
        if self.output_device is None:
            print(value)
        else:
            self.output_device.append(value)

    def write_memory(self):
        print(','.join([str(i) for i in self.memory]))