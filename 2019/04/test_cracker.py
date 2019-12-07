import cracker

def test_cracker():
    assert not cracker.hack('111111')
    assert not cracker.hack('223450')
    assert not cracker.hack('123789')

def test_cracker_extended():
    assert     cracker.hack('112233')
    assert not cracker.hack('123444')
    assert     cracker.hack('111122')
    assert not cracker.hack('122223')