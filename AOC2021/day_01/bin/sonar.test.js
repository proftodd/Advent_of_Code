const sonar = require('./sonar.js')

test('it scans depth correctly', () => {
    var readings = '199,200,208,210,200,207,240,269,260,263'.split(',')
    var depthIncreases = sonar.scan(readings)
    expect(depthIncreases).toBe(7)
})