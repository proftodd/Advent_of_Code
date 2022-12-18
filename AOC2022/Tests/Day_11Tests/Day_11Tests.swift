import XCTest

@testable import Day_11

final class Day_11Tests: XCTestCase {
    let lines = [
        "Monkey 0:",
        "  Starting items: 79, 98",
        "  Operation: new = old * 19",
        "  Test: divisible by 23",
        "    If true: throw to monkey 2",
        "    If false: throw to monkey 3",
        "",
        "Monkey 1:",
        "  Starting items: 54, 65, 75, 74",
        "  Operation: new = old + 6",
        "  Test: divisible by 19",
        "    If true: throw to monkey 2",
        "    If false: throw to monkey 0",
        "",
        "Monkey 2:",
        "  Starting items: 79, 60, 97",
        "  Operation: new = old * old",
        "  Test: divisible by 13",
        "    If true: throw to monkey 1",
        "    If false: throw to monkey 3",
        "",
        "Monkey 3:",
        "  Starting items: 74",
        "  Operation: new = old + 3",
        "  Test: divisible by 17",
        "    If true: throw to monkey 0",
        "    If false: throw to monkey 1",
        ""
    ]
    
    func testItParsesMonkeysFromNotes() {
        let barrel = Barrel(lines)
        XCTAssertEqual(4, barrel.monkeys.count)
    }

    func testAct() {
        let barrel = Barrel(lines)
        for _ in 1...20 {
            barrel.act()
        }
        XCTAssertEqual(10605, barrel.monkeyBusiness())
    }
}
