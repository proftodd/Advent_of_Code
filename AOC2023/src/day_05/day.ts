import { getSeeds, groupLines, makeTopLevelMapper } from './Seeds'

export default function process(data: string[]): void {
    const groupedLines = groupLines(data)
    const seeds = getSeeds(groupedLines[0][0])
    const mapper = makeTopLevelMapper(groupedLines)
    console.log(`seeds = ${seeds}`)
    const distances = seeds.map(s => [s, mapper(s)])
    distances.forEach(d => {
        console.log(`${d[0]} => ${d[1]}`)
    })
    const min = distances.reduce((prev, curr) => prev[1] < curr[1] ? prev : curr)
    console.log(`The seed with the minimum distance is ${min[0]} with a distance of ${min[1]}`)
}