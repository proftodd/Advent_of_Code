import fs from 'fs'
import readline from 'readline'
import greeter from './src/greeter'
import day_01 from './src/day_01/day'

const rl = readline.createInterface({
    input: process.stdin,
    output: process.stdout
})

const getInput = (dayNumber: number): string[] => {
    var filename = `../input/2023/Day_${dayNumber.toString().padStart(2, '0')}/input.txt`
    return fs.readFileSync(filename, 'utf8').split('\n')
}

let puzzleInput: string[]
console.log(greeter(2023))
rl.question('Which day would you like to run? ', s => {
    puzzleInput = getInput(parseInt(s))
    day_01(puzzleInput)
    rl.close()
})
