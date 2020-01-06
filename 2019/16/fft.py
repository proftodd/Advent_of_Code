import sys
from multiprocessing import Pool

BASE_PATTERN = [[0], [1], [0], [-1]]
operator_cache = {}


def get_pattern(sequence_length, position):
    pattern = []
    for p in BASE_PATTERN:
        pattern.extend(p * (position + 1))
    pattern = pattern[1:] + [pattern[0]]
    while len(pattern) < sequence_length:
        pattern.extend(pattern)
    pattern = pattern[:sequence_length]
    return pattern


def get_operators(pattern):
    adders = []
    subtractors = []
    for i in range(len(pattern)):
        if pattern[i] == 1:
            adders.append(i)
        elif pattern[i] == -1:
            subtractors.append(i)
    return adders, subtractors


def calculate_operators(sequence_length):
    global operator_cache
    operator_cache = {d: get_operators(get_pattern(sequence_length, d)) for d in range(sequence_length)}


def calculate_digit(digit, the_sequence):
    this_sum = 0
    adders, subtractors = operator_cache[digit]
    this_sum += sum([the_sequence[p] for p in adders])
    this_sum -= sum([the_sequence[p] for p in subtractors])
    return abs(this_sum) % 10


def fft(sequence, phases):
    return_sequence = list(map(int, list(sequence)))
    calculate_operators(len(sequence))
    with Pool() as p:
        for i in range(phases):
            threads = [p.apply_async(calculate_digit, (j, return_sequence)) for j in range(len(sequence))]
            return_sequence = [r.get() for r in threads]
            if i % 5 == 0:
                print(f"{i} phases completed")
    return ''.join(list(map(str, return_sequence)))


def decode(sequence, phases):
    offset = int(sequence[:7])
    message_end = offset + 8
    signal = ''.join(sequence * 10_000)
    output_signal = fft(signal, phases)
    return output_signal[offset:message_end]


def read_sequence(filename):
    with open(filename, 'r') as fp:
        line = fp.readline()
    sequence = line.strip()
    return sequence


if __name__ == '__main__':
    filename = sys.argv[1]
    phases = int(sys.argv[2])

    sequence = read_sequence(filename)
    output_sequence = fft(sequence, phases)
    print(f"The first 8 digits are {output_sequence[:8]}")
