const fs = require('fs')
const Submarine = require('./submarine.js')

var filename = '../../input/2021/day_02/input.txt'
var directions = fs.readFileSync(filename, 'utf8').split('\n')

var submarine = new Submarine()
submarine.move(directions)

var sub2 = new Submarine()
sub2.enhancedMove(directions)

console.log(`After following directions, submarine checksum is ${submarine.checkSum()}`)
console.log(`After following directions in enhanced move, submarine checksum is ${sub2.checkSum()}`)
