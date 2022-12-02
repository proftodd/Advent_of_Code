import Foundation
import Game

enum FileReadError: Error {
    case noPathGiven
    case failedToLoadData
}

@main
public struct Day_02 {
    public static func main() throws {
        var result = ""

        guard let path = UserDefaults.standard.string(forKey: "path") else {
            throw FileReadError.noPathGiven
        }

        let url = URL(fileURLWithPath: path)
        do {
            result = try String(contentsOf: url, encoding: .utf8)
        } catch {
            throw FileReadError.failedToLoadData
        }

        let lines = result.components(separatedBy: .newlines)

        let finalScore = Game.scoreCollectionOfGames(games: lines)
        print("Final score: \(finalScore)")
    }
}
