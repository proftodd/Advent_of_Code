import cracker

def test_cracker():
    assert     cracker.hack('111111')
    assert not cracker.hack('223450')
    assert not cracker.hack('123789')
