import queue

from intcode.intcode import (
    Stop,
    Addition,
    Multiplication,
    Input,
    Output,
    JumpIfTrue,
    JumpIfFalse,
    LessThan,
    Equals,
    RelativeBase,
    Intcode
)


def test_stop():
    program = [99]
    ss = Intcode()
    ss.load_program(program)
    stop = Stop(ss)
    stop.act()
    assert ss.instr_ptr == -1


def test_addition_position_mode():
    program = [1, 0, 1, 4, 0]
    ss = Intcode()
    ss.load_program(program)
    adder = Addition(ss)
    adder.act()
    assert ss.instr_ptr == 4
    assert ss.read_memory(4) == 1


def test_addition_immediate_mode():
    program = [1101, 1, 2, 4, 0]
    ss = Intcode()
    ss.load_program(program)
    adder_immediate = Addition(ss)
    adder_immediate.act()
    assert ss.instr_ptr == 4
    assert ss.read_memory(4) == 3


def test_addition_cannot_have_destination_parameter_in_IMMEDIATE_mode():
    ss = Intcode()
    ss.load_program([11101, 1, 2, 4, 0])
    try:
        Addition(ss)
        assert False
    except ValueError:
        assert True


def test_addition_relative_mode():
    program = [22201, 0, 1, 2, 1, 2, 0]
    ss = Intcode()
    ss.load_program(program)
    ss.relative_base = 4
    adder = Addition(ss)
    adder.act()
    assert ss.read_memory(6) == 3


def test_multiplication_position_mode():
    program = [2, 0, 2, 4, 0]
    ss = Intcode()
    ss.load_program(program)
    mult_position = Multiplication(ss)
    mult_position.act()
    assert ss.instr_ptr == 4
    assert ss.read_memory(4) == 4


def test_multiplication_immediate_mode():
    program = [1102, 2, 2, 4, 0]
    ss = Intcode()
    ss.load_program(program)
    mult_immediate = Multiplication(ss)
    mult_immediate.act()
    assert ss.instr_ptr == 4
    assert ss.read_memory(4) == 4


def test_multiplication_cannot_have_destination_parameter_in_IMMEDIATE_mode():
    ss = Intcode()
    ss.load_program([11102, 1, 2, 4, 0])
    try:
        Multiplication(ss)
        assert False
    except ValueError:
        assert True


def test_multiplication_relative_mode():
    program = [22202, 0, 1, 2, 1, 2, 0]
    ss = Intcode()
    ss.load_program(program)
    ss.relative_base = 4
    multiplier = Multiplication(ss)
    multiplier.act()
    assert ss.read_memory(6) == 2


def test_input():
    program = [4, 2, 0]
    input_buffer = queue.Queue()
    input_buffer.put(42)
    ss = Intcode(input_device=input_buffer)
    ss.load_program(program)
    input_instruction = Input(ss)
    input_instruction.act()
    assert ss.instr_ptr == 2
    assert ss.read_memory(2) == 42


def test_input_cannot_have_destination_parameter_in_IMMEDIATE_mode():
    ss = Intcode()
    ss.load_program([104, 1])
    try:
        Input(ss)
        assert False
    except ValueError:
        assert True


def test_input_relative():
    program = [204, 0, 0]
    q = queue.Queue()
    q.put('42')
    ss = Intcode(input_device=q)
    ss.load_program(program)
    ss.relative_base = 2
    input_instruction = Input(ss)
    input_instruction.act()
    assert ss.read_memory(2) == 42


def test_output_position():
    program = [5, 2, 69]
    output_buffer = queue.Queue()
    ss = Intcode(output_device=output_buffer)
    ss.load_program(program)
    output_instruction = Output(ss)
    output_instruction.act()
    assert ss.instr_ptr == 2
    assert output_buffer.get() == 69


def test_output_immediate():
    program = [105, 69]
    output_buffer = queue.Queue()
    ss = Intcode(output_device=output_buffer)
    ss.load_program(program)
    output_instruction = Output(ss)
    output_instruction.act()
    assert ss.instr_ptr == 2
    assert output_buffer.get() == 69


def test_jump_if_true_position():
    program = [5, 3, 2, 1]
    ss = Intcode()
    ss.load_program(program)
    jump_true = JumpIfTrue(ss)
    jump_true.act()
    assert ss.instr_ptr == 2
    program = [5, 3, 2, 0]
    ss = Intcode()
    ss.load_program(program)
    jump_false = JumpIfTrue(ss)
    jump_false.act()
    assert ss.instr_ptr == 3


def test_jump_if_true_immediate():
    program = [1105, 1, 2]
    ss = Intcode()
    ss.load_program(program)
    will_true = JumpIfTrue(ss)
    will_true.act()
    assert ss.instr_ptr == 2
    program = [1105, 0, 2]
    ss = Intcode()
    ss.load_program(program)
    wont_jump = JumpIfTrue(ss)
    wont_jump.act()
    assert ss.instr_ptr == 3


def test_jump_if_false_position():
    program = [6, 3, 2, 0]
    ss = Intcode()
    ss.load_program(program)
    will_jump = JumpIfFalse(ss)
    will_jump.act()
    assert ss.instr_ptr == 2
    program = [6, 3, 2, 1]
    ss = Intcode()
    ss.load_program(program)
    wont_jump = JumpIfFalse(ss)
    wont_jump.act()
    assert ss.instr_ptr == 3


def test_jump_if_false_immediate():
    program = [1106, 0, 2]
    ss = Intcode()
    ss.load_program(program)
    will_jump = JumpIfFalse(ss)
    will_jump.act()
    assert ss.instr_ptr == 2
    program = [1106, 1, 2]
    ss = Intcode()
    ss.load_program(program)
    wont_jump = JumpIfFalse(ss)
    wont_jump.act()
    assert ss.instr_ptr == 3


def test_less_than_position():
    program = [7, 0, 2, 4, 0]
    ss = Intcode()
    ss.load_program(program)
    less_than_true = LessThan(ss)
    less_than_true.act()
    assert ss.instr_ptr == 4
    assert ss.read_memory(4) == 0
    program = [7, 4, 2, 4, 0]
    ss = Intcode()
    ss.load_program(program)
    less_than_false = LessThan(ss)
    less_than_false.act()
    assert ss.instr_ptr == 4
    assert ss.read_memory(4) == 1


def test_less_than_immediate():
    program = [1107, 0, 2, 4, 0]
    ss = Intcode()
    ss.load_program(program)
    less_than_true = LessThan(ss)
    less_than_true.act()
    assert ss.instr_ptr == 4
    assert ss.read_memory(4) == 1
    program = [1107, 4, 2, 4, 0]
    ss = Intcode()
    ss.load_program(program)
    less_than_false = LessThan(ss)
    less_than_false.act()
    assert ss.instr_ptr == 4
    assert ss.read_memory(4) == 0


def test_less_than_cannot_have_destination_parameter_in_IMMEDIATE_mode():
    ss = Intcode()
    ss.load_program([11107, 1, 2, 4, 0])
    try:
        LessThan(ss)
        assert False
    except ValueError:
        assert True


def test_less_than_relative_mode():
    program = [22207, 0, 1, 2, 1, 2, -1]
    ss = Intcode()
    ss.load_program(program)
    ss.relative_base = 4
    lt_true = LessThan(ss)
    lt_true.act()
    assert ss.read_memory(6) == 1
    program = [22207, 0, 1, 2, 3, 2, -1]
    ss = Intcode()
    ss.load_program(program)
    ss.relative_base = 4
    lt_false = LessThan(ss)
    lt_false.act()
    assert ss.read_memory(6) == 0


def test_equal_position():
    program = [8, 0, 2, 4, 0]
    ss = Intcode()
    ss.load_program(program)
    less_than_true = Equals(ss)
    less_than_true.act()
    assert ss.instr_ptr == 4
    assert ss.read_memory(4) == 0
    program = [8, 4, 5, 6, 0, 0, 0]
    ss = Intcode()
    ss.load_program(program)
    less_than_false = Equals(ss)
    less_than_false.act()
    assert ss.instr_ptr == 4
    assert ss.read_memory(6) == 1


def test_equal_immediate():
    program = [1108, 0, 2, 4, 0]
    ss = Intcode()
    ss.load_program(program)
    less_than_true = Equals(ss)
    less_than_true.act()
    assert ss.instr_ptr == 4
    assert ss.read_memory(4) == 0
    program = [1108, 2, 2, 4, 0]
    ss = Intcode()
    ss.load_program(program)
    less_than_false = Equals(ss)
    less_than_false.act()
    assert ss.instr_ptr == 4
    assert ss.read_memory(4) == 1


def test_equals_cannot_have_destination_parameter_in_IMMEDIATE_mode():
    ss = Intcode()
    ss.load_program([11108, 1, 2, 4, 0])
    try:
        Equals(ss)
        assert False
    except ValueError:
        assert True


def test_equals_relative_mode():
    program = [22208, 0, 1, 2, 1, 1, -1]
    ss = Intcode()
    ss.load_program(program)
    ss.relative_base = 4
    eq_true = Equals(ss)
    eq_true.act()
    assert ss.read_memory(6) == 1
    program = [22208, 0, 1, 2, 3, 2, -1]
    ss = Intcode()
    ss.load_program(program)
    ss.relative_base = 4
    eq_false = Equals(ss)
    eq_false.act()
    assert ss.read_memory(6) == 0


def test_relative_base_position():
    program = [9, 2, -7]
    ss = Intcode()
    ss.relative_base = 10
    ss.load_program(program)
    relative_base = RelativeBase(ss)
    relative_base.act()
    assert ss.relative_base == 3


def test_relative_base_immediate():
    program = [109, 19]
    ss = Intcode()
    ss.relative_base = 5
    ss.load_program(program)
    relative_base = RelativeBase(ss)
    relative_base.act()
    assert ss.relative_base == 24


def test_relative_base_relative():
    program = [209, 0, -69]
    ss = Intcode()
    ss.relative_base = 70
    ss.load_program(program)
    relative_base = RelativeBase(ss)
    relative_base.act()
    assert ss.relative_base == 70


def test_day_02_example_00():
    memory = [99]
    ss = Intcode()
    ss.load_program(memory)
    ss.run_program()
    assert memory == [99]


def test_day_02_example_01():
    memory = [1, 0, 0, 0, 99]
    ss = Intcode()
    ss.load_program(memory)
    ss.run_program()
    assert memory == [2, 0, 0, 0, 99]


def test_day_02_example_02():
    memory = [2, 3, 0, 3, 99]
    ss = Intcode()
    ss.load_program(memory)
    ss.run_program()
    assert memory == [2, 3, 0, 6, 99]


def test_day_02_example_03():
    memory = [2, 4, 4, 5, 99, 0]
    ss = Intcode()
    ss.load_program(memory)
    ss.run_program()
    assert memory == [2, 4, 4, 5, 99, 9801]


def test_day_03_example_04():
    memory = [1, 1, 1, 4, 99, 5, 6, 0, 99]
    ss = Intcode()
    ss.load_program(memory)
    ss.run_program()
    assert memory == [30, 1, 1, 4, 2, 5, 6, 0, 99]


def test_day_04_input():
    program = Intcode.read_program('../02/input.txt')
    ss = Intcode()
    ss.load_program(program)
    ss.run_program()
    assert ss.program[0] == 394702


def test_day_04_corrected_input():
    memory = Intcode.read_program('../02/input.txt')
    memory[1] = 12
    memory[2] = 2
    ss = Intcode()
    ss.load_program(memory)
    ss.run_program()
    assert ss.program[0] == 3850704


def test_day_04_part_2():
    memory = Intcode.read_program('../02/input.txt')
    memory[1] = 67
    memory[2] = 18
    ss = Intcode()
    ss.load_program(memory)
    ss.run_program()
    assert ss.program[0] == 19690720


def test_day_05_example_01():
    input_buffer = queue.Queue()
    input_buffer.put(42)
    output_buffer = queue.Queue()
    ss = Intcode(input_buffer, output_buffer)
    memory = [3, 0, 4, 0, 99]
    ss.load_program(memory)
    ss.run_program()
    assert memory == [42, 0, 4, 0, 99]
    assert input_buffer.empty()
    assert output_buffer.get() == 42


def test_day_05_example_02():
    input_buffer_1 = queue.Queue()
    input_buffer_1.put(1)
    output_buffer_1 = queue.Queue()
    ss = Intcode(input_device=input_buffer_1, output_device=output_buffer_1)
    original_memory = [3, 9, 8, 9, 10, 9, 4, 9, 99, -1, 8]
    memory = list(original_memory)
    ss.load_program(memory)
    ss.run_program()
    assert memory[9] == 0
    assert output_buffer_1.get() == 0
    input_buffer_2 = queue.Queue()
    input_buffer_2.put(8)
    output_buffer_2 = queue.Queue()
    ss = Intcode(input_device=input_buffer_2, output_device=output_buffer_2)
    memory = list(original_memory)
    ss.load_program(memory)
    ss.run_program()
    assert memory[9] == 1
    assert output_buffer_2.get() == 1


def test_day_05_example_03():
    input_buffer_1 = queue.Queue()
    input_buffer_1.put(1)
    output_buffer_1 = queue.Queue()
    ss = Intcode(input_device=input_buffer_1, output_device=output_buffer_1)
    original_memory = [3, 9, 7, 9, 10, 9, 4, 9, 99, -1, 8]
    memory = list(original_memory)
    ss.load_program(memory)
    ss.run_program()
    assert memory[9] == 1
    assert output_buffer_1.get() == 1
    input_buffer_2 = queue.Queue()
    input_buffer_2.put(9)
    output_buffer_2 = queue.Queue()
    ss = Intcode(input_device=input_buffer_2, output_device=output_buffer_2)
    memory = list(original_memory)
    ss.load_program(memory)
    ss.run_program()
    assert memory[9] == 0
    assert output_buffer_2.get() == 0


def test_day_05_example_04():
    input_buffer_1 = queue.Queue()
    input_buffer_1.put(1)
    output_buffer_1 = queue.Queue()
    ss = Intcode(input_device=input_buffer_1, output_device=output_buffer_1)
    original_memory = [3, 3, 1108, -1, 8, 3, 4, 3, 99]
    memory = list(original_memory)
    ss.load_program(memory)
    ss.run_program()
    assert memory == [3, 3, 1108, 0, 8, 3, 4, 3, 99]
    assert output_buffer_1.get() == 0
    input_buffer_2 = queue.Queue()
    input_buffer_2.put(8)
    output_buffer_2 = queue.Queue()
    ss = Intcode(input_device=input_buffer_2, output_device=output_buffer_2)
    memory = list(original_memory)
    ss.load_program(memory)
    ss.run_program()
    assert memory == [3, 3, 1108, 1, 8, 3, 4, 3, 99]
    assert output_buffer_2.get() == 1


def test_day_05_example_05():
    input_buffer_1 = queue.Queue()
    input_buffer_1.put(1)
    output_buffer_1 = queue.Queue()
    ss = Intcode(input_device=input_buffer_1, output_device=output_buffer_1)
    original_memory = [3, 3, 1107, -1, 8, 3, 4, 3, 99]
    memory = list(original_memory)
    ss.load_program(memory)
    ss.run_program()
    assert memory[3] == 1
    assert output_buffer_1.get() == 1
    input_buffer_2 = queue.Queue()
    input_buffer_2.put(9)
    output_buffer_2 = queue.Queue()
    ss = Intcode(input_device=input_buffer_2, output_device=output_buffer_2)
    memory = list(original_memory)
    ss.load_program(memory)
    ss.run_program()
    assert memory[3] == 0
    assert output_buffer_2.get() == 0


def test_day_05_example_06():
    input_buffer_1 = queue.Queue()
    input_buffer_1.put(1)
    output_buffer_1 = queue.Queue()
    ss = Intcode(input_device=input_buffer_1, output_device=output_buffer_1)
    original_memory = [3, 12, 6, 12, 15, 1, 13, 14, 13, 4, 13, 99, -1, 0, 1, 9]
    memory = list(original_memory)
    ss.load_program(memory)
    ss.run_program()
    assert memory[12] == 1
    assert output_buffer_1.get() == 1
    input_buffer_2 = queue.Queue()
    input_buffer_2.put(0)
    output_buffer_2 = queue.Queue()
    ss = Intcode(input_device=input_buffer_2, output_device=output_buffer_2)
    memory = list(original_memory)
    ss.load_program(memory)
    ss.run_program()
    assert memory[12] == 0
    assert output_buffer_2.get() == 0


def test_day_05_example_07():
    original_memory = [3, 3, 1105, -1, 9, 1101, 0, 0, 12, 4, 12, 99, 1]
    input_buffer_1 = queue.Queue()
    input_buffer_1.put(1)
    output_buffer_1 = queue.Queue()
    ss = Intcode(input_device=input_buffer_1, output_device=output_buffer_1)
    memory = list(original_memory)
    ss.load_program(memory)
    ss.run_program()
    assert memory == [3, 3, 1105, 1, 9, 1101, 0, 0, 12, 4, 12, 99, 1]
    assert output_buffer_1.get() == 1
    input_buffer_2 = queue.Queue()
    input_buffer_2.put(0)
    output_buffer_2 = queue.Queue()
    ss = Intcode(input_device=input_buffer_2, output_device=output_buffer_2)
    memory = list(original_memory)
    ss.load_program(memory)
    ss.run_program()
    assert memory == [3, 3, 1105, 0, 9, 1101, 0, 0, 12, 4, 12, 99, 0]
    assert output_buffer_2.get() == 0


def test_day_05_example_08():
    original_memory = [3, 21, 1008, 21, 8, 20, 1005, 20, 22, 107, 8, 21, 20, 1006, 20, 31, 1106, 0, 36, 98, 0, 0, 1002, 21, 125, 20, 4, 20, 1105, 1, 46, 104, 999, 1105, 1, 46, 1101, 1000, 1, 20, 4, 20, 1105, 1, 46, 98, 99]  # noqa: E501
    input_buffer_1 = queue.Queue()
    input_buffer_1.put(7)
    output_buffer_1 = queue.Queue()
    ss_1 = Intcode(input_device=input_buffer_1, output_device=output_buffer_1)
    memory = list(original_memory)
    ss_1.load_program(memory)
    ss_1.run_program()
    assert output_buffer_1.get() == 999
    input_buffer_2 = queue.Queue()
    input_buffer_2.put(8)
    output_buffer_2 = queue.Queue()
    ss_2 = Intcode(input_device=input_buffer_2, output_device=output_buffer_2)
    memory = list(original_memory)
    ss_2.load_program(memory)
    ss_2.run_program()
    assert output_buffer_2.get() == 1000
    input_buffer_3 = queue.Queue()
    input_buffer_3.put(9)
    output_buffer_3 = queue.Queue()
    ss_3 = Intcode(input_device=input_buffer_3, output_device=output_buffer_3)
    memory = list(original_memory)
    ss_3.load_program(memory)
    ss_3.run_program()
    assert output_buffer_3.get() == 1001


def test_day_09_example_01():
    program = [109, 1, 204, -1, 1001, 100, 1, 100, 1008, 100, 16, 101, 1006, 101, 0, 99]
    output_buffer = queue.Queue()
    ss = Intcode(output_device=output_buffer)
    ss.load_program(list(program))
    ss.run_program()
    for i in range(len(program)):
        assert output_buffer.get() == program[i]


def test_day_09_example_02():
    program = [1102, 34915192, 34915192, 7, 4, 7, 99, 0]
    output_buffer = queue.Queue()
    ss = Intcode(output_device=output_buffer)
    ss.load_program(program)
    ss.run_program()
    assert len(str(output_buffer.get())) == 16


def test_day_09_example_03():
    program = [104, 1125899906842624, 99]
    output_buffer = queue.Queue()
    ss = Intcode(output_device=output_buffer)
    ss.load_program(program)
    ss.run_program()
    assert output_buffer.get() == program[1]
