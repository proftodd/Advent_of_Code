import Foundation

public struct Instruction: Equatable {
    static let capturePattern = #"move (\d+) from (\d+) to (\d+)"#
    static let captureRegex = try! NSRegularExpression(
        pattern: capturePattern,
        options: []
    )
    var number: Int
    var from: Int
    var to: Int

    init(number: Int, from: Int, to: Int) {
        self.number = number
        self.from = from
        self.to = to
    }

    init(_ line: String) {
        let lineRange = NSRange(line.startIndex..<line.endIndex, in: line)
        let matches = Instruction.captureRegex.matches(in: line, options: [], range: lineRange)
        guard let match = matches.first else {
            number = 0
            from = 0
            to = 0
            return
        }
        var theMatches: [String] = []
        for rangeIndex in 0..<match.numberOfRanges {
            let matchRange = match.range(at: rangeIndex)
            if matchRange == lineRange { continue }
            if let substringRange = Range(matchRange, in: line) {
                let capture = String(line[substringRange])
                theMatches.append(capture)
            }
        }
        number = Int(theMatches[0])!
        from = Int(theMatches[1])!
        to = Int(theMatches[2])!
    }
}

public class Beach {
    var stacks: [[Character]]
    var instructions: [Instruction]

    init(_ lines: [String]) {
        let(initialState, instructions) = Beach.parseSetup(lines)

        self.instructions = instructions.map { Instruction($0) }

        let stackLabelString = initialState[initialState.count - 1]
        let stackCount = (stackLabelString.count + 1) / 4
        self.stacks = Array(repeating: [], count: stackCount)
        for i in stride(from: initialState.count - 2, through: 0, by: -1) {
            let level = initialState[i]
            for j in 0..<stackCount {
                let crate = level[level.index(level.startIndex, offsetBy: 4 * j + 1)]
                if crate != " " {
                    self.stacks[j].append(crate)
                }
            }
        }
    }

    public static func parseSetup(_ lines: [String]) -> ([String], [String]) {
        var indexOfFirstEmptyLine = 0
        while (lines[indexOfFirstEmptyLine] != "") {
            indexOfFirstEmptyLine += 1
        }

        let initialState = lines.prefix(indexOfFirstEmptyLine).filter { $0 != "" }
        let instructions = lines[indexOfFirstEmptyLine + 1..<lines.count].filter { $0 != "" }

        return (initialState, instructions)
    }

    public func getStack(_ stackNumber: Int) -> [Character] {
        return self.stacks[stackNumber - 1]
    }

    public func getInstructions() -> [Instruction] {
        return self.instructions
    }

    func moveNCrates(from: Int, to: Int, number: Int = 1) {
        let pile = Array(self.stacks[from - 1].suffix(number))
        self.stacks[from - 1] = Array(self.stacks[from - 1].dropLast(number))
        self.stacks[to - 1] += pile
        // print("Moved \(pile) from \(from) to \(to)")
    }

    public func followInstructions() {
        for instruction in self.instructions {
            for _ in 1...instruction.number {
                self.moveNCrates(from: instruction.from, to: instruction.to)
            }
            // for s in self.stacks {
            //     print(s)
            // }
            // print("")
        }
    }

    public func followInstructionsWithModernCrane() {
        for instruction in self.instructions {
            self.moveNCrates(from: instruction.from, to: instruction.to, number: instruction.number)
            // for s in self.stacks {
            //     print(s)
            // }
            // print("")
        }
    }

    public func getTopOfAllStacks() -> String {
        var theTops: [Character] = []
        for i in 0..<stacks.count {
            theTops.append(stacks[i][stacks[i].count - 1])
        }
        return String(theTops)
    }
}