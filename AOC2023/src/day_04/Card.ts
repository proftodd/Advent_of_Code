class Card {
    cardNumber: number
    winners: number[]
    numbersYouHave: number[]
    winningNumbersYouHave: number[]
    score: number

    constructor(line: string) {
        const [cardNumberPart, numberPart] = line.split(': ')
        const [_, cardNumberString] = cardNumberPart.split(/ +/)
        const [winnersString, numbersYouHaveString] = numberPart.split('|')
        this.cardNumber = parseInt(cardNumberString)
        this.winners = winnersString.trim().split(/ +/).map(s => parseInt(s))
        this.numbersYouHave = numbersYouHaveString.trim().split(/ +/).map(s => parseInt(s))
        this.winningNumbersYouHave = this.numbersYouHave.filter(n => this.winners.some(nn => nn === n))
        this.score = this.winningNumbersYouHave.length > 0 ? 2 ** (this.winningNumbersYouHave.length - 1) : 0
    }
}

class Deck {
    deck: Array<[ number, Card, number ]>
    score: number

    constructor(lines: string[]) {
        this.deck = lines.map(line => new Card(line)).map(c => [c.cardNumber, c, 1 ])
        // console.log(this.deck)
        let score = 0
        for (const [thisCardNumber, thisCard, thisCount] of this.deck) {
            // console.log(`(${thisCardNumber}, ${thisCard}, ${thisCount})`)
            for (let i = 1; i <= thisCard.winningNumbersYouHave.length; ++i) {
                const otherCardIndex = thisCardNumber + i - 1
                const [otherCardNumber, otherCard, otherCount] = this.deck[otherCardIndex]
                const newCount = otherCount + thisCount
                this.deck[otherCardIndex] = [otherCardNumber, otherCard, newCount]
                // console.log(`   (${this.deck[otherCardIndex]})`)
            }
            score += thisCount
        }
        this.score = score
    }
}

export { Card, Deck }