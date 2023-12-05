import { Game, Play } from './Game'

export default function process(data: string[]): void {
    const probe = { red: 12, green: 13, blue: 14 }
    const sumOfIds = data
        .map(line => new Game(line))
        .filter(g => g.possible(probe))
        .map(g => g.gameNumber)
        .reduce((partialSum, id) => partialSum + id, 0)
    console.log(`Sum of ids = ${sumOfIds}`)
}