from fft import calculate_digit, get_pattern, fft


def test_calculate_digit():
    assert calculate_digit('12345678', [0, 1, 0, -1]) == '4'
    assert calculate_digit('12345678', [0, 0, 1, 1, 0, 0, -1, -1]) == '8'
    assert calculate_digit('12345678', [0, 0, 0, 1, 1, 1, 0, 0, 0, -1, -1, -1]) == '2'
    assert calculate_digit('12345678', get_pattern(4)) == '2'
    assert calculate_digit('12345678', get_pattern(5)) == '6'
    assert calculate_digit('12345678', get_pattern(6)) == '1'
    assert calculate_digit('12345678', get_pattern(7)) == '5'
    assert calculate_digit('12345678', get_pattern(8)) == '8'


def test_example_00():
    input_sequence = '12345678'
    output_sequence_1 = fft(input_sequence, 1)
    output_sequence_2 = fft(input_sequence, 2)
    output_sequence_3 = fft(input_sequence, 3)
    output_sequence_4 = fft(input_sequence, 4)
    assert len(str(input_sequence)) == len(output_sequence_1)
    assert output_sequence_1 == '48226158'
    assert output_sequence_2 == '34040438'
    assert output_sequence_3 == '03415518'
    assert output_sequence_4 == '01029498'


def test_example_01():
    input_sequence = '80871224585914546619083218645595'
    output_sequence = fft(input_sequence, 100)
    assert len(str(input_sequence)) == len(str(output_sequence))
    assert output_sequence[:8] == '24176176'


def test_example_02():
    input_sequence = '19617804207202209144916044189917'
    output_sequence = fft(input_sequence, 100)
    assert len(str(input_sequence)) == len(str(output_sequence))
    assert output_sequence[:8] == '73745418'


def test_example_03():
    input_sequence = '69317163492948606335995924319873'
    output_sequence = fft(input_sequence, 100)
    assert len(str(input_sequence)) == len(str(output_sequence))
    assert output_sequence[:8] == '52432133'
