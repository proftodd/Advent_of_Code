import sys

def hack(guess):
    str_guess = str(guess)
    repeats = 0
    prev_digit = int(str_guess[0])
    for i in range(1, len(str_guess)):
        this_digit = int(str_guess[i])
        if this_digit < prev_digit:
            return False
        if this_digit == prev_digit:
            repeats = repeats + 1
        prev_digit = this_digit
    return repeats > 0

def main():
    min, max, *_ = sys.argv[1:]
    possible = [i for i in range(int(min), int(max) + 1) if hack(i)]
    print(f"{len(possible)} possible passwords in [{min}, {max}]")

if __name__ == '__main__':
    main()
