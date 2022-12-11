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

    let lines2 = [
        "R 5",
        "U 8",
        "L 8",
        "D 3",
        "R 17",
        "D 10",
        "L 25",
        "U 20",
        ""
    ]

    func testTupleAdditionAndComparison() {
        XCTAssertEqual(Coordinate(x: 1, y: 1) + Coordinate(x:  2, y: 2), Coordinate(x: 3, y: 3))
        XCTAssertEqual(Coordinate(x: 5, y: 5) + Coordinate(x: -4, y: 1), Coordinate(x: 1, y: 6))
    }

    func testSimulation() {
        let rope = Rope(knotCount: 2, moves: lines)
        rope.simulate()
        let log = rope.tails.last!.logger
        XCTAssertEqual(log.count, 13)

        let tenKnotRope1 = Rope(knotCount: 10, moves: lines)
        tenKnotRope1.simulate()
        let log2 = tenKnotRope1.tails.last!.logger
        XCTAssertEqual(log2.count, 1)

        let tenKnotRope2 = Rope(knotCount: 10, moves: lines2)
        tenKnotRope2.simulate()
        let log3 = tenKnotRope2.tails.last!.logger
        XCTAssertEqual(log3.count, 36)
    }
}