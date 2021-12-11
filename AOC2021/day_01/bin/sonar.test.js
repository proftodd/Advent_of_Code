const sonar = require('./sonar.js')

var readings = '199,200,208,210,200,207,240,269,260,263'.split(',')

test('it scans depth correctly', () => {
    var depthIncreases = sonar.scan(readings)
    expect(depthIncreases).toBe(7)
})

test('it scans windows correctly', () => {
    var depthIncreases = sonar.scanWindow(readings)
    expect(depthIncreases).toBe(5)
})