class Submarine {
    constructor() {
        this.type = "Submarine"
        this.horizontal = 0
        this.depth = 0
        this.aim = 0
    }

    move(directions) {
        directions.forEach(d => {
            const [direction, value] = d.split(' ')
            if (direction === 'forward') {
                this.horizontal += parseInt(value)
            }
            if (direction === 'down') {
                this.depth += parseInt(value)
            }
            if (direction === 'up') {
                this.depth -= parseInt(value)
            }
        })
    }

    enhancedMove(directions) {
        directions.forEach(d => {
            const [direction, valueString] = d.split(' ')
            const value = parseInt(valueString)
            if (direction === 'forward') {
                this.horizontal += value
                this.depth += value * this.aim
            }
            if (direction === 'down') {
                this.aim += value
            }
            if (direction === 'up') {
                this.aim -= value
            }
        })
    }

    checkSum() {
        return this.horizontal * this.depth
    }
}

module.exports = Submarine
