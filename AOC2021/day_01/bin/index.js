const fs = require('fs')
const sonar = require('./sonar.js')

var filename = '../../input/2021/day_01/input.txt'
var readings = fs.readFileSync(filename, 'utf8').split('\n')
var depthIncreases = sonar.scan(readings)
console.log(`Depth increases: ${depthIncreases}`)