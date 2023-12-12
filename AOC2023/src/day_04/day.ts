import { Card, Deck } from './Card'

export default function process(data: string[]): void {
    const nonEmptyLines = data.filter(line => line != '')
    const cards = nonEmptyLines.map(line => new Card(line))
    const totalScore = cards.reduce((partialSum, c) => partialSum + c.score, 0)
    console.log(`Score of cards = ${totalScore}`)
    const deck = new Deck(nonEmptyLines)
    console.log(`Deck score = ${deck.score}`)
}