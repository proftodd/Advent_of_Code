import { Game, Play } from './Game'

export default function process(data: string[]): void {
    const games = data.filter(line => line != '').map(line => new Game(line))
    const probe = { red: 12, green: 13, blue: 14 }
    const sumOfIds = games
        .filter(g => g.possible(probe))
        .map(g => g.gameNumber)
        .reduce((partialSum, id) => partialSum + id, 0)
    console.log(`Sum of ids = ${sumOfIds}`)

    const sumOfPowers = games
        .map(g => g.power())
        .reduce((partialSum, p) => partialSum + p, 0)
    console.log(`Sum of powers = ${sumOfPowers}`)
}