import XCTest

@testable import Day_05

final class Day_05Tests: XCTestCase {
    let lines = [
        "    [D]    ",
        "[N] [C]    ",
        "[Z] [M] [P]",
        " 1   2   3 ",
        "",
        "move 1 from 2 to 1",
        "move 3 from 1 to 3",
        "move 2 from 2 to 1",
        "move 1 from 1 to 2",
        ""
    ]

    func testParseSetup() {
        let (initialState, instructions) = Beach.parseSetup(lines)
        XCTAssertEqual(initialState[0], lines[0])
        XCTAssertEqual(initialState[2], lines[2])
        XCTAssertEqual(initialState[3], lines[3])
        XCTAssertEqual(instructions[0], lines[5])
        XCTAssertEqual(instructions[instructions.count - 1], lines[lines.count - 2])
    }

    func testBeachInit() {
        let beach = Beach(lines)
        XCTAssertEqual(beach.getStack(1), ["Z", "N"])
        XCTAssertEqual(beach.getStack(2), ["M", "C", "D"])
        XCTAssertEqual(beach.getStack(3), ["P"])
        XCTAssertEqual(beach.getInstructions()[0], Instruction(number: 1, from: 2, to: 1))
        XCTAssertEqual(beach.getInstructions()[1], Instruction(number: 3, from: 1, to: 3))
        XCTAssertEqual(beach.getInstructions()[2], Instruction(number: 2, from: 2, to: 1))
        XCTAssertEqual(beach.getInstructions()[3], Instruction(number: 1, from: 1, to: 2))
    }

    func testInstructionInit() {
        XCTAssertEqual(Instruction(lines[5]), Instruction(number: 1, from: 2, to: 1))
        XCTAssertEqual(Instruction(lines[6]), Instruction(number: 3, from: 1, to: 3))
        XCTAssertEqual(Instruction(lines[7]), Instruction(number: 2, from: 2, to: 1))
        XCTAssertEqual(Instruction(lines[8]), Instruction(number: 1, from: 1, to: 2))
    }

    func testDirectionsAndTop() {
        let beach = Beach(lines)
        beach.followInstructions()
        XCTAssertEqual(beach.getTopOfAllStacks(), "CMZ")
    }
}