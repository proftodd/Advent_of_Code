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

        XCTAssertEqual(forest[2, 1].scenicScore, 4)
        XCTAssertEqual(forest[2, 3].scenicScore, 8)

        XCTAssertEqual(forest.max( { $0.scenicScore }), 8)
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

    func testGetScenicScore() {
        let forest = Forest(lines)
        let t1 = forest[2, 3]
        let sl1 = forest.getSightline(coord: t1.coordinates, direction: getTransform(Direction.south))
        let tti1 = sl1.firstIndex(where: { $0 >= t1.height }) ?? sl1.count - 1
        let v1 = sl1.prefix(tti1 + 1)
        XCTAssertEqual(v1, [3])

        let sl2 = forest.getSightline(coord: t1.coordinates, direction: getTransform(Direction.north))
        let tti2 = sl2.firstIndex(where: { $0 >= t1.height }) ?? sl2.count - 1
        let v2 = sl2.prefix(tti2 + 1)
        XCTAssertEqual(v2, [3, 5])
    }
}