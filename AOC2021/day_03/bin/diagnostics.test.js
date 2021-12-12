const diagnostics = require('./diagnostics.js')

var readings = '00100,11110,10110,10111,10101,01111,00111,11100,10000,11001,00010,01010'.split(',')

test('it calculates gamma and epsilon correctly', () => {
    var powerConsumption = diagnostics.run(readings)
    expect(powerConsumption).toBe(198)
})