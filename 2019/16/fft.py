import sys

BASE_PATTERN = [[0], [1], [0], [-1]]
pattern_cache = {}


def get_pattern(n):
    pattern = []
    for p in BASE_PATTERN:
        pattern.extend(p * n)
    return pattern


def calculate_digit(sequence, pattern):
    sum = 0
    for i in range(len(sequence)):
        index = (i + 1) % len(pattern)
        if pattern[index]:
            sum = sum + int(sequence[i]) * pattern[index]
    return str(abs(sum) % 10)


def fft(sequence, phases):
    return_sequence = sequence
    for i in range(phases):
        new_sequence = []
        for j in range(len(sequence)):
            pattern = pattern_cache.get(j + 1, get_pattern(j + 1))
            new_sequence.append(calculate_digit(return_sequence, pattern))
        return_sequence = new_sequence
    return ''.join(return_sequence)


if __name__ == '__main__':
    filename = sys.argv[1]
    phases = int(sys.argv[2])

    with open(filename, 'r') as fp:
        line = fp.readline()
    sequence = line.strip()
    output_sequence = fft(sequence, phases)
    print(f"The first 8 digits are {output_sequence[:8]}")
