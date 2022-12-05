import XCTest

@testable import Day_04

final class Day_04Tests: XCTestCase {
    let lines = [
        "2-4,6-8",
        "2-3,4-5",
        "5-7,7-9",
        "2-8,3-7",
        "6-6,4-6",
        "2-6,4-8",
        ""
    ]

    func testParseLine() {
        let parsedLine = parseLine("2-4,6-8")
        XCTAssertTrue(parsedLine.0.start == 2 && parsedLine.0.end == 4 && parsedLine.1.start == 6 && parsedLine.1.end == 8)
    }

    func testSortIntervals() {
        var sortedIntervals: (Interval, Interval)

        sortedIntervals = sortIntervals(a: Interval(start: 2, end: 4), b: Interval(start: 6, end: 8))
        XCTAssertEqual(sortedIntervals.0, Interval(start: 2, end: 4))
        XCTAssertEqual(sortedIntervals.1, Interval(start: 6, end: 8))

        sortedIntervals = sortIntervals(a: Interval(start: 6, end: 6), b: Interval(start: 4, end: 6))
        XCTAssertEqual(sortedIntervals.0, Interval(start: 4, end: 6))
        XCTAssertEqual(sortedIntervals.1, Interval(start: 6, end: 6))
    }

    func testContains() {
        XCTAssertTrue(Interval(start: 4, end: 6).contains(Interval(start: 6, end: 6)))
        XCTAssertFalse(Interval(start: 2, end: 6).contains(Interval(start: 4, end: 8)))
    }
}