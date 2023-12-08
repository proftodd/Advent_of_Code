import { Schematic } from './Schematic'

export default function process(data: string[]): void {
    const schematic = new Schematic(data)
    const sumOfPartNumbers = schematic.parts.reduce((partialSum, p) => partialSum + p, 0)
    console.log(`Sum of part numbers = ${sumOfPartNumbers}`)
}