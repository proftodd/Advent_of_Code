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

        var elfLoads: [Int: Int] = [:]
        for (elf, elfPack) in elves {
            elfLoads[elf] = elfPack.reduce(0, +)
        }
        let maximumLoad = elfLoads.values.max()!
        print("Maximum load: \(maximumLoad)")
    }
}