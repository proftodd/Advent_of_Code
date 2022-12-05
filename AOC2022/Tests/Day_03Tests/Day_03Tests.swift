import XCTest

@testable import Day_03

final class Day_03Tests: XCTestCase {
    func testFindCommonItemInRucksack() {
        XCTAssertEqual(findCommonItems(rucksack: "vJrwpWtwJgWrhcsFMMfFFhFp"), Set("p"))
        XCTAssertEqual(findCommonItems(rucksack: "jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL"), Set("L"))
        XCTAssertEqual(findCommonItems(rucksack: "PmmdzqPrVvPwwTWBwg"), Set("P"))
        XCTAssertEqual(findCommonItems(rucksack: "wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn"), Set("v"))
        XCTAssertEqual(findCommonItems(rucksack: "ttgJtRGJQctTZtZT"), Set("t"))
        XCTAssertEqual(findCommonItems(rucksack: "CrZsJsPPZsGzwwsLwLmpwMDw"), Set("s"))
    }

    func testScoreCharacter() {
        XCTAssertEqual(scoreCharacter(myChar: "a"), 1)
        XCTAssertEqual(scoreCharacter(myChar: "p"), 16)
        XCTAssertEqual(scoreCharacter(myChar: "s"), 19)
        XCTAssertEqual(scoreCharacter(myChar: "t"), 20)
        XCTAssertEqual(scoreCharacter(myChar: "v"), 22)
        XCTAssertEqual(scoreCharacter(myChar: "z"), 26)
        XCTAssertEqual(scoreCharacter(myChar: "A"), 27)
        XCTAssertEqual(scoreCharacter(myChar: "L"), 38)
        XCTAssertEqual(scoreCharacter(myChar: "P"), 42)
        XCTAssertEqual(scoreCharacter(myChar: "Z"), 52)
    }

    let lines = [
        "vJrwpWtwJgWrhcsFMMfFFhFp",
        "jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL",
        "PmmdzqPrVvPwwTWBwg",
        "wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn",
        "ttgJtRGJQctTZtZT",
        "CrZsJsPPZsGzwwsLwLmpwMDw"
    ]

    func testScoreRucksackCollection() {
        XCTAssertEqual(scoreRucksackCollection(lines: lines), 157)
    }
}