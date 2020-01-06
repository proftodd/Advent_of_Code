from fft import calculate_digit, get_pattern, decode
import fft


def test_get_pattern():
    assert get_pattern(8, 0) == [1, 0, -1, 0, 1, 0, -1, 0]
    assert get_pattern(8, 1) == [0, 1, 1, 0, 0, -1, -1, 0]
    assert get_pattern(8, 2) == [0, 0, 1, 1, 1, 0, 0, 0]
    assert get_pattern(8, 3) == [0, 0, 0, 1, 1, 1, 1, 0]
    assert get_pattern(8, 4) == [0, 0, 0, 0, 1, 1, 1, 1]
    assert get_pattern(8, 5) == [0, 0, 0, 0, 0, 1, 1, 1]
    assert get_pattern(8, 6) == [0, 0, 0, 0, 0, 0, 1, 1]
    assert get_pattern(8, 7) == [0, 0, 0, 0, 0, 0, 0, 1]


def test_get_operators():
    assert fft.get_operators(get_pattern(8, 0)) == ([0, 4], [2, 6])
    assert fft.get_operators(get_pattern(8, 1)) == ([1, 2], [5, 6])
    assert fft.get_operators(get_pattern(8, 2)) == ([2, 3, 4], [])
    assert fft.get_operators(get_pattern(8, 3)) == ([3, 4, 5, 6], [])
    assert fft.get_operators(get_pattern(8, 4)) == ([4, 5, 6, 7], [])
    assert fft.get_operators(get_pattern(8, 5)) == ([5, 6, 7], [])
    assert fft.get_operators(get_pattern(8, 6)) == ([6, 7], [])
    assert fft.get_operators(get_pattern(8, 7)) == ([7], [])


def test_calculate_digit():
    input_sequence = list(map(int, list('12345678')))
    fft.calculate_operators(len(input_sequence))
    assert calculate_digit(0, input_sequence) == 4
    assert calculate_digit(1, input_sequence) == 8
    assert calculate_digit(2, input_sequence) == 2
    assert calculate_digit(3, input_sequence) == 2
    assert calculate_digit(4, input_sequence) == 6
    assert calculate_digit(5, input_sequence) == 1
    assert calculate_digit(6, input_sequence) == 5
    assert calculate_digit(7, input_sequence) == 8


def test_example_00():
    input_sequence = '12345678'
    output_sequence_1 = fft.fft(input_sequence, 1)
    output_sequence_2 = fft.fft(input_sequence, 2)
    output_sequence_3 = fft.fft(input_sequence, 3)
    output_sequence_4 = fft.fft(input_sequence, 4)
    assert len(str(input_sequence)) == len(output_sequence_1)
    assert output_sequence_1 == '48226158'
    assert output_sequence_2 == '34040438'
    assert output_sequence_3 == '03415518'
    assert output_sequence_4 == '01029498'


def test_example_01():
    input_sequence = '80871224585914546619083218645595'
    output_sequence = fft.fft(input_sequence, 100)
    assert len(str(input_sequence)) == len(str(output_sequence))
    assert output_sequence[:8] == '24176176'


def test_example_02():
    input_sequence = '19617804207202209144916044189917'
    output_sequence = fft.fft(input_sequence, 100)
    assert len(str(input_sequence)) == len(str(output_sequence))
    assert output_sequence[:8] == '73745418'


def test_example_03():
    input_sequence = '69317163492948606335995924319873'
    output_sequence = fft.fft(input_sequence, 100)
    assert len(str(input_sequence)) == len(str(output_sequence))
    assert output_sequence[:8] == '52432133'


def test_decode_01():
    sequence = '03036732577212944063491565474664'
    signal = ''.join(sequence * 10_000)
    assert decode(signal, 100) == '84462026'


def test_decode_02():
    sequence = '02935109699940807407585447034323'
    signal = ''.join(sequence * 10_000)
    assert decode(signal, 100) == '78725270'


def test_decode_03():
    sequence = '03081770884921959731165446850517'
    signal = ''.join(sequence * 10_000)
    assert decode(signal, 100) == '53553731'
