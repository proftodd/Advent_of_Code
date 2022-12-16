import Foundation

public class Cpu {
    var instructions: [String]

    init(_ instructions: [String]) {
        self.instructions = instructions
    }

    public func execute() -> (Int, String) {
        var x = 1
        var cycle = 0
        var index = 0
        var signals: [Int] = []
        var arg = ""
        var screen = ""

        while (index < instructions.count) {
            let cursor = cycle % 40
            cycle += 1
            // print("cycle = \(cycle), x = \(x)")
            if ((cycle - 20) % 40 == 0) {
                let value = cycle * x
                // print("\tAdding value to signals: \(value)")
                signals.append(value)
            }

            // print("during cycle \(cycle), cursor is at position \(cursor) and x is at position \(x)")
            if (abs(x - cursor) < 2) {
                screen += "#"
            } else {
                screen += "."
            }
            if (cursor == 39) {
                screen += "\n"
            }

            if (arg == "") {
                let instruction = instructions[index]
                switch (instruction) {
                    case "noop":
                        // print("\tnoop: taking no action")
                        break
                    case _ where (instruction.hasPrefix("addx")):
                        let args = instruction.components(separatedBy: .whitespaces)
                        arg = args[1]
                        // print("\taddx: setting arg to \(arg), and taking no further action")
                    default:
                        print("Instruction not recognized: \(instruction)")
                }
                index += 1
            } else {
                x += Int(arg)!
                // print("\t x is now \(x)")
                arg = ""
            }
        }

        if (screen.hasSuffix("\n.")) {
            screen = String(screen.dropLast(2))
        }

        return (signals.reduce(0, +), screen)
    }
}