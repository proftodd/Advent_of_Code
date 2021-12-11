class Submarine {
    constructor() {
        this.type = "Submarine"
        this.horizontal = 0
        this.depth = 0
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

    checkSum() {
        return this.horizontal * this.depth
    }
}

module.exports = Submarine
