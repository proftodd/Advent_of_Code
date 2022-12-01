import Foundation

enum FileReadError: Error {
    case noPathGiven
    case failedToLoadData
}

@main
public struct Day_01 {
    public static func main() throws {
        var result = ""

        guard let path = UserDefaults.standard.string(forKey: "path") else {
            throw FileReadError.noPathGiven
        }

        let url = URL(fileURLWithPath: path)
        do {
            result = try String(contentsOf: url, encoding: .utf8)
        } catch {
            throw FileReadError.failedToLoadData
        }

        let lines = result.components(separatedBy: .newlines)
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
