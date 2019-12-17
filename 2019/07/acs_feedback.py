import itertools
import sys
import intcode.intcode

from queue import Queue
from threading import Thread


def try_permutation(program, perm):
    labels = 'ABCDE'
    io_buffers = [Queue()] * len(perm)
    threads = []
    for i in range(len(perm)):
        ss = intcode.intcode.Intcode(io_buffers[i], io_buffers[(i + 1) % len(perm)])
        ss.load_program(list(program))
        io_buffers[i].put(perm[i])
        if i == 0:
            io_buffers[i].put(0)
        threads.append(Thread(target=ss.run_program, name=f"Intcode-{labels[i]}"))
        threads[i].start()

    for t in threads:
        t.join()

    return io_buffers[0].get()


def maximize_thrust(program, settings):
    max_thrust = 0
    best_settings = None
    for perm in itertools.permutations(settings):
        this_thrust = try_permutation(program, perm)
        if this_thrust > max_thrust:
            best_settings = perm
            max_thrust = this_thrust
    return max_thrust, best_settings


def main():
    program = intcode.intcode.Intcode.read_program(sys.argv[1])
    settings = [5, 6, 7, 8, 9]
    max_thrust, best_settings = maximize_thrust(program, settings)
    print(f"maximum thrust of {max_thrust} achieved with {best_settings}")


if __name__ == '__main__':
    main()
