import { describe, expect, it } from 'vitest'
import { getSeeds, getSeedsFromRanges, groupLines, identityMapper, makeCompositeMapper, makeSimpleMapper, makeTopLevelMapper, simpleMapper, NamedFullMapper } from './Seeds'

const testData = [
    'seeds: 79 14 55 13',
    '',
    'seed-to-soil map:',
    '50 98 2',
    '52 50 48',
    '',
    'soil-to-fertilizer map:',
    '0 15 37',
    '37 52 2',
    '39 0 15',
    '',
    'fertilizer-to-water map:',
    '49 53 8',
    '0 11 42',
    '42 0 7',
    '57 7 4',
    '',
    'water-to-light map:',
    '88 18 7',
    '18 25 70',
    '',
    'light-to-temperature map:',
    '45 77 23',
    '81 45 19',
    '68 64 13',
    '',
    'temperature-to-humidity map:',
    '0 69 1',
    '1 0 69',
    '',
    'humidity-to-location map:',
    '60 56 37',
    '56 93 4',
    ''
]

describe('groupLines', () => {
    it('groups test data correctly', () => {
        const groupedLines = groupLines(testData)
        expect(groupedLines.length).toBe(8)
        expect(groupedLines[0]).toStrictEqual(['seeds: 79 14 55 13'])
        expect(groupedLines[1]).toStrictEqual(['seed-to-soil map:', '50 98 2', '52 50 48'])
        expect(groupedLines[groupedLines.length - 1]).toStrictEqual(['humidity-to-location map:', '60 56 37', '56 93 4',])
    })
})

describe('simple mapper', () => {
    it('returns a value that is passed to it', () => {
        const just = 99
        expect(simpleMapper(1, just)).toStrictEqual(just)
    })

    it('maps a number if it receives null and the number falls in its range', () => {
        expect(simpleMapper(51, null)).toStrictEqual(53)
    })

    it('does not map a number that falls outside its range', () => {
        expect(simpleMapper(1, null)).toStrictEqual(null)
    })
})

describe('identity mapper', () => {
    it('returns a value that is passed to it', () => {
        const just = 99
        expect(simpleMapper(1, just)).toStrictEqual(just)
    })

    it('returns a number if it receives Nothing', () => {
        expect(identityMapper(1, null)).toStrictEqual(1)
    })
})

describe('make simple mapper', () => {
    it('creates a simple mapper that returns required results', () => {
        const myMapper = makeSimpleMapper(testData[3])
        expect(myMapper(1, 48)).toStrictEqual(48)
        expect(myMapper(98, null)).toStrictEqual(50)
        expect(myMapper(1, null)).toStrictEqual(null)
    })
})

describe('NamedFullMapper', () => {
    it('composes simple mappers into composite mappers', () => {
        const namedMapper = new NamedFullMapper(testData.slice(2, 5))
        expect(namedMapper.name).toStrictEqual(testData[2])
        expect(namedMapper.map(1)).toStrictEqual(1)
        expect(namedMapper.map(13)).toStrictEqual(13)
        expect(namedMapper.map(14)).toStrictEqual(14)
        expect(namedMapper.map(50)).toStrictEqual(52)
        expect(namedMapper.map(55)).toStrictEqual(57)
        expect(namedMapper.map(79)).toStrictEqual(81)
        expect(namedMapper.map(98)).toStrictEqual(50)
        expect(namedMapper.map(99)).toStrictEqual(51)
    })
})

describe('TopLevelMapper', () => {
    it('constructs top-level mapper', () => {
        const groups = groupLines(testData)
        const topLevelMapper = makeTopLevelMapper(groups)
        expect(topLevelMapper(79)).toBe(82)
        expect(topLevelMapper(14)).toBe(43)
        expect(topLevelMapper(55)).toBe(86)
        expect(topLevelMapper(13)).toBe(35)
    })
})

describe('getSeeds', () => {
    it('parses seeds correctly', () => {
        expect(getSeeds(testData[0])).toStrictEqual([79, 14, 55, 13])
    })
})

describe('getSeedsFromRanges', () => {
    it('parses seeds correctly', () => {
        expect(getSeedsFromRanges(testData[0]).length).toBe(27)
    })
})