import XCTest

@testable import Day_09

final class Day_09Tests: XCTestCase {
    let lines = [
        "R 4",
        "U 4",
        "L 3",
        "D 1",
        "R 4",
        "D 1",
        "L 5",
        "R 2",
        ""
    ]

    func testTupleAdditionAndComparison() {
        XCTAssertEqual(Coordinate(x: 1, y: 1) + Coordinate(x:  2, y: 2), Coordinate(x: 3, y: 3))
        XCTAssertEqual(Coordinate(x: 5, y: 5) + Coordinate(x: -4, y: 1), Coordinate(x: 1, y: 6))
    }

    func testSimulation() {
        let rope = Rope(lines)
        rope.simulate()
        let log = rope.tail.logger
        XCTAssertEqual(log.count, 13)
    }
}