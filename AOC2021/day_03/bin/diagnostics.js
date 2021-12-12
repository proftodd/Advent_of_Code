const run = (readings) => {
    var digitCount = []
    for (let i = 0; i < readings[0].length; ++i) {
        digitCount[i] = [0, 0]
    }
    // console.log(`before scanning readings digitCount[0] = ${digitCount[0]}`)
    // console.log(`before scanning readings digitCount[4] = ${digitCount[4]}`)
    readings.forEach(r => {
        // console.log(`\treading = ${r}`)
        var digits = Array.from(r)
        for (let i = 0; i < digits.length; ++i) {
            if (digits[i] === '1') {
                // console.log(`The ${i} digit is ${digits[i]}, so bumping ${digitCount[i][0]} by one`)
                ++digitCount[i][0]
            }
            if (digits[i] === '0') {
                // console.log(`The ${i} digit is ${digits[i]}, so bumping ${digitCount[i][1]} by one`)
                ++digitCount[i][1]
            }
        }
            // console.log(`\t\tafter scanning one reading digitCount[0] = ${digitCount[0]}`)
            // console.log(`\t\tafter scanning one reading digitCount[4] = ${digitCount[4]}`)
        })

    // console.log(`after scanning readings digitCount[0] = ${digitCount[0]}`)
    // console.log(`after scanning readings digitCount[4] = ${digitCount[4]}`)
    let gamma = ''
    let epsilon = ''
    for (let i = 0; i < digitCount.length; ++i) {
        if (digitCount[i][0] > digitCount[i][1]) {
            gamma += '1'
            epsilon += '0'
        } else {
            gamma += '0'
            epsilon += '1'
        }
    }

    console.log(`gamma = ${gamma}`)
    console.log(`epsilon = ${epsilon}`)
    return parseInt(gamma, 2) * parseInt(epsilon, 2)
}

module.exports = { run }
