function scan(readings) {
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

exports.scan = scan