import { Schematic } from './Schematic'

export default function process(data: string[]): void {
    const schematic = new Schematic(data)
    const sumOfPartNumbers = schematic.parts.reduce((partialSum, p) => partialSum + p, 0)
    console.log(`Sum of part numbers = ${sumOfPartNumbers}`)
    const gearRatios = schematic.gears.map(g => g.parts.reduce((gp, g) => gp * g, 1))
    const gearRatioSum = gearRatios.reduce((grs, gr) => grs + gr, 0)
    console.log(`Sum of gear ratios = ${gearRatioSum}`)
}