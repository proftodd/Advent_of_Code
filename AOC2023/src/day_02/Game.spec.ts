import { describe, expect, it } from 'vitest'
import { Game, Play } from './Game'

const testData = [
    'Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green',
    'Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue',
    'Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red',
    'Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red',
    'Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green'
]

describe('play', () => {
    it('can be constructed', () => {
        const testPlay = new Play('3 blue, 4 red')
        expect(testPlay.red).toBe(4)
        expect(testPlay.green).toBe(0)
        expect(testPlay.blue).toBe(3)
    })

    it('determines possible plays', () => {
        const testPlay = new Play('3 blue, 4 red')
        expect(testPlay.possible({ red: 12, green: 13, blue: 14 })).toBe(true)
    })

    it('determines impossible plays', () => {
        const testPlay = new Play('8 green, 6 blue, 20 red')
        expect(testPlay.possible({ red: 12, green: 13, blue: 14 })).toBe(false)
    })
})

describe('game', () => {
    it('can be constructed', () => {
        const testGame = new Game('Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green')
        expect(testGame.gameNumber).toBe(1)
        expect(testGame.plays.length).toBe(3)
    })

    it('determines possible games', () => {
        const testGame = new Game('Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green')
        expect(testGame.possible({ red: 12, green: 13, blue: 14 })).toBe(true)
    })

    it('determines impossible games', () => {
        const testGame = new Game('Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red')
        expect(testGame.possible({ red: 12, green: 13, blue: 14 })).toBe(false)
    })
})

describe('driver', () => {
    it('correctly processes test data', () => {
        const sumOfIds = testData
            .map(d => new Game(d))
            .filter(g => g.possible({ red: 12, green: 13, blue: 14 }))
            .map(g => g.gameNumber)
            .reduce((partialSum, id) => partialSum + id, 0)
        expect(sumOfIds).toBe(8)
    })
})