import XCTest

@testable import Day_06

final class Day_06Tests: XCTestCase {
    let test_01 = "mjqjpqmgbljsphdztnvjfqwrcgsmlb"
    let test_02 = "bvwbjplbgvbhsrlpgdmjqwftvncz"
    let test_03 = "nppdvjthqldpwncqszvftbrmjlhg"
    let test_04 = "nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg"
    let test_05 = "zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw"

    func testDoesAThing() {
        XCTAssertEqual(findMarkerAndPosition(test_01).1, 7)
        XCTAssertEqual(findMarkerAndPosition(test_02).1, 5)
        XCTAssertEqual(findMarkerAndPosition(test_03).1, 6)
        XCTAssertEqual(findMarkerAndPosition(test_04).1, 10)
        XCTAssertEqual(findMarkerAndPosition(test_05).1, 11)
    }
}