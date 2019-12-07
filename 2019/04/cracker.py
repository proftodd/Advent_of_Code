import sys

def hack(guess):
    repeats = {}
    prev_digit = -1
    for this_digit in map(int, str(guess)):
        if this_digit < prev_digit:
            return False
        if this_digit == prev_digit:
            if this_digit not in repeats:
                repeats[this_digit] = 1
            repeats[this_digit] = repeats[this_digit] + 1
        prev_digit = this_digit
    return any(v == 2 for v in repeats.values())

def main():
    min, max, *_ = sys.argv[1:]
    possible = [i for i in range(int(min), int(max) + 1) if hack(i)]
    print(f"{len(possible)} possible passwords in [{min}, {max}]")

if __name__ == '__main__':
    main()
