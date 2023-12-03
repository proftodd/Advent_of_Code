const spelled = [
    { spelling: 'one', length: 3, value: 1 },
    { spelling: 'two', length: 3, value: 2 },
    { spelling: 'three', length: 5, value: 3 },
    { spelling: 'four', length: 4, value: 4 },
    { spelling: 'five', length: 4, value: 5 },
    { spelling: 'six', length: 3, value: 6 },
    { spelling: 'seven', length: 5, value: 7 },
    { spelling: 'eight', length: 5, value: 8 },
    { spelling: 'nine', length: 4, value: 9 },
]

function matchBySpelling(line: string, startingIndex: number): number {
    for (var i = 0; i < spelled.length; ++i) {
        if (line.substring(startingIndex, startingIndex + spelled[i].length) == spelled[i].spelling) {
            return spelled[i].value
        }
    }
    return -1
}

export function findCalibrationValue(line: string): number {
    var d1 = 0
    var d2 = 0
    for (var i = 0; i < line.length; ++i) {
        if (line[i] >= '0' && line[i] <= '9') {
            d1 = parseInt(line[i])
            break
        } else if ((d1 = matchBySpelling(line, i)) > -1) {
            break
        }
    }
    for (var i = line.length - 1; i >= 0; --i) {
        if (line[i] >= '0' && line[i] <= '9') {
            d2 = parseInt(line[i])
            break;
        } else if ((d2 = matchBySpelling(line, i)) > -1) {
            break
        }
    }
    return 10 * d1 + d2
}