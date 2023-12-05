class Game {
    gameNumber: number
    plays: Play[]

    constructor(line: string) {
        const [gameString, playString] = line.split(': ')
        const [_, gameNumberString] = gameString.split(' ')
        this.gameNumber = parseInt(gameNumberString)
        this.plays = playString.split('; ').map(ps => new Play(ps))
    }

    possible(draw: Draw): boolean {
        return this.plays.every(p => p.possible(draw))
    }

    power(): number {
        const minimumSet: Draw = this.plays
            .reduce(
                (agg, p) => ({ red: Math.max(agg.red, p.red), green: Math.max(agg.green, p.green), blue: Math.max(agg.blue, p.blue) }),
                { red: 0, green: 0, blue: 0 }
            )
        return  minimumSet.red * minimumSet.green * minimumSet.blue
    }
}

class Play {
    red: number = 0
    green: number = 0
    blue: number = 0

    constructor(playString: string) {
        playString.split(', ').forEach(part => {
            const [colorCount, color] = part.split(' ')
            if (color == 'red') {
                this.red = parseInt(colorCount)
            }
            if (color == 'green') {
                this.green = parseInt(colorCount)
            }
            if (color == 'blue') {
                this.blue = parseInt(colorCount)
            }
        })
    }

    possible(draw: Draw): boolean {
        return this.red <= draw.red
            && this.green <= draw.green
            && this.blue <= draw.blue
    }
}

class Draw {
    red: number
    green: number
    blue: number

    constructor(red: number = 0, green: number = 0, blue: number = 0) {
        this.red = red
        this.green = green
        this.blue = blue
    }
}

export { Game, Play }
