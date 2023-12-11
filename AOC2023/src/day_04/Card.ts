class Card {
    cardNumber: number
    winners: number[]
    numbersYouHave: number[]
    score: number

    constructor(line: string) {
        const [cardNumberPart, numberPart] = line.split(': ')
        const [_, cardNumberString] = cardNumberPart.split(' ')
        const [winnersString, numbersYouHaveString] = numberPart.split('|')
        this.cardNumber = parseInt(cardNumberString)
        this.winners = winnersString.trim().split(/ +/).map(s => parseInt(s))
        this.numbersYouHave = numbersYouHaveString.trim().split(/ +/).map(s => parseInt(s))
        const common = this.numbersYouHave.filter(n => this.winners.some(nn => nn === n))
        this.score = common.length > 0 ? 2 ** (common.length - 1) : 0
    }
}

export { Card }