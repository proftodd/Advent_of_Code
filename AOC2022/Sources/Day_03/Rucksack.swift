public func findCommonItems(rucksack: String) -> Set<Character> {
    let midpoint = rucksack.count / 2
    let left = rucksack.prefix(midpoint)
    let right = rucksack.suffix(midpoint)
    let leftSet = Set(left)
    let rightSet = Set(right)
    return leftSet.intersection(rightSet)
}

public func findBadge(rucksackGroup: [String]) -> Set<Character> {
    var theIntersection = Set(rucksackGroup[0])
    for i in 1..<rucksackGroup.count {
        theIntersection = theIntersection.intersection(Set(rucksackGroup[i]))
    }
    return theIntersection
}

public func scoreCharacter(myChar: Character) -> Int {
    if "ABCDEFGHIJKLMNOPQRSTUVWXYZ".contains(myChar) {
        return Int(myChar.asciiValue! - Character("A").asciiValue! + 27)
    } else if "abcdefghijklmnopqrstuvwxyz".contains(myChar) {
        return Int(myChar.asciiValue! - Character("a").asciiValue! + 1)
    } else {
        return 0
    }
}

public func scoreRucksackCollection(lines: [String]) -> Int {
    return lines
        .filter { $0 != "" }
        .map { findCommonItems(rucksack: $0) }
        .map { $0.map { scoreCharacter(myChar: $0) } }
        .map { $0.reduce(0, +) }
        .reduce(0, +)
}

public func findAndPrioritizeBadges(lines: [String]) -> Int {
    let nonEmptyLines = lines.filter { $0 != "" }
    let groupSize = 3
    let countOfGroups = nonEmptyLines.count / groupSize
    return (0..<countOfGroups)
        .map { Array(nonEmptyLines[($0 * groupSize)..<(($0 * groupSize) + 3)]) }
        .map { findBadge(rucksackGroup: $0) }
        .map { $0.map { scoreCharacter(myChar: $0) } }
        .map { $0.reduce(0, +) }
        .reduce(0, +)
}