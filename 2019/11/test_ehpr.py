import time
from queue import Queue
from threading import Thread
from ehpr import EHPR


def test_ehpr():
    ehpr = EHPR(None, None)
    assert ehpr.scan_camera() == 0
    ehpr.paint(1)
    assert ehpr.scan_camera() == 1
    assert len(ehpr.painted_squares) == 1
    ehpr.turn_and_move(0)
    assert ehpr.x == -1
    assert ehpr.y == 0
    assert ehpr.direction_index == 3
    assert ehpr.scan_camera() == 0
    ehpr.paint(0)
    ehpr.turn_and_move(0)
    assert ehpr.x == -1
    assert ehpr.y == 1
    assert ehpr.direction_index == 2
    ehpr.paint(1)
    ehpr.turn_and_move(0)
    ehpr.paint(1)
    ehpr.turn_and_move(0)
    assert ehpr.x == 0
    assert ehpr.y == 0
    assert ehpr.direction_index == 0
    assert ehpr.scan_camera() == 1
    ehpr.paint(0)
    ehpr.turn_and_move(1)
    ehpr.paint(1)
    ehpr.turn_and_move(0)
    ehpr.paint(1)
    ehpr.turn_and_move(0)
    assert ehpr.x == 0
    assert ehpr.y == -1
    assert ehpr.direction_index == 3
    assert len(ehpr.painted_squares) == 6


def test_ehpr_with_buffers():
    input_buffer = Queue()
    output_buffer = Queue()
    ehpr = EHPR(input_buffer, output_buffer)
    input_buffer.put(1)
    input_buffer.put(0)
    ehpr.act()
    assert output_buffer.get() == 0
    assert ehpr.grid[(0, 0)] == 1
    assert ehpr.x == -1
    assert ehpr.y == 0
    assert ehpr.direction_index == 3
    input_buffer.put(0)
    input_buffer.put(0)
    ehpr.act()
    assert output_buffer.get() == 0
    assert ehpr.grid[(-1, 0)] == 0
    assert ehpr.x == -1
    assert ehpr.y == 1
    assert ehpr.direction_index == 2
    input_buffer.put(1)
    input_buffer.put(0)
    input_buffer.put(1)
    input_buffer.put(0)
    ehpr.act()
    ehpr.act()
    assert ehpr.grid[(-1, 1)] == 1
    assert ehpr.grid[(0, 1)] == 1
    assert ehpr.x == 0
    assert ehpr.y == 0
    assert ehpr.direction_index == 0
    for v in [0, 1, 1, 0, 1, 0]:
        input_buffer.put(v)
    ehpr.act()
    ehpr.act()
    ehpr.act()
    assert ehpr.x == 0
    assert ehpr.y == -1
    assert ehpr.direction_index == 3
    assert ehpr.grid[(1, 0)] == 1
    assert ehpr.grid[(1, -1)] == 1
    assert len(ehpr.painted_squares) == 6
    for i in [0, 0, 1, 0, 0]:
        assert output_buffer.get() == i


def test_ehpr_as_thread():
    input_buffer = Queue()
    output_buffer = Queue()
    ehpr = EHPR(input_buffer, output_buffer)
    input_buffer.put(1)
    input_buffer.put(0)
    t = Thread(target=ehpr.run, name="EHPR")
    t.start()
    time.sleep(2)
    input_buffer.put(0)
    input_buffer.put(0)
    time.sleep(2)
    input_buffer.put(1)
    input_buffer.put(0)
    input_buffer.put(1)
    input_buffer.put(0)
    time.sleep(2)
    for v in [0, 1, 1, 0, 1, 0]:
        input_buffer.put(v)
    time.sleep(2)
    ehpr.should_run = False
    t.join()
    assert ehpr.grid[(0, 0)] == 0
    assert ehpr.grid[(-1, 0)] == 0
    assert ehpr.grid[(-1, 1)] == 1
    assert ehpr.grid[(0, 1)] == 1
    assert ehpr.grid[(1, 0)] == 1
    assert ehpr.grid[(1, -1)] == 1
    assert len(ehpr.painted_squares) == 6
    assert ehpr.x == 0
    assert ehpr.y == -1
    assert ehpr.direction_index == 3
    for i in [0, 0, 0, 0, 1, 0, 0]:
        assert output_buffer.get() == i
