public struct Day_03 {
    public static func main(lines: [String]) {
        let prioritySum = scoreRucksackCollection(lines: lines)
        print("Priority sum = \(prioritySum)")
        let badgePrioritySum = findAndPrioritizeBadges(lines: lines)
        print("Sum of badge priority = \(badgePrioritySum)")
    }
}