public struct Day_02 {
    public static func main(lines: [String]) {
        let finalScore = scoreCollectionOfGames(games: lines, scoringMethod: scoreGame)
        let finalScoreWithStrategy = scoreCollectionOfGames(games: lines, scoringMethod: scoreGameWithStrategy)
        print("Final score: \(finalScore)")
        print("Score with strategy: \(finalScoreWithStrategy)")
    }
}