import XCTest

@testable import Day_02

final class Day_02Tests: XCTestCase {
    func testParseOpponentPlays() {
        XCTAssertEqual(opponentPlay(label: "A"), Play.Rock)
        XCTAssertEqual(opponentPlay(label: "B"), Play.Paper)
        XCTAssertEqual(opponentPlay(label: "C"), Play.Scissors)
        XCTAssertEqual(opponentPlay(label: "Q"), nil)
    }

    func testParseMyPlays() {
        XCTAssertEqual(myPlay(label: "X"), Play.Rock)
        XCTAssertEqual(myPlay(label: "Y"), Play.Paper)
        XCTAssertEqual(myPlay(label: "Z"), Play.Scissors)
        XCTAssertEqual(myPlay(label: "Q"), nil)
    }

    func testDetermineWinner() {
        XCTAssertEqual(determineWinner(opponent: Play.Rock, me: Play.Rock), 3)
        XCTAssertEqual(determineWinner(opponent: Play.Rock, me: Play.Paper), 6)
        XCTAssertEqual(determineWinner(opponent: Play.Rock, me: Play.Scissors), 0)
        XCTAssertEqual(determineWinner(opponent: Play.Paper, me: Play.Rock), 0)
        XCTAssertEqual(determineWinner(opponent: Play.Paper, me: Play.Paper), 3)
        XCTAssertEqual(determineWinner(opponent: Play.Paper, me: Play.Scissors), 6)
        XCTAssertEqual(determineWinner(opponent: Play.Scissors, me: Play.Rock), 6)
        XCTAssertEqual(determineWinner(opponent: Play.Scissors, me: Play.Paper), 0)
        XCTAssertEqual(determineWinner(opponent: Play.Scissors, me: Play.Scissors), 3)
    }

    func testScoreGame() {
        XCTAssertEqual(scoreGame(opponent: "A", me: "Y"), 8)
        XCTAssertEqual(scoreGame(opponent: "B", me: "X"), 1)
        XCTAssertEqual(scoreGame(opponent: "C", me: "Z"), 6)
    }

    func testScoreGameWithStrategy() {
        XCTAssertEqual(scoreGameWithStrategy(opponent: "A", me: "Y"), 4)
        XCTAssertEqual(scoreGameWithStrategy(opponent: "B", me: "X"), 1)
        XCTAssertEqual(scoreGameWithStrategy(opponent: "C", me: "Z"), 7)
    }

    let setOfGames = [
        "A Y",
        "B X",
        "C Z",
        ""
    ]

    func testScoreCollectionOfGames() {
        XCTAssertEqual(scoreCollectionOfGames(games: setOfGames, scoringMethod: scoreGame), 15)
    }

    func testScoreCollectionOfGamesWithStrategy() {
        XCTAssertEqual(scoreCollectionOfGames(games: setOfGames, scoringMethod: scoreGameWithStrategy), 12)
    }
}