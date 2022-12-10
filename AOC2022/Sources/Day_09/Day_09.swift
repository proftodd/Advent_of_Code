public struct Day_09 {
    public static func main(_ lines: [String]) {
        let rope = Rope(lines)
        rope.simulate()
        let log = rope.tail.logger
        print("Tail has been in \(log.count) unique positions in this simulation")
    }
}