import XCTest

@testable import Day_08

final class Day_08Tests: XCTestCase {
    let lines = [
        "30373",
        "25512",
        "65332",
        "33549",
        "35390",
        ""
    ]

    func testItParsesForestCorrectly() {
        let forest = Forest(lines)
        XCTAssertEqual(forest.height, 5)
        XCTAssertEqual(forest.width, 5)
        XCTAssertEqual(forest[0, 0].height, 3)
        XCTAssertEqual(forest[1, 4].height, 5)
        XCTAssertEqual(forest[4, 1].height, 2)
        XCTAssertEqual(forest[4, 4].height, 0)

        XCTAssertTrue(forest[0, 0].visible)
        XCTAssertTrue(forest[1, 1].visible)
        XCTAssertTrue(forest[2, 1].visible)
        XCTAssertFalse(forest[3, 1].visible)
        XCTAssertTrue(forest[1, 2].visible)
        XCTAssertFalse(forest[2, 2].visible)
        XCTAssertTrue(forest[3, 2].visible)
        XCTAssertFalse(forest[1, 3].visible)
        XCTAssertTrue(forest[2, 3].visible)
        XCTAssertFalse(forest[3, 3].visible)

        XCTAssertEqual(forest.count( { $0.visible }), 21)
    }

    func testGetSightline() {
        let forest = Forest(lines)
        let sl1 = forest.getSightline(coord: (2, 0), direction: getTransform(Direction.north))
        XCTAssertEqual(sl1, [])

        let sl2 = forest.getSightline(coord: (2, 4), direction: getTransform(Direction.north))
        XCTAssertEqual(sl2, [5, 3, 5, 3])

        let sl3 = forest.getSightline(coord: (2, 2), direction: getTransform(Direction.east))
        XCTAssertEqual(sl3, [3, 2])
    }
}