import Foundation

func gcd(_ x: Int, _ y: Int) -> Int {
    var a = 0
    var b = max(x, y)
    var r = min(x, y)

    while r != 0 {
        a = b
        b = r
        r = a % b
    }

    return b
}

func calcLcm(_ x: Int, _ y: Int) -> Int {
    return x / gcd(x, y) * y
}

public class Barrel {
    public var monkeys: [Monkey]
    public lazy var lcm = monkeys.map { $0.divisor }.reduce(1, calcLcm)

    init(_ lines: [String], worryModerator: Int?) {
        monkeys = []
        for i in 0..<lines.count / 7 {
            let monkey = Monkey(barrel: self, notes: Array(lines[i * 7...i * 7 + 6]), worryModerator: worryModerator)
            self.monkeys.append(monkey)
        }
    }

    public func act(_ reps: Int) {
        for _ in 1...reps {
            monkeys.forEach { $0.act() }
        }
    }

    public func report(_ round: Int) -> String {
        return """
        == After round \(round) ==
        Monkey \(monkeys[0].number) inspected items \(monkeys[0].itemsHandled) times
        Monkey \(monkeys[1].number) inspected items \(monkeys[1].itemsHandled) times
        Monkey \(monkeys[2].number) inspected items \(monkeys[2].itemsHandled) times
        Monkey \(monkeys[3].number) inspected items \(monkeys[3].itemsHandled) times 
        """
    }

    public func monkeyBusiness() -> Int {
        return monkeys
            .sorted { $0.itemsHandled > $1.itemsHandled }
            .prefix(2)
            .map { $0.itemsHandled }
            .reduce(1, *)
        // let sortedMonkeys = monkeys
        //     .sorted { $0.itemsHandled > $1.itemsHandled }
        //     .prefix(2)
        //     .map { $0.itemsHandled }
        // print("Most active monkey: \(sortedMonkeys[0])")
        // print("Next most active monkey: \(sortedMonkeys[1])")
        // return sortedMonkeys.reduce(1, *)
    }
}

public class Monkey {
    var barrel: Barrel
    var number: Int
    var items: [Int]
    var _divisor: Int
    var divisor: Int { get { return _divisor } }
    var operation: (Int) -> Int
    var test: (Int) -> Bool
    var trueTarget: Int
    var falseTarget: Int
    var _itemsHandled: Int
    var itemsHandled: Int { get { return _itemsHandled } }
    var worryModerator: Int?

    init(barrel: Barrel, notes: [String], worryModerator: Int?) {
        self.barrel = barrel

        self.number = Monkey.getNumber(notes[0])
        // print("Parsing monkey \(self.number)")

        self.items = Monkey.getItems(notes[1])
        // print("\tStarting items: \(self.items)")

        let (operation, argument) = Monkey.getOperation(notes[2])
        switch(operation) {
            case "+": self.operation = argument == "old" ? { $0 + $0 } : { $0 + Int(argument)! }
            case "*": self.operation = argument == "old" ? { $0 * $0 } : { $0 * Int(argument)! }
            default: self.operation = { _ in 0 }
        }
        // print("\tOperation: new = old \(operation) \(argument)")

        let aDivisor = Monkey.getTestArgument(notes[3])
        self._divisor = aDivisor
        self.test = { $0 % aDivisor == 0 }
        // print("\tTest: divisible by \(testArgument)")

        self.trueTarget = Monkey.getTarget(notes[4])
        self.falseTarget = Monkey.getTarget(notes[5])
        // print("\tActions:")
        // print("\t\ttrue target = \(self.trueTarget)")
        // print("\t\tfalse target = \(self.falseTarget)")

        _itemsHandled = 0
        self.worryModerator = worryModerator
    }

    public static func getNumber(_ line: String) -> Int {
        do {
            let regex = try NSRegularExpression(pattern: "Monkey (\\d*):")
            let matches = regex.matches(in: line, options: [], range: NSRange(location: 0, length: line.count))
            if let match = matches.first {
                let range = match.range(at: 1)
                if let numberRange = Range(range, in: line) {
                    let number = line[numberRange]
                    // print("Parsing monkey \(number)")
                    return Int(number)!
                }
            }
            print("line \(line) did not match regex \(regex)")
            return -1
        } catch {
            print("Error in regular expression")
            return -1
        }
    }

    public static func getItems(_ line: String) -> [Int] {
        var ret: [Int] = []
        do {
            let regex = try NSRegularExpression(pattern: "  Starting items: ((\\d*, )*\\d*)")
            let matches = regex.matches(in: line, options: [], range: NSRange(location: 0, length: line.count))
            if let match = matches.first {
                let range = match.range(at: 1)
                if let numbersRange = Range(range, in: line) {
                    let numbers = line[numbersRange]
                    let numberStrings = numbers.components(separatedBy: ", ")
                    ret += numberStrings.map { Int($0)! }
                }
                return ret
            }
            print("line \(line) did not match regex \(regex)")
            return ret
        } catch {
            print("Error in regular expression")
            return ret
        }
    }

    public static func getOperation(_ line: String) -> (String, String) {
        do {
            let regex = try NSRegularExpression(pattern: "Operation: new = old (.) (.*)")
            let matches = regex.matches(in: line, options: [], range: NSRange(location: 0, length: line.count))
            if let match = matches.first {
                var operation: String
                var argument: String
                let operationRange = match.range(at: 1)
                if let operationInnerRange = Range(operationRange, in: line) {
                    operation = String(line[operationInnerRange])
                } else {
                    operation = ""
                }
                let argumentRange = match.range(at: 2)
                if let argumentInnerRange = Range(argumentRange, in: line) {
                    argument = String(line[argumentInnerRange])
                } else {
                    argument = ""
                }
                return (operation, argument)
            }
            print("line \(line) did not match regex \(regex)")
            return ("", "")
        } catch {
            print("Error in regular expression")
            return ("", "")
        }
    }

    public static func getTestArgument(_ line: String) -> Int {
        do {
            let regex = try NSRegularExpression(pattern: "  Test: divisible by (\\d*)")
            let matches = regex.matches(in: line, options: [], range: NSRange(location: 0, length: line.count))
            if let match = matches.first {
                let range = match.range(at: 1)
                if let numberRange = Range(range, in: line) {
                    let number = line[numberRange]
                    // print("target is \(number)")
                    return Int(number)!
                }
            }
            print("line \(line) did not match regex \(regex)")
            return -1
        } catch {
            print("Error in regular expression")
            return -1
        }
    }

    public static func getTarget(_ line: String) -> Int {
        do {
            let regex = try NSRegularExpression(pattern: "    If .*: throw to monkey (\\d*)")
            let matches = regex.matches(in: line, options: [], range: NSRange(location: 0, length: line.count))
            if let match = matches.first {
                let range = match.range(at: 1)
                if let numberRange = Range(range, in: line) {
                    let number = line[numberRange]
                    // print("target is \(number)")
                    return Int(number)!
                }
            }
            print("line \(line) did not match regex \(regex)")
            return -1
        } catch {
            print("Error in regular expression")
            return -1
        }
    }

    public func act() {
        _itemsHandled += items.count
        while items.count > 0 {
            var item = items.removeFirst()
            item = operation(item)
            if self.worryModerator == nil {
                item %= self.barrel.lcm
            } else {
                item = item / self.worryModerator!
            }
            if test(item) {
                barrel.monkeys[trueTarget].addItem(item)
            } else {
                barrel.monkeys[falseTarget].addItem(item)
            }
        }
    }

    public func addItem(_ item: Int) {
        items.append(item)
    }
}