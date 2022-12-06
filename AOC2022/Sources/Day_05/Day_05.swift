public struct Day_05 {
    public static func main(_ lines: [String]) {
        let beach = Beach(lines)
        beach.followInstructions()
        let tops = beach.getTopOfAllStacks()

        print("The tops of the stacks is \(tops)")

        let beach2 = Beach(lines)
        beach2.followInstructionsWithModernCrane()
        let tops2 = beach2.getTopOfAllStacks()

        print("The tops of the second beach's stacks is \(tops2)")
    }
}