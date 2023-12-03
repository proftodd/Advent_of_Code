import { describe, expect, it } from 'vitest'
import hello from './greeter'

describe('greeter', () => {
    it('passes with default arg', () => {
        expect(hello()).toBe('Welcome to Advent of Code 2023!')
    })
    it('passes with provided arg', () => {
        const year = 1492;
        expect(hello(year)).toBe('Welcome to Advent of Code 1492!')
    })
})