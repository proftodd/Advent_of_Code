import fs from 'fs'
import readline from 'readline'
import greeter from './src/greeter'

const rl = readline.createInterface({
    input: process.stdin,
    output: process.stdout
})

const getInput = (dayString: string): string[] => {
    const filename = `../input/2023/${dayString}/input.txt`
    return fs.readFileSync(filename, 'utf8').split('\n')
}

console.log(greeter(2023))
rl.question('Which day would you like to run? ', async s => {
    const dayString = `Day_${s.padStart(2, '0')}`
    const puzzleInput = getInput(dayString)
    // Reference for dynamic import:
    // https://javascript.info/modules-dynamic-imports
    const module = `./src/${dayString.toLowerCase()}/day`
    const { default: process } = await import(module)
    process(puzzleInput)
    rl.close()
})
