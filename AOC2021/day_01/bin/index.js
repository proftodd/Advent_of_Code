const fs = require('fs')
const sonar = require('./sonar.js')

var filename = '../../input/2021/day_01/input.txt'
var readings = fs.readFileSync(filename, 'utf8').split('\n')
var noisyDepthIncreases = sonar.scan(readings)
var windowDepthIncreaes = sonar.scanWindow(readings)
console.log(`Noisy depth increases: ${noisyDepthIncreases}`)
console.log(`Window depth increases: ${windowDepthIncreaes}`)