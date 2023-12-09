class Schematic {
    lines: string[]
    height: number
    width: number
    numbers: { position: Point, length: number, number: number, adjacentCharacters: { position: Point, character: string }[] }[]
    parts: number[]

    constructor(lines: string[]) {
        this.lines = lines
        this.height = lines.length
        this.width = lines[0].length
        let numbers = []
        for (var i = 0; i < this.height; ++i) {
            for (var j = 0; j < this.width; ) {
                if (lines[i][j] >= '0' && lines[i][j] <= '9') {
                    var start = j
                    ++j
                    var length = 1
                    while (lines[i][j] >= '0' && lines[i][j] <= '9') {
                        ++length
                        ++j
                    }
                    const position = new Point(start, i)
                    const number = parseInt(lines[i].substring(start, start + length))
                    // console.log(position)
                    // console.log(length)
                    // console.log(number)
                    // console.log()
                    numbers.push({ position, length, number, adjacentCharacters: [] })
                } else {
                    ++j
                }
            }
        }
        this.numbers = numbers
            .map(n => {
                const adjacentCharacters = []
                for (var i = n.position.y - 1; i <= n.position.y + 1; ++i) {
                    for (var j = n.position.x - 1; j <= n.position.x + n.length; ++j) {
                        if ((i < 0 || i >= this.height) || (j < 0 || j >= this.width)) {
                            adjacentCharacters.push({ position: new Point(j, i), character: '.' })
                        } else {
                            adjacentCharacters.push({ position: new Point(j, i), character: this.lines[i][j] })
                        }
                    }
                }
                // console.log(n.number)
                // console.log(adjacentCharacters)
                // console.log()
                return { ...n, adjacentCharacters }
            })
        this.parts = this.numbers
            .filter(n => n.adjacentCharacters.some(c => c.character != '.' && (c.character < '0' || c.character > '9')))
            .map(n => n.number)
    }
}

class Point {
    x: number
    y: number

    constructor(x: number, y: number) {
        this.x = x
        this.y = y
    }
}

export { Schematic, Point }