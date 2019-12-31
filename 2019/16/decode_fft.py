import sys
from fft import fft


if __name__ == '__main__':
    filename = sys.argv[1]
    phases = int(sys.argv[2])

    with open(sys.argv[1], 'r') as fp:
        line = fp.readline()
    sequence = line.strip()
    offset = int(sequence[:7])
    message_end = offset + 8
    print(f"The message offset is {offset}")
    print(f"The message ends at {message_end}")
    signal = ''.join(sequence * 10_000)
    output_signal = fft(signal, phases)
    print(f"The message {output_signal[offset:message_end]}")
