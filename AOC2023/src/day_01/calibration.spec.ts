import { describe, expect, it } from 'vitest'
import { findCalibrationValue, sumCalibrationValues } from './calibration'

const testData = [
    '1abc2',
    'pqr3stu8vwx',
    'a1b2c3d4e5f',
    'treb7uchet',
    ''
]

describe('calibration', () => {
    describe('findCalibrationValue', () => {
        it('parses first line correctly', () => {
            expect(findCalibrationValue(testData[0])).toBe(12)
        })
    
        it('parses second line correctly', () => {
            expect(findCalibrationValue(testData[1])).toBe(38)
        })
    
        it('parses third line correctly', () => {
            expect(findCalibrationValue(testData[2])).toBe(15)
        })
    
        it('parses fourth line correctly', () => {
            expect(findCalibrationValue(testData[3])).toBe(77)
        })

        it('parses an empty line correctly', () => {
            expect(findCalibrationValue(testData[4])).toBe(0)
        })
    })

    describe('sumCalibrationValues', () => {
        it('sums the list of numbers correctly', () => {
            const numbers = testData.map(findCalibrationValue)
            expect(sumCalibrationValues(numbers)).toBe(142)
        })
    })
})
