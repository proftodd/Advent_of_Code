public struct Day_08 {
    public static func main(_ lines: [String]) {
        let forest = Forest(lines)
        let visibleCount = forest.count( { $0.visible })
        print("Number of visible trees = \(visibleCount)")
    }
}