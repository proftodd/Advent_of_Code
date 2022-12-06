public struct Day_05 {
    public static func main(_ lines: [String]) {
        let beach = Beach(lines)
        beach.followInstructions()
        let tops = beach.getTopOfAllStacks()

        print("The tops of the stacks is \(tops)")
    }
}