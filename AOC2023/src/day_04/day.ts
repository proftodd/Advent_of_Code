import { Card, Deck } from './Card'

export default function process(data: string[]): void {
    const cards = data.map(line => new Card(line))
    const totalScore = cards.reduce((partialSum, c) => partialSum + c.score, 0)
    console.log(`Score of cards = ${totalScore}`)
    const deck = new Deck(data)
    console.log(`Deck score = ${deck.score}`)
}