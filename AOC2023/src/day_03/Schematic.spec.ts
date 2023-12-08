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

    it('recognizes numbers', () => {
        expect(schematic.numbers).toContainEqual({ position: new Point(0, 0), length: 3, number: 467 })
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