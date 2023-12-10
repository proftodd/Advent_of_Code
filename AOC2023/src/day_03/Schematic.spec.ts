import { describe, expect, it } from 'vitest'
import { Point, Schematic } from './Schematic'

const testData = [
    '467..114..',
    '...*......',
    '..35..633.',
    '......#...',
    '617*......',
    '.....+.58.',
    '..592.....',
    '......755.',
    '...$.*....',
    '.664.598..'
]

describe('schematic', () => {
    const schematic = new Schematic(testData)
    it('knows its height and width', () => {
        expect(schematic.height).toBe(10)
        expect(schematic.width).toBe(10)
    })

    describe('parts', () => {
        it('recognizes numbers', () => {
            const expected = {
                position: new Point(0, 0),
                length: 3,
                number: 467,
                adjacentCharacters: [
                    { position: new Point(-1, -1), character: '.' },
                    { position: new Point(0, -1), character: '.' },
                    { position: new Point(1, -1), character: '.' },
                    { position: new Point(2, -1), character: '.' },
                    { position: new Point(3, -1), character: '.' },
                    { position: new Point(-1, 0), character: '.' },
                    { position: new Point(0, 0), character: '4' },
                    { position: new Point(1, 0), character: '6' },
                    { position: new Point(2, 0), character: '7' },
                    { position: new Point(3, 0), character: '.' },
                    { position: new Point(-1, 1), character: '.' },
                    { position: new Point(0, 1), character: '.' },
                    { position: new Point(1, 1), character: '.' },
                    { position: new Point(2, 1), character: '.' },
                    { position: new Point(3, 1), character: '*' }]
            }
            // const observed = schematic.numbers[0]
            // expect(observed).toStrictEqual(expected)
            expect(schematic.numbers).toContainEqual(expected)
        })

        it('distinguishes parts from numbers', () => {
            expect(schematic.parts).toContain(467)
            expect(schematic.parts).not.toContain(114)
            expect(schematic.parts).not.toContain(58)
        })

        it('calculates sum of part numbes', () => {
            const sumOfPartNumbers = schematic.parts.reduce((partialSum, p) => partialSum + p, 0)
            expect(sumOfPartNumbers).toBe(4361)
        })
    })

    describe('gears', () => {
        it('finds gears correctly', () => {
            const p1 = new Point(3, 1)
            const p2 = new Point(5, 8)
            const p3 = new Point(3, 4)
            expect(schematic.gears.find(g => g.position.x === p1.x && g.position.y === p1.y)).toStrictEqual({ position: p1, parts: [467, 35] })
            expect(schematic.gears.find(g => g.position.x === p2.x && g.position.y === p2.y)).toStrictEqual({ position: p2, parts: [755, 598] })
            expect(schematic.gears.find(g => g.position.x === p3.x && g.position.y === p3.y)).toBeFalsy()
        })

        it('calculates gear ratio correctly', () => {
            const gearRatios = schematic.gears.map(g => g.parts.reduce((gp, g) => gp * g, 1))
            const gearRatioSum = gearRatios.reduce((grs, gr) => grs + gr, 0)
            expect(gearRatioSum).toBe(467835)
        })
    })
})