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
        let r = mappers[0](x, null)
        for (let i = 1; i < mappers.length; ++i) {
            r = mappers[i](x, r)
        }
        return identityMapper(x, r)
    }
}

function makeTopLevelMapper(groups: string[][]): FullMapper {
    const mappers = groups.slice(1).map(g => new NamedFullMapper(g))
    return (x: number): number => {
        let r = mappers[0].map(x)
        // console.log(`${mappers[0].name}.map(${x}) = ${r}`)
        for (let i = 1; i < mappers.length; ++i) {
            // const prevR = r
            r = mappers[i].map(r)
            // console.log(`${mappers[i].name}.map(${prevR}) = ${r}`)
        }
        return r
    }
}

function getSeeds(seedLine: string): number[] {
    return seedLine
        .split(/ +/)
        .slice(1)
        .map(s => parseInt(s))
}

export { getSeeds, groupLines, identityMapper, makeCompositeMapper, makeSimpleMapper, makeTopLevelMapper, simpleMapper, NamedFullMapper }
