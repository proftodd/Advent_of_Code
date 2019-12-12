from intcode import (
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
    stop = Stop(ss, 0, [99])
    new_ptr = stop.act()
    assert new_ptr == -1


def test_addition_position_mode():
    memory = [1, 0, 1, 4, 0]
    adder = Addition(ss, 0, memory)
    new_ptr = adder.act()
    assert new_ptr == 4
    assert memory == [1, 0, 1, 4, 1]


def test_addition_immediate_mode():
    memory = [1101, 1, 2, 4, 0]
    adder_immediate = Addition(ss, 0, memory)
    new_ptr = adder_immediate.act()
    assert new_ptr == 4
    assert memory == [1101, 1, 2, 4, 3]


def test_multiplication_position_mode():
    memory = [2, 0, 2, 4, 0]
    mult_position = Multiplication(ss, 0, memory)
    new_ptr = mult_position.act()
    assert new_ptr == 4
    assert memory == [2, 0, 2, 4, 4]


def test_multiplication_immediate_mode():
    memory = [1102, 2, 2, 4, 0]
    mult_immediate = Multiplication(ss, 0, memory)
    new_ptr = mult_immediate.act()
    assert new_ptr == 4
    assert memory == [1102, 2, 2, 4, 4]


def test_input():
    my_ss = Intcode(input_device=['42'])
    memory = [4, 2, 0]
    input_instruction = Input(my_ss, 0, memory)
    new_ptr = input_instruction.act()
    assert new_ptr == 2
    assert memory == [4, 2, 42]


def test_output_position():
    output_buffer = []
    my_ss = Intcode(output_device=output_buffer)
    memory = [5, 2, 69]
    output_instruction = Output(my_ss, 0, memory)
    new_ptr = output_instruction.act()
    assert new_ptr == 2
    assert output_buffer == [69]


def test_output_immediate():
    output_buffer = []
    my_ss = Intcode(output_device=output_buffer)
    memory = [105, 69]
    output_instruction = Output(my_ss, 0, memory)
    new_ptr = output_instruction.act()
    assert new_ptr == 2
    assert output_buffer == [69]


def test_jump_if_true_position():
    memory = [5, 3, 2, 1]
    jump_true = JumpIfTrue(ss, 0, memory)
    new_ptr = jump_true.act()
    assert new_ptr == 2
    memory = [5, 3, 2, 0]
    jump_false = JumpIfTrue(ss, 0, memory)
    new_ptr = jump_false.act()
    assert new_ptr == 3


def test_jump_if_true_immediate():
    memory = [1105, 1, 2]
    will_true = JumpIfTrue(ss, 0, memory)
    new_ptr = will_true.act()
    assert new_ptr == 2
    memory = [1105, 0, 2]
    wont_jump = JumpIfTrue(ss, 0, memory)
    new_ptr = wont_jump.act()
    assert new_ptr == 3


def test_jump_if_false_position():
    memory = [6, 3, 2, 0]
    will_jump = JumpIfFalse(ss, 0, memory)
    new_ptr = will_jump.act()
    assert new_ptr == 2
    memory = [5, 3, 2, 1]
    wont_jump = JumpIfFalse(ss, 0, memory)
    new_ptr = wont_jump.act()
    assert new_ptr == 3


def test_jump_if_false_immediate():
    memory = [1106, 0, 2]
    will_jump = JumpIfFalse(ss, 0, memory)
    new_ptr = will_jump.act()
    assert new_ptr == 2
    memory = [1106, 1, 2]
    wont_jump = JumpIfFalse(ss, 0, memory)
    new_ptr = wont_jump.act()
    assert new_ptr == 3


def test_less_than_position():
    memory_1 = [7, 0, 2, 4, 0]
    less_than_true = LessThan(ss, 0, memory_1)
    new_ptr = less_than_true.act()
    assert new_ptr == 4
    assert memory_1 == [7, 0, 2, 4, 0]
    memory_2 = [7, 4, 2, 4, 0]
    less_than_false = LessThan(ss, 0, memory_2)
    new_ptr = less_than_false.act()
    assert new_ptr == 4
    assert memory_2 == [7, 4, 2, 4, 1]


def test_less_than_immediate():
    memory_1 = [1107, 0, 2, 4, 0]
    less_than_true = LessThan(ss, 0, memory_1)
    new_ptr = less_than_true.act()
    assert new_ptr == 4
    assert memory_1 == [1107, 0, 2, 4, 1]
    memory_2 = [1107, 3, 2, 4, 0]
    less_than_false = LessThan(ss, 0, memory_2)
    new_ptr = less_than_false.act()
    assert new_ptr == 4
    assert memory_2 == [1107, 3, 2, 4, 0]


def test_equal_position():
    memory_1 = [8, 0, 2, 4, 0]
    less_than_true = Equals(ss, 0, memory_1)
    new_ptr = less_than_true.act()
    assert new_ptr == 4
    assert memory_1 == [8, 0, 2, 4, 0]
    memory_2 = [8, 4, 5, 6, 0, 0, 0]
    less_than_false = Equals(ss, 0, memory_2)
    new_ptr = less_than_false.act()
    assert new_ptr == 4
    assert memory_2 == [8, 4, 5, 6, 0, 0, 1]


def test_equal_immediate():
    memory_1 = [1108, 0, 2, 4, 0]
    less_than_true = Equals(ss, 0, memory_1)
    new_ptr = less_than_true.act()
    assert new_ptr == 4
    assert memory_1 == [1108, 0, 2, 4, 0]
    memory_2 = [1108, 2, 2, 4, 0]
    less_than_false = Equals(ss, 0, memory_2)
    new_ptr = less_than_false.act()
    assert new_ptr == 4
    assert memory_2 == [1108, 2, 2, 4, 1]


def test_day_02_example_00():
    memory = [99]
    ss.load_memory(memory)
    ss.run_program()
    assert memory == [99]


def test_day_02_example_01():
    memory = [1, 0, 0, 0, 99]
    ss.load_memory(memory)
    ss.run_program()
    assert memory == [2, 0, 0, 0, 99]


def test_day_02_example_02():
    memory = [2, 3, 0, 3, 99]
    ss.load_memory(memory)
    ss.run_program()
    assert memory == [2, 3, 0, 6, 99]


def test_day_02_example_03():
    memory = [2, 4, 4, 5, 99, 0]
    ss.load_memory(memory)
    ss.run_program()
    assert memory == [2, 4, 4, 5, 99, 9801]


def test_day_03_example_04():
    memory = [1, 1, 1, 4, 99, 5, 6, 0, 99]
    ss.load_memory(memory)
    ss.run_program()
    assert memory == [30, 1, 1, 4, 2, 5, 6, 0, 99]


def test_day_04_input():
    ss.read_memory('../02/input.txt')
    ss.run_program()
    assert ss.memory[0] == 394702


def test_day_04_corrected_input():
    fp = open('../02/input.txt')
    line = fp.readline().strip()
    fp.close()
    memory = list(map(int, line.split(',')))
    memory[1] = 12
    memory[2] = 2
    ss.load_memory(memory)
    ss.run_program()
    assert ss.memory[0] == 3850704


def test_day_04_part_2():
    fp = open('../02/input.txt')
    line = fp.readline().strip()
    fp.close()
    memory = list(map(int, line.split(',')))
    memory[1] = 67
    memory[2] = 18
    ss.load_memory(memory)
    ss.run_program()
    assert ss.memory[0] == 19690720


def test_day_05_example_01():
    input_buffer = ['42']
    output_buffer = []
    my_ss = Intcode(input_buffer, output_buffer)
    memory = [3, 0, 4, 0, 99]
    my_ss.load_memory(memory)
    my_ss.run_program()
    assert memory == [42, 0, 4, 0, 99]
    assert input_buffer == []
    assert output_buffer[0] == 42


def test_day_05_example_02():
    input_buffer_1 = ['1']
    output_buffer_1 = []
    my_ss = Intcode(input_device=input_buffer_1, output_device=output_buffer_1)
    original_memory = [3, 9, 8, 9, 10, 9, 4, 9, 99, -1, 8]
    memory = list(original_memory)
    my_ss.load_memory(memory)
    my_ss.run_program()
    assert memory[9] == 0
    assert output_buffer_1 == [0]
    input_buffer_2 = ['8']
    output_buffer_2 = []
    my_ss = Intcode(input_device=input_buffer_2, output_device=output_buffer_2)
    memory = list(original_memory)
    my_ss.load_memory(memory)
    my_ss.run_program()
    assert memory[9] == 1
    assert output_buffer_2 == [1]


def test_day_05_example_03():
    input_buffer_1 = ['1']
    output_buffer_1 = []
    my_ss = Intcode(input_device=input_buffer_1, output_device=output_buffer_1)
    original_memory = [3, 9, 7, 9, 10, 9, 4, 9, 99, -1, 8]
    memory = list(original_memory)
    my_ss.load_memory(memory)
    my_ss.run_program()
    assert memory[9] == 1
    assert output_buffer_1 == [1]
    input_buffer_2 = ['9']
    output_buffer_2 = []
    my_ss = Intcode(input_device=input_buffer_2, output_device=output_buffer_2)
    memory = list(original_memory)
    my_ss.load_memory(memory)
    my_ss.run_program()
    assert memory[9] == 0
    assert output_buffer_2 == [0]


def test_day_05_example_04():
    input_buffer_1 = ['1']
    output_buffer_1 = []
    my_ss = Intcode(input_device=input_buffer_1, output_device=output_buffer_1)
    original_memory = [3, 3, 1108, -1, 8, 3, 4, 3, 99]
    memory = list(original_memory)
    my_ss.load_memory(memory)
    my_ss.run_program()
    assert memory == [3, 3, 1108, 0, 8, 3, 4, 3, 99]
    assert output_buffer_1 == [0]
    input_buffer_2 = ['8']
    output_buffer_2 = []
    my_ss = Intcode(input_device=input_buffer_2, output_device=output_buffer_2)
    memory = list(original_memory)
    my_ss.load_memory(memory)
    my_ss.run_program()
    assert memory == [3, 3, 1108, 1, 8, 3, 4, 3, 99]
    assert output_buffer_2 == [1]


def test_day_05_example_05():
    input_buffer_1 = ['1']
    output_buffer_1 = []
    my_ss = Intcode(input_device=input_buffer_1, output_device=output_buffer_1)
    original_memory = [3, 3, 1107, -1, 8, 3, 4, 3, 99]
    memory = list(original_memory)
    my_ss.load_memory(memory)
    my_ss.run_program()
    assert memory[3] == 1
    assert output_buffer_1 == [1]
    input_buffer_2 = ['9']
    output_buffer_2 = []
    my_ss = Intcode(input_device=input_buffer_2, output_device=output_buffer_2)
    memory = list(original_memory)
    my_ss.load_memory(memory)
    my_ss.run_program()
    assert memory[3] == 0
    assert output_buffer_2 == [0]


def test_day_05_example_06():
    input_buffer_1 = ['1']
    output_buffer_1 = []
    my_ss = Intcode(input_device=input_buffer_1, output_device=output_buffer_1)
    original_memory = [3, 12, 6, 12, 15, 1, 13, 14, 13, 4, 13, 99, -1, 0, 1, 9]
    memory = list(original_memory)
    my_ss.load_memory(memory)
    my_ss.run_program()
    assert memory[12] == 1
    assert output_buffer_1 == [1]
    input_buffer_2 = ['0']
    output_buffer_2 = []
    my_ss = Intcode(input_device=input_buffer_2, output_device=output_buffer_2)
    memory = list(original_memory)
    my_ss.load_memory(memory)
    my_ss.run_program()
    assert memory[12] == 0
    assert output_buffer_2 == [0]


def test_day_05_example_07():
    original_memory = [3, 3, 1105, -1, 9, 1101, 0, 0, 12, 4, 12, 99, 1]
    input_buffer_1 = ['1']
    output_buffer_1 = []
    my_ss = Intcode(input_device=input_buffer_1, output_device=output_buffer_1)
    memory = list(original_memory)
    my_ss.load_memory(memory)
    my_ss.run_program()
    assert memory == [3, 3, 1105, 1, 9, 1101, 0, 0, 12, 4, 12, 99, 1]
    assert output_buffer_1 == [1]
    input_buffer_2 = ['0']
    output_buffer_2 = []
    my_ss = Intcode(input_device=input_buffer_2, output_device=output_buffer_2)
    memory = list(original_memory)
    my_ss.load_memory(memory)
    my_ss.run_program()
    assert memory == [3, 3, 1105, 0, 9, 1101, 0, 0, 12, 4, 12, 99, 0]
    assert output_buffer_2 == [0]


def test_day_05_example_08():
    original_memory = [3, 21, 1008, 21, 8, 20, 1005, 20, 22, 107, 8, 21, 20, 1006, 20, 31, 1106, 0, 36, 98, 0, 0, 1002, 21, 125, 20, 4, 20, 1105, 1, 46, 104, 999, 1105, 1, 46, 1101, 1000, 1, 20, 4, 20, 1105, 1, 46, 98, 99]  # noqa: E501
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
