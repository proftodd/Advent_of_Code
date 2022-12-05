public struct Day_01 {
    public static func main(lines: [String]) {
        var elves: [Int: [Int]] = [:]
        var currentElf = 1
        elves[currentElf] = []
        for line in lines {
            if (line == "") {
                currentElf += 1
                elves[currentElf] = []
            } else {
                elves[currentElf]!.append(Int(line)!)
            }
        }

        var theLoads = elves.values
            .map { $0.reduce(0, +) }
        theLoads.sort(by: >)

        let maximumLoad = theLoads[0]
        let sumOfThreeLargestLoads = theLoads.prefix(3).reduce(0, +)

        print("Maximum load: \(maximumLoad)")
        print("Sum of three largest loads = \(sumOfThreeLargestLoads)")
    }
}
