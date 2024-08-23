type Optional = number | null
type FullMapper = (x: number) => number
type OptionalMapper = (x: number, sx: Optional) => Optional

class NamedFullMapper {
    name: string
    mapper: FullMapper

    constructor(lines: string[]) {
        this.name = lines[0]
        this.mapper = makeCompositeMapper(lines.slice(1))
    }

    map(x: number): number {
        return this.mapper(x)
    }
}

function groupLines(data: string[]): string[][] {
    return data.reduce((acc: string[][], line: string) => {
        if (line == '') {
            return [...acc, []]
        } else {
            let lastGroup: string[] = acc[acc.length - 1]
            lastGroup.push(line)
            return acc
        }
    }, [[]])
    .filter(g => g.length > 0)
}

function simpleMapper(x: number, sx: Optional): Optional {
    if (sx !== null) { return sx }
    if (x >= 50 && x < 98) { return x + 2 }
    return null
}

function identityMapper(x: number, sx: Optional): number {
    if (sx !== null) { return sx }
    return x
}

function makeSimpleMapper(line: string): OptionalMapper {
    const [destinationRangeStart, sourceRangeStart, rangeLength] = line.split(/ +/)
        .map(s => parseInt(s))
    const difference: number = destinationRangeStart - sourceRangeStart
    return (x: number, sx: Optional): Optional => {
        if (sx !== null) { return sx }
        if (x >= sourceRangeStart && x < sourceRangeStart + rangeLength) { return x + difference }
        return null
    }
}

function makeCompositeMapper(lines: string[]): FullMapper {
    const mappers = lines.map(makeSimpleMapper)
    return (x: number): number => {
        const r = mappers.reduce((acc, mapper) => mapper(x, acc), mappers[0](x, null))
        return identityMapper(x, r)
    }
}

function makeTopLevelMapper(groups: string[][]): FullMapper {
    const mappers = groups.slice(1).map(g => new NamedFullMapper(g))
    return (x: number): number =>
        mappers.slice(1).reduce(
            (r, m) => m.map(r),
            mappers[0].map(x)
        )
}

function getSeeds(seedLine: string): number[] {
    return seedLine
        .split(/ +/)
        .slice(1)
        .map(s => parseInt(s))
}

function getSeedRanges(seedLine: string): Array<Array<number>> {
    const seedRanges = []
    const seedEntries = seedLine.split(/ +/).slice(1)
    for (let i = 0; i < seedEntries.length; i += 2) {
        const startingSeed = parseInt(seedEntries[i])
        const seedCount = parseInt(seedEntries[i + 1])
        seedRanges.push([startingSeed, seedCount])
    }
    return seedRanges
}

const arrayRange = (start: number, length: number) =>
    Array.from(
        { length: length },
        (_, index) => start + index
    )

const partitionRange = (start: number, range: number, splits: number) => {
    const partitions: Array<Array<number>> = []
    const width = Math.floor(range / splits)
    for (let i = 0; i < splits - 1; ++i) {
        partitions.push([start + i * width, start + (i + 1) * width - 1])
    }
    partitions.push([start + (splits - 1) * width, start + range - 1])
    return partitions
}

export {
    getSeeds,
    getSeedRanges,
    arrayRange,
    groupLines,
    identityMapper,
    makeCompositeMapper,
    makeSimpleMapper,
    makeTopLevelMapper,
    simpleMapper,
    NamedFullMapper,
    partitionRange
}
