import wires

def test_segment_is_vertical():
    p1 = wires.Point(0, 0)
    p2 = wires.Point(1, 0)
    p3 = wires.Point(0, 1)
    s1 = wires.Segment(p1, p2)
    s2 = wires.Segment(p1, p3)
    assert s1.is_horizontal()
    assert s2.is_vertical()
    assert not s1.is_vertical()
    assert not s2.is_horizontal()

def test_manhattan_distance():
    p1 = wires.Point(0, 0)
    p2 = wires.Point(1, 1)
    assert wires.Point.manhattan_distance(p1, p2) == 2

def test_segment_intersection():
    s1 = wires.Segment(wires.Point(-1, 0), wires.Point(1, 0))
    s2 = wires.Segment(wires.Point(0, -1), wires.Point(0, 1))
    s3 = wires.Segment(wires.Point(-1, 1), wires.Point(1, 1))
    s4 = wires.Segment(wires.Point(0, 2), wires.Point(0, 3))
    assert wires.Segment.intersection(s1, s2) == wires.Point(0, 0)
    assert wires.Segment.intersection(s1, s3) == None
    assert wires.Segment.intersection(s1, s4) == None

def test_wire_intersection():
    s1 = wires.Segment(wires.Point(-1, -1), wires.Point(-1, 2))
    s2 = wires.Segment(wires.Point(-1, 2), wires.Point(1, 2))
    s3 = wires.Segment(wires.Point(1, 2), wires.Point(1, -1))
    s4 = wires.Segment(wires.Point(-2, 0), wires.Point(2, 0))
    s5 = wires.Segment(wires.Point(0, -1), wires.Point(0, 0))
    s6 = wires.Segment(wires.Point(0, 0), wires.Point(0, 1))
    p1 = wires.Path()
    p1.add_segment(s1)
    p1.add_segment(s2)
    p1.add_segment(s3)
    p2 = wires.Path()
    p2.add_segment(s4)
    p3 = wires.Path()
    p3.add_segment(s5)
    p3.add_segment(s6)
    i1 = wires.Path.intersection(p1, p2)
    i2 = wires.Path.intersection(p1, p3)
    assert wires.Path.intersection(p1, p2) == [wires.Point(-1, 0), wires.Point(1, 0)]
    assert i2 == []
