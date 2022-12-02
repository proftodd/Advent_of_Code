import XCTest
import Game

@testable import Day_02

final class Day_02Tests: XCTestCase {
    func testParseOpponentPlays() {
        XCTAssertEqual(Game.opponentPlay(label: "A"), Game.Play.Rock)
        XCTAssertEqual(Game.opponentPlay(label: "B"), Game.Play.Paper)
        XCTAssertEqual(Game.opponentPlay(label: "C"), Game.Play.Scissors)
        XCTAssertEqual(Game.opponentPlay(label: "Q"), nil)
    }

    func testParseMyPlays() {
        XCTAssertEqual(Game.myPlay(label: "X"), Game.Play.Rock)
        XCTAssertEqual(Game.myPlay(label: "Y"), Game.Play.Paper)
        XCTAssertEqual(Game.myPlay(label: "Z"), Game.Play.Scissors)
        XCTAssertEqual(Game.myPlay(label: "Q"), nil)
    }

    func testDetermineWinner() {
        XCTAssertEqual(Game.determineWinner(opponent: Game.Play.Rock, me: Game.Play.Rock), 3)
        XCTAssertEqual(Game.determineWinner(opponent: Game.Play.Rock, me: Game.Play.Paper), 6)
        XCTAssertEqual(Game.determineWinner(opponent: Game.Play.Rock, me: Game.Play.Scissors), 0)
        XCTAssertEqual(Game.determineWinner(opponent: Game.Play.Paper, me: Game.Play.Rock), 0)
        XCTAssertEqual(Game.determineWinner(opponent: Game.Play.Paper, me: Game.Play.Paper), 3)
        XCTAssertEqual(Game.determineWinner(opponent: Game.Play.Paper, me: Game.Play.Scissors), 6)
        XCTAssertEqual(Game.determineWinner(opponent: Game.Play.Scissors, me: Game.Play.Rock), 6)
        XCTAssertEqual(Game.determineWinner(opponent: Game.Play.Scissors, me: Game.Play.Paper), 0)
        XCTAssertEqual(Game.determineWinner(opponent: Game.Play.Scissors, me: Game.Play.Scissors), 3)
    }

    func testScoreGame() {
        XCTAssertEqual(Game.scoreGame(opponent: "A", me: "Y"), 8)
        XCTAssertEqual(Game.scoreGame(opponent: "B", me: "X"), 1)
        XCTAssertEqual(Game.scoreGame(opponent: "C", me: "Z"), 6)
    }

    func testScoreGameWithStrategy() {
        XCTAssertEqual(Game.scoreGameWithStrategy(opponent: "A", me: "Y"), 4)
        XCTAssertEqual(Game.scoreGameWithStrategy(opponent: "B", me: "X"), 1)
        XCTAssertEqual(Game.scoreGameWithStrategy(opponent: "C", me: "Z"), 7)
    }

    let setOfGames = [
        "A Y",
        "B X",
        "C Z",
        ""
    ]

    func testScoreCollectionOfGames() {
        XCTAssertEqual(Game.scoreCollectionOfGames(games: setOfGames), 15)
    }

    func testScoreCollectionOfGamesWithStrategy() {
        XCTAssertEqual(Game.scoreCollectionOfGamesWithStrategy(games: setOfGames), 12)
    }
}