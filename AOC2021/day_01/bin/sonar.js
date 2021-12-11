const scan = (readings) => {
    let previousDepth = parseInt(readings[0])
    // console.log(`initial depth: ${previousDepth}`)
    let depthIncreases = 0

    for(let i = 1; i < readings.length; ++i) {
        var currentDepth = parseInt(readings[i])
        // console.log(`current depth: ${currentDepth}`)
        if (currentDepth > previousDepth) {
            ++depthIncreases
        }
        previousDepth = currentDepth
    }

    return depthIncreases
}

const scanWindow = (readings) => {
    const numericReadings = readings.map(r => parseInt(r))
    let previousWindow = numericReadings.slice(0, 3)
    // console.log(`first window = ${previousWindow}`)
    let depthIncreases = 0

    for (let i = 1; i < readings.length - 2; ++i) {
        var currentWindow = numericReadings.slice(i, i + 3)
        // console.log(`currentWindow = ${currentWindow}`)
        if (currentWindow.reduce((a, b) => a + b, 0) > previousWindow.reduce((a, b) => a + b, 0)) {
            ++depthIncreases
        }
        previousWindow = currentWindow
    }

    return depthIncreases
}

module.exports = { scan, scanWindow }
