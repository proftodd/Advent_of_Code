export function findCalibrationValue(line: string): number {
    var d1: number = 0
    var d2: number = 0
    for (var i = 0; i < line.length; ++i) {
        if (line[i] >= '0' && line[i] <= '9') {
            d1 = parseInt(line[i])
            break
        }
    }
    for (var i = line.length - 1; i >= 0; --i) {
        if (line[i] >= '0' && line[i] <= '9') {
            d2 = parseInt(line[i])
            break
        }
    }
    return 10 * d1 + d2
}

export function sumCalibrationValues(values: number[]): number {
    return values.reduce((partialSum, a) => partialSum + a, 0)
}