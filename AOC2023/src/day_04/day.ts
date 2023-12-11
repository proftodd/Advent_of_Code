import { Card } from './Card'

export default function process(data: string[]): void {
    const deck = data.map(line => new Card(line))
    const totalScore = deck.reduce((partialSum, c) => partialSum + c.score, 0)
    console.log(`Deck score = ${totalScore}`)
}