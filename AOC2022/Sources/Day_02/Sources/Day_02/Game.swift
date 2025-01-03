import Foundation

public enum Play: Int {
    case Rock = 1
    case Paper
    case Scissors
}

public func opponentPlay(label: Character) -> Play? {
    switch label {
        case "A": return Play.Rock
        case "B": return Play.Paper
        case "C": return Play.Scissors
        default: return nil
    }
}

public func myPlay(label: Character) -> Play? {
    switch label  {
        case "X": return Play.Rock
        case "Y": return Play.Paper
        case "Z": return Play.Scissors
        default: return nil
    }
}

public func determineWinner(opponent: Play, me: Play) -> Int {
    switch(opponent) {
        case .Rock:
            switch(me) {
                case .Rock: return 3
                case .Paper: return 6
                case .Scissors: return 0
            }
        case .Paper:
            switch(me) {
                case .Rock: return 0
                case .Paper: return 3
                case .Scissors: return 6
            }
        case .Scissors:
            switch(me) {
                case .Rock: return 6
                case .Scissors: return 3
                case .Paper: return 0
            }
    }
}

public func scoreGame(opponent: Character, me: Character) -> Int {
    let opponentPlay = opponentPlay(label: opponent)!
    let myPlay = myPlay(label: me)!
    let gameScore = determineWinner(opponent: opponentPlay, me: myPlay)
    return gameScore + myPlay.rawValue
}

public func determineMyPlay(opponentPlay: Play, myOutcome: Character) -> Play? {
    switch myOutcome {
        case "X":
            switch opponentPlay {
                case .Rock: return Play.Scissors
                case .Paper: return Play.Rock
                case .Scissors: return Play.Paper
            }
        case "Y":
            return opponentPlay
        case "Z":
            switch opponentPlay {
                case .Rock: return Play.Paper
                case .Paper: return Play.Scissors
                case .Scissors: return Play.Rock
            }
        default: return nil
    }
}

public func scoreGameWithStrategy(opponent: Character, me: Character) -> Int {
    let opponentPlay = opponentPlay(label: opponent)!
    let myPlay = determineMyPlay(opponentPlay: opponentPlay, myOutcome: me)!
    let gameScore = determineWinner(opponent: opponentPlay, me: myPlay)
    return gameScore + myPlay.rawValue
}

public func scoreCollectionOfGames(games: [String], scoringMethod: (Character, Character) -> Int) -> Int {
    return games
        .filter { $0 != "" }
        .map { $0.components(separatedBy: .whitespaces) }
        .map { (Character($0[0]), Character($0[1])) }
        .map { scoringMethod($0.0, $0.1) }
        .reduce(0, +)
}
