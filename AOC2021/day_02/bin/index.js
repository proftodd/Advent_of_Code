const fs = require('fs')
const Submarine = require('./submarine.js')

var filename = '../../input/2021/day_02/input.txt'
var directions = fs.readFileSync(filename, 'utf8').split('\n')
var submarine = new Submarine()
submarine.move(directions)

console.log(`After following directions, submarine checksum is ${submarine.checkSum()}`)
