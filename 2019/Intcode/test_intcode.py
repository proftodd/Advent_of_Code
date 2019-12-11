from intcode import (
    POSITION_MODE,
    IMMEDIATE_MODE,
    Stop,
    Addition,
    Multiplication,
    Input,
    Output,
    JumpIfTrue,
    JumpIfFalse,
    LessThan,
    Equals,
    Intcode
)

ss = Intcode()

def test_stop():
    stop = Stop(ss)
    new_ptr = stop.act(0, [])
    assert new_ptr == -1

def test_addition_position_mode():
    modes = [POSITION_MODE] * Addition.expected_parameters()
    adder_position = Addition(ss, modes, [0, 1, 2])
    memory = [1, 2, 0]
    new_ptr = adder_position.act(0, memory)
    assert new_ptr == 4
    assert memory == [1, 2, 3]

def test_addition_immediate_mode():
    modes = [IMMEDIATE_MODE] * Addition.expected_parameters()
    adder_immediate = Addition(ss, modes, [0, 1, 2])
    memory = [1, 2, 0]
    new_ptr = adder_immediate.act(0, memory)
    assert new_ptr == 4
    assert memory == [1, 2, 1]

def test_multiplication_position_mode():
    modes = [POSITION_MODE] * Multiplication.expected_parameters()
    mult_position = Multiplication(ss, modes, [0, 1, 2])
    memory = [1, 2, 0]
    new_ptr = mult_position.act(0, memory)
    assert new_ptr == 4
    assert memory == [1, 2, 2]

def test_multiplication_immediate_mode():
    modes = [IMMEDIATE_MODE] * Multiplication.expected_parameters()
    mult_immediate = Multiplication(ss, modes, [0, 1, 2])
    memory = [2, 3, 0]
    new_ptr = mult_immediate.act(0, memory)
    assert new_ptr == 4
    assert memory == [2, 3, 0]

def test_input():
    ss = Intcode(input_device=['42'])
    input_instruction = Input(ss, [], [0])
    memory = [0]
    new_ptr = input_instruction.act(0, memory)
    assert new_ptr == 2
    assert memory == [42]

def test_output():
    output_buffer = []
    ss = Intcode(output_device=output_buffer)
    output_instruction = Output(ss, [0], [0])
    memory = [69]
    new_ptr = output_instruction.act(0, memory)
    assert new_ptr == 2
    assert output_buffer == [69]

def test_jump_if_true_position():
    modes = [POSITION_MODE] * JumpIfTrue.expected_parameters()
    jump_true_position = JumpIfTrue(ss, modes, [0, 1])
    memory = [1, 2]
    new_ptr = jump_true_position.act(0, memory)
    assert new_ptr == 2
    assert memory == [1, 2]
    memory = [0, 2]
    new_ptr = jump_true_position.act(0, memory)
    assert new_ptr == 3
    assert memory == [0, 2]

def test_jump_if_true_immediate():
    modes = [IMMEDIATE_MODE] * JumpIfTrue.expected_parameters()
    jump_true_immediate = JumpIfTrue(ss, modes, [0, 1])
    memory = [1, 2]
    new_ptr = jump_true_immediate.act(0, memory)
    assert new_ptr == 3
    assert memory == [1, 2]
    memory = [0, 2]
    jump_true_immediate = JumpIfTrue(ss, modes, [1, 1])
    new_ptr = jump_true_immediate.act(0, memory)
    assert new_ptr == 1
    assert memory == [0, 2]

def test_jump_if_false_position():
    modes = [POSITION_MODE] * JumpIfFalse.expected_parameters()
    jump_false_position = JumpIfFalse(ss, modes, [0, 1])
    memory = [1, 2]
    new_ptr = jump_false_position.act(0, memory)
    assert new_ptr == 3
    assert memory == [1, 2]
    memory = [0, 2]
    new_ptr = jump_false_position.act(0, memory)
    assert new_ptr == 2
    assert memory == [0, 2]

def test_jump_if_false_immediate():
    modes = [IMMEDIATE_MODE] * JumpIfFalse.expected_parameters()
    jump_false_immediate = JumpIfFalse(ss, modes, [0, 1])
    memory = [1, 2]
    new_ptr = jump_false_immediate.act(0, memory)
    assert new_ptr == 1
    assert memory == [1, 2]
    memory = [0, 2]
    jump_false_immediate = JumpIfFalse(ss, modes, [1, 1])
    new_ptr = jump_false_immediate.act(0, memory)
    assert new_ptr == 3
    assert memory == [0, 2]

def test_less_than_position():
    modes = [POSITION_MODE] * LessThan.expected_parameters()
    less_than_position = LessThan(ss, modes, [0, 1, 2])
    memory_1 = [0, 2, 5]
    new_ptr = less_than_position.act(0, memory_1)
    assert new_ptr == 4
    assert memory_1 == [0, 2, 1]
    memory_2 = [3, 2, 5]
    new_ptr = less_than_position.act(0, memory_2)
    assert new_ptr == 4
    assert memory_2 == [3, 2, 0]

def test_less_than_immediate():
    modes = [IMMEDIATE_MODE] * LessThan.expected_parameters()
    less_than_immediate_true = LessThan(ss, modes, [0, 1, 2])
    memory_1 = [0, 2, 5]
    new_ptr = less_than_immediate_true.act(0, memory_1)
    assert new_ptr == 4
    assert memory_1 == [0, 2, 1]
    less_than_immediate_false = LessThan(ss, modes, [2, 1, 2])
    memory_2 = [3, 2, 5]
    new_ptr = less_than_immediate_false.act(0, memory_2)
    assert new_ptr == 4
    assert memory_2 == [3, 2, 0]

def test_equal_position():
    modes = [POSITION_MODE] * Equals.expected_parameters()
    equals_position = Equals(ss, modes, [0, 1, 2])
    memory_1 = [2, 2, 5]
    new_ptr = equals_position.act(0, memory_1)
    assert new_ptr == 4
    assert memory_1 == [2, 2, 1]
    memory_2 = [3, 2, 5]
    new_ptr = equals_position.act(0, memory_2)
    assert new_ptr == 4
    assert memory_2 == [3, 2, 0]

def test_equal_immediate():
    modes = [IMMEDIATE_MODE] * Equals.expected_parameters()
    equals_immediate_true = Equals(ss, modes, [0, 0, 2])
    memory_1 = [0, 2, 5]
    new_ptr = equals_immediate_true.act(0, memory_1)
    assert new_ptr == 4
    assert memory_1 == [0, 2, 1]
    equals_immediate_false = Equals(ss, modes, [2, 1, 2])
    memory_2 = [3, 2, 5]
    new_ptr = equals_immediate_false.act(0, memory_2)
    assert new_ptr == 4
    assert memory_2 == [3, 2, 0]

def test_day_2_example_00():
    memory = [99]
    ss.load_memory(memory)
    ss.run_program()
    assert memory == [99]

def test_day_2_example_01():
    memory = [1, 0, 0, 0, 99]
    ss.load_memory(memory)
    ss.run_program()
    assert memory == [2, 0, 0, 0, 99]

def test_day_2_example_02():
    memory = [2, 3, 0, 3, 99]
    ss.load_memory(memory)
    ss.run_program()
    assert memory == [2, 3, 0, 6, 99]

def test_day_2_example_03():
    memory = [2, 4, 4, 5, 99, 0]
    ss.load_memory(memory)
    ss.run_program()
    assert memory == [2, 4, 4, 5, 99, 9801]

def test_day_3_example_04():
    memory = [1, 1, 1, 4, 99, 5, 6, 0, 99]
    ss.load_memory(memory)
    ss.run_program()
    assert memory == [30, 1, 1, 4, 2, 5, 6, 0, 99]

def test_day_4_input():
    ss.read_memory('../02/input.txt')
    ss.run_program()
    assert ss.memory[0] == 394702

def test_day_4_corrected_input():
    fp = open('../02/input.txt')
    line = fp.readline().strip()
    fp.close()
    memory = list(map(int, line.split(',')))
    memory[1] = 12
    memory[2] = 2
    ss.load_memory(memory)
    ss.run_program()
    assert ss.memory[0] == 3850704

def test_day_4_part_2():
    fp = open('../02/input.txt')
    line = fp.readline().strip()
    fp.close()
    memory = list(map(int, line.split(',')))
    memory[1] = 67
    memory[2] = 18
    ss.load_memory(memory)
    ss.run_program()
    assert ss.memory[0] == 19690720

def test_day_5_example_01():
    input_buffer = ['42']
    output_buffer = []
    ss = Intcode(input_buffer, output_buffer)
    memory = [3, 0, 4, 0, 99]
    ss.load_memory(memory)
    ss.run_program()
    assert memory == [42, 0, 4, 0, 99]
    assert input_buffer == []
    assert output_buffer[0] == 42

def test_day_5_example_02():
    input_buffer_1 = ['1']
    output_buffer_1 = []
    ss = Intcode(input_device=input_buffer_1, output_device=output_buffer_1)
    original_memory = [3, 9, 8, 9, 10, 9, 4, 9, 99, -1, 8]
    memory = list(original_memory)
    ss.load_memory(memory)
    ss.run_program()
    assert memory[9] == 0
    assert output_buffer_1 == [0]
    input_buffer_2 = ['8']
    output_buffer_2 = []
    ss = Intcode(input_device=input_buffer_2, output_device=output_buffer_2)
    memory = list(original_memory)
    ss.load_memory(memory)
    ss.run_program()
    assert memory[9] == 1
    assert output_buffer_2 == [1]

def test_day_5_example_03():
    input_buffer_1 = ['1']
    output_buffer_1 = []
    ss = Intcode(input_device=input_buffer_1, output_device=output_buffer_1)
    original_memory = [3, 9, 7, 9, 10, 9, 4, 9, 99, -1, 8]
    memory = list(original_memory)
    ss.load_memory(memory)
    ss.run_program()
    assert memory[9] == 1
    assert output_buffer_1 == [1]
    input_buffer_2 = ['9']
    output_buffer_2 = []
    ss = Intcode(input_device=input_buffer_2, output_device=output_buffer_2)
    memory = list(original_memory)
    ss.load_memory(memory)
    ss.run_program()
    assert memory[9] == 0
    assert output_buffer_2 == [0]

def test_day_5_example_04():
    input_buffer_1 = ['1']
    output_buffer_1 = []
    ss = Intcode(input_device=input_buffer_1, output_device=output_buffer_1)
    original_memory = [3, 3, 1108, -1, 8, 3, 4, 3, 99]
    memory = list(original_memory)
    ss.load_memory(memory)
    ss.run_program()
    assert memory == [3, 3, 1108, 0, 8, 3, 4, 3, 99]
    assert output_buffer_1 == [0]
    input_buffer_2 = ['8']
    output_buffer_2 = []
    ss = Intcode(input_device=input_buffer_2, output_device=output_buffer_2)
    memory = list(original_memory)
    ss.load_memory(memory)
    ss.run_program()
    assert memory == [3, 3, 1108, 1, 8, 3, 4, 3, 99]
    assert output_buffer_2 == [1]

def test_day_5_example_05():
    input_buffer_1 = ['1']
    output_buffer_1 = []
    ss = Intcode(input_device=input_buffer_1, output_device=output_buffer_1)
    original_memory = [3, 3, 1107, -1, 8, 3, 4, 3, 99]
    memory = list(original_memory)
    ss.load_memory(memory)
    ss.run_program()
    assert memory[3] == 1
    assert output_buffer_1 == [1]
    input_buffer_2 = ['9']
    output_buffer_2 = []
    ss = Intcode(input_device=input_buffer_2, output_device=output_buffer_2)
    memory = list(original_memory)
    ss.load_memory(memory)
    ss.run_program()
    assert memory[3] == 0
    assert output_buffer_2 == [0]

def test_day_5_example_06():
    input_buffer_1 = ['1']
    output_buffer_1 = []
    ss = Intcode(input_device=input_buffer_1, output_device=output_buffer_1)
    original_memory = [3, 12, 6, 12, 15, 1, 13, 14, 13, 4, 13, 99, -1, 0, 1, 9]
    memory = list(original_memory)
    ss.load_memory(memory)
    ss.run_program()
    assert memory[12] == 1
    assert output_buffer_1 == [1]
    input_buffer_2 = ['0']
    output_buffer_2 = []
    ss = Intcode(input_device=input_buffer_2, output_device=output_buffer_2)
    memory = list(original_memory)
    ss.load_memory(memory)
    ss.run_program()
    assert memory[12] == 0
    assert output_buffer_2 == [0]

def test_day_5_example_07():
    original_memory = [3, 3, 1105, -1, 9, 1101, 0, 0, 12, 4, 12, 99, 1]
    input_buffer_1 = ['1']
    output_buffer_1 = []
    ss = Intcode(input_device=input_buffer_1, output_device=output_buffer_1)
    memory = list(original_memory)
    ss.load_memory(memory)
    ss.run_program()
    assert memory == [3, 3, 1105, 1, 9, 1101, 0, 0, 12, 4, 12, 99, 1]
    assert output_buffer_1 == [1]
    input_buffer_2 = ['0']
    output_buffer_2 = []
    ss = Intcode(input_device=input_buffer_2, output_device=output_buffer_2)
    memory = list(original_memory)
    ss.load_memory(memory)
    ss.run_program()
    assert memory == [3, 3, 1105, 0, 9, 1101, 0, 0, 12, 4, 12, 99, 0]
    assert output_buffer_2 == [0]


def test_day_5_example_08():
    original_memory = [3, 21, 1008, 21, 8, 20, 1005, 20, 22, 107, 8, 21, 20, 1006, 20, 31, 1106, 0, 36, 98, 0, 0, 1002, 21, 125, 20, 4, 20, 1105, 1, 46, 104, 999, 1105, 1, 46, 1101, 1000, 1, 20, 4, 20, 1105, 1, 46, 98, 99]
    input_buffer_1 = ['7']
    output_buffer_1 = []
    ss_1 = Intcode(input_device=input_buffer_1, output_device=output_buffer_1)
    memory = list(original_memory)
    ss_1.load_memory(memory)
    ss_1.run_program()
    assert output_buffer_1 == [999]
    input_buffer_2 = ['8']
    output_buffer_2 = []
    ss_2 = Intcode(input_device=input_buffer_2, output_device=output_buffer_2)
    memory = list(original_memory)
    ss_2.load_memory(memory)
    ss_2.run_program()
    assert output_buffer_2 == [1000]
    input_buffer_3 = ['9']
    output_buffer_3 = []
    ss_3 = Intcode(input_device=input_buffer_3, output_device=output_buffer_3)
    memory = list(original_memory)
    ss_3.load_memory(memory)
    ss_3.run_program()
    assert output_buffer_3 == [1001]
