import sys
import fft


if __name__ == '__main__':
    filename = sys.argv[1]
    phases = int(sys.argv[2])

    sequence = fft.read_sequence(filename)
    signal = ''.join(sequence * 10_000)
    message = fft.decode(signal, phases)
    print(f"The message is {message}")
