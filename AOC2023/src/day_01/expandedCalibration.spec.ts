import { describe, expect, it } from 'vitest'
import { findCalibrationValue } from './expandedCalibration'

const testData = [
    'two1nine',
    'eightwothree',
    'abcone2threexyz',
    'xtwone3four',
    '4nineeightseven2',
    'zoneight234',
    '7pqrstsixteen',
    ''
]

describe('expanded calibration', () => {
    describe('findCalibrationValue', () => {
        it('parses first line correctly', () => {
            expect(findCalibrationValue(testData[0])).toBe(29)
        })
    
        it('parses second line correctly', () => {
            expect(findCalibrationValue(testData[1])).toBe(83)
        })
    
        it('parses third line correctly', () => {
            expect(findCalibrationValue(testData[2])).toBe(13)
        })
    
        it('parses fourth line correctly', () => {
            expect(findCalibrationValue(testData[3])).toBe(24)
        })

        it('parses fifth line correctly', () => {
            expect(findCalibrationValue(testData[4])).toBe(42)
        })

        it('parses sixth line correctly', () => {
            expect(findCalibrationValue(testData[5])).toBe(14)
        })

        it('parses seventh line correctly', () => {
            expect(findCalibrationValue(testData[6])).toBe(76)
        })

        it('parses an empty line correctly', () => {
            expect(findCalibrationValue(testData[7])).toBe(0)
        })
    })
})