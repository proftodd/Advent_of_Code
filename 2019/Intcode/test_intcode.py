from intcode import (
    POSITION_MODE,
    IMMEDIATE_MODE,
    Stop,
    Addition,
    Multiplication,
    JumpIfTrue,
    JumpIfFalse,
    LessThan,
    Equals,
    Intcode
)

def test_stop():
    stop = Stop()
    new_ptr = stop.act(0, [])
    assert new_ptr == -1

def test_addition_position_mode():
    modes = [POSITION_MODE] * Addition.expected_parameters()
    adder_position = Addition(modes, [0, 1, 2])
    memory = [1, 2, 0]
    new_ptr = adder_position.act(0, memory)
    assert new_ptr == 4
    assert memory == [1, 2, 3]

def test_addition_immediate_mode():
    modes = [IMMEDIATE_MODE] * Addition.expected_parameters()
    adder_immediate = Addition(modes, [0, 1, 2])
    memory = [1, 2, 0]
    new_ptr = adder_immediate.act(0, memory)
    assert new_ptr == 4
    assert memory == [1, 2, 1]

def test_multiplication_position_mode():
    modes = [POSITION_MODE] * Multiplication.expected_parameters()
    mult_position = Multiplication(modes, [0, 1, 2])
    memory = [1, 2, 0]
    new_ptr = mult_position.act(0, memory)
    assert new_ptr == 4
    assert memory == [1, 2, 2]

def test_multiplication_immediate_mode():
    modes = [IMMEDIATE_MODE] * Multiplication.expected_parameters()
    mult_immediate = Multiplication(modes, [0, 1, 2])
    memory = [2, 3, 0]
    new_ptr = mult_immediate.act(0, memory)
    assert new_ptr == 4
    assert memory == [2, 3, 0]

def test_jump_if_true_position():
    modes = [POSITION_MODE] * JumpIfTrue.expected_parameters()
    jump_true_position = JumpIfTrue(modes, [0, 1])
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
    jump_true_immediate = JumpIfTrue(modes, [0, 1])
    memory = [1, 2]
    new_ptr = jump_true_immediate.act(0, memory)
    assert new_ptr == 3
    assert memory == [1, 2]
    memory = [0, 2]
    jump_true_immediate = JumpIfTrue(modes, [1, 1])
    new_ptr = jump_true_immediate.act(0, memory)
    assert new_ptr == 1
    assert memory == [0, 2]

def test_jump_if_false_position():
    modes = [POSITION_MODE] * JumpIfFalse.expected_parameters()
    jump_false_position = JumpIfFalse(modes, [0, 1])
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
    jump_false_immediate = JumpIfFalse(modes, [0, 1])
    memory = [1, 2]
    new_ptr = jump_false_immediate.act(0, memory)
    assert new_ptr == 1
    assert memory == [1, 2]
    memory = [0, 2]
    jump_false_immediate = JumpIfFalse(modes, [1, 1])
    new_ptr = jump_false_immediate.act(0, memory)
    assert new_ptr == 3
    assert memory == [0, 2]

def test_less_than_position():
    modes = [POSITION_MODE] * LessThan.expected_parameters()
    less_than_position = LessThan(modes, [0, 1, 2])
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
    less_than_immediate_true = LessThan(modes, [0, 1, 2])
    memory_1 = [0, 2, 5]
    new_ptr = less_than_immediate_true.act(0, memory_1)
    assert new_ptr == 4
    assert memory_1 == [0, 2, 1]
    less_than_immediate_false = LessThan(modes, [2, 1, 2])
    memory_2 = [3, 2, 5]
    new_ptr = less_than_immediate_false.act(0, memory_2)
    assert new_ptr == 4
    assert memory_2 == [3, 2, 0]

def test_equal_position():
    modes = [POSITION_MODE] * Equals.expected_parameters()
    equals_position = Equals(modes, [0, 1, 2])
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
    equals_immediate_true = Equals(modes, [0, 0, 2])
    memory_1 = [0, 2, 5]
    new_ptr = equals_immediate_true.act(0, memory_1)
    assert new_ptr == 4
    assert memory_1 == [0, 2, 1]
    equals_immediate_false = Equals(modes, [2, 1, 2])
    memory_2 = [3, 2, 5]
    new_ptr = equals_immediate_false.act(0, memory_2)
    assert new_ptr == 4
    assert memory_2 == [3, 2, 0]

def test_day_2_example_00():
    ss = Intcode()
    memory = [99]
    ss.load_memory(memory)
    ss.run_program()
    assert memory == [99]

def test_day_2_example_01():
    ss = Intcode()
    memory = [1, 0, 0, 0, 99]
    ss.load_memory(memory)
    ss.run_program()
    assert memory == [2, 0, 0, 0, 99]

def test_day_2_example_02():
    ss = Intcode()
    memory = [2, 3, 0, 3, 99]
    ss.load_memory(memory)
    ss.run_program()
    assert memory == [2, 3, 0, 6, 99]

def test_day_2_example_03():
    ss = Intcode()
    memory = [2, 4, 4, 5, 99, 0]
    ss.load_memory(memory)
    ss.run_program()
    assert memory == [2, 4, 4, 5, 99, 9801]

def test_day_3_example_04():
    ss = Intcode()
    memory = [1, 1, 1, 4, 99, 5, 6, 0, 99]
    ss.load_memory(memory)
    ss.run_program()
    assert memory == [30, 1, 1, 4, 2, 5, 6, 0, 99]

def test_day_4_input():
    ss = Intcode()
    ss.read_memory('../02/input.txt')
    ss.run_program()
    assert ss.memory[0] == 394702

def test_day_4_corrected_input():
    ss = Intcode()
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
    ss = Intcode()
    fp = open('../02/input.txt')
    line = fp.readline().strip()
    fp.close()
    memory = list(map(int, line.split(',')))
    memory[1] = 67
    memory[2] = 18
    ss.load_memory(memory)
    ss.run_program()
    assert ss.memory[0] == 19690720
