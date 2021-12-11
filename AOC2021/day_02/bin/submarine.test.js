const Submarine = require('./submarine.js')

var directions = 'forward 5,down 5,forward 8,up 3,down 8,forward 2'.split(',')

test('it moves correctly', () => {
    const submarine = new Submarine()
    submarine.move(directions)
    expect(submarine.checkSum()).toBe(150)
})
