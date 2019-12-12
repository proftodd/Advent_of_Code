import sys
import intcode.intcode

def main():
    settings = [0, 1, 2, 3, 4]
    program = sys.argv[1]
    input_buffers = [
        [settings[1]],
        [settings[0]],
        [settings[4]],
        [settings[3]],
        [settings[2]]
    ]
    output_buffers = [[], [], [], [], []]
    ss = []
    value = 0
    for i in range(5):
        ss.append(intcode.intcode.Intcode(input_device=input_buffers[i], output_device=output_buffers[i]))
        ss[i].read_memory(program)
        input_buffers[i].append(value)
        ss[i].run_program()
        value = str(output_buffers[i][0])
    print(output_buffers[4][0])


if __name__ == '__main__':
    main()
