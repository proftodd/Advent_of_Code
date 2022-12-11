import Foundation

public class Cpu {
    var instructions: [String]

    init(_ instructions: [String]) {
        self.instructions = instructions
    }

    public func execute() -> Int {
        var register = 1
        var cycle = 0
        var signals: [Int] = []
        
        for instruction in instructions {
            switch (instruction) {
                case "noop":
                    cycle += 1
                    // print("cycle = \(cycle), op = \(instruction), register = \(register)")
                    if ((cycle - 20) % 40 == 0) {
                        // print("\tAdding value to signal: \(cycle * register)")
                        signals.append(cycle * register)
                    }
                    break
                case _ where instruction.hasPrefix("addx"):
                    cycle += 1
                    // print("cycle = \(cycle), op = \(instruction), register = \(register)")
                    if ((cycle - 20) % 40 == 0) {
                        // print("\tAdding value to signal: \(cycle * register)")
                        signals.append(cycle * register)
                    }
                    cycle += 1
                    // print("cycle = \(cycle), op = \(instruction), register = \(register)")
                    if ((cycle - 20) % 40 == 0) {
                        // print("\tAdding value to signal: \(cycle * register)")
                        signals.append(cycle * register)
                    }
                    let args = instruction.components(separatedBy: .whitespaces)
                    let addition = Int(args[1])!
                    register += addition
                default:
                    print("Instruction not recognized: [\(instruction)]")
            }

        }

        return signals.reduce(0, +)
    }
}