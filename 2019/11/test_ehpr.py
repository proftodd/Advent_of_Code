from ehpr import EHPR


def test_ehpr():
    ehpr = EHPR()
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