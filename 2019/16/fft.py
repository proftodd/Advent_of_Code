import sys

BASE_PATTERN = [[0], [1], [0], [-1]]
pattern_cache = {}


def get_pattern(n):
    pattern = []
    for p in BASE_PATTERN:
        pattern.extend(p * n)
    pattern = pattern[1:] + [pattern[0]]
    return pattern


def calculate_digit(digit, sequence, pattern):
    this_sum = 0
    for k in range(digit, len(sequence)):
        index = k % len(pattern)
        if pattern[index]:
            this_sum = this_sum + sequence[k] * pattern[index]
    return abs(this_sum) % 10


def fft(sequence, phases):
    return_sequence = list(map(int, list(sequence)))
    for i in range(phases):
        new_sequence = [0] * len(sequence)
        for j in range(len(sequence) - 1, -1, -1):
            pattern = pattern_cache.get(j + 1, get_pattern(j + 1))
            new_sequence[j] = calculate_digit(j, return_sequence, pattern)
        return_sequence = new_sequence
    return ''.join(list(map(str, return_sequence)))


if __name__ == '__main__':
    filename = sys.argv[1]
    phases = int(sys.argv[2])

    with open(filename, 'r') as fp:
        line = fp.readline()
    sequence = line.strip()
    output_sequence = fft(sequence, phases)
    print(f"The first 8 digits are {output_sequence[:8]}")
