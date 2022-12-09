import Foundation
import Day_01
import Day_02
import Day_03
import Day_04
import Day_05
import Day_06
import Day_07
import Day_08

enum AdventOfCodeError: Error {
    case noCommandGiven
    case commandNotRecognized
    case failedToLoadData
    case commandNotImplemented
}

@main
public struct AOC2022 {
    public static func main() throws {
        let dayRange = 1...25
        let implementedRange = 1...8
        let day = UserDefaults.standard.integer(forKey: "day")

        if !dayRange.contains(day) {
            throw AdventOfCodeError.commandNotRecognized
        }
        if !implementedRange.contains(day) {
            throw AdventOfCodeError.commandNotImplemented
        }

        let path = "../input/2022/Day_\(String(format: "%02d", day))/input.txt"
        let url = URL(fileURLWithPath: path)
        var inputText = ""
        do {
            inputText = try String(contentsOf: url, encoding: .utf8)
        } catch {
            throw AdventOfCodeError.failedToLoadData
        }
        let lines = inputText.components(separatedBy: .newlines)

        switch (day) {
            case 1: Day_01.main(lines: lines)
            case 2: Day_02.main(lines: lines)
            case 3: Day_03.main(lines: lines)
            case 4: Day_04.main(lines: lines)
            case 5: Day_05.main(lines)
            case 6: Day_06.main(lines[0])
            case 7: Day_07.main(lines)
            case 8: Day_08.main(lines)
            default: throw AdventOfCodeError.commandNotImplemented
        }
    }
}
