const fs = require('fs')
const diagnostics = require('./diagnostics.js')

var filename = '../../input/2021/day_03/input.txt'
var readings = fs.readFileSync(filename, 'utf8').split('\n')
var powerConsumption = diagnostics.run(readings)
console.log(`power consumption = ${powerConsumption}`)
