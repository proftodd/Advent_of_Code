import XCTest

@testable import Day_06

final class Day_06Tests: XCTestCase {
    let test_01 = "mjqjpqmgbljsphdztnvjfqwrcgsmlb"
    let test_02 = "bvwbjplbgvbhsrlpgdmjqwftvncz"
    let test_03 = "nppdvjthqldpwncqszvftbrmjlhg"
    let test_04 = "nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg"
    let test_05 = "zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw"

    func testDoesAThing() {
        XCTAssertEqual(findMarkerOfSize(signal: test_01, size: 4), 7)
        XCTAssertEqual(findMarkerOfSize(signal: test_02, size: 4), 5)
        XCTAssertEqual(findMarkerOfSize(signal: test_03, size: 4), 6)
        XCTAssertEqual(findMarkerOfSize(signal: test_04, size: 4), 10)
        XCTAssertEqual(findMarkerOfSize(signal: test_05, size: 4), 11)

        XCTAssertEqual(findMarkerOfSize(signal: test_01, size: 14), 19)
        XCTAssertEqual(findMarkerOfSize(signal: test_02, size: 14), 23)
        XCTAssertEqual(findMarkerOfSize(signal: test_03, size: 14), 23)
        XCTAssertEqual(findMarkerOfSize(signal: test_04, size: 14), 29)
        XCTAssertEqual(findMarkerOfSize(signal: test_05, size: 14), 26)
    }
}