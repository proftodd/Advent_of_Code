import sys
from queue import Queue
from intcode.intcode import Intcode

main_movement_routine = 'B,A,B,C,A,C,A,B,C,A'
movement_function = [
    'R,6,R,8,R,8,L,6,R,8',
    'L,10,L,6,R,10',
    'L,10,R,8,R,8,L,10'
]


def get_ascii_program():
    program_string = main_movement_routine + '\n' + '\n'.join(movement_function) + '\n'
    program_codes = [ord(c) for c in program_string]
    return program_codes


if __name__ == '__main__':
    program = Intcode.read_program(sys.argv[1])
    program[0] = 2
    input_buffer = Queue()
    input_buffer.queue.extend(get_ascii_program())
    input_buffer.queue.extend([ord('n'), ord('\n')])
    output_buffer = Queue()
    ss = Intcode(input_device=input_buffer, output_device=output_buffer)
    ss.load_program(program)
    ss.run_program()
    dust = output_buffer.get()
    print(f"The dust collected = {dust}")
