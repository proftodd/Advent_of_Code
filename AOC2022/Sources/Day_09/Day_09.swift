public struct Day_09 {
    public static func main(_ lines: [String]) {
        let rope = Rope(knotCount: 2, moves: lines)
        rope.simulate()
        let log = rope.tails.last!.logger
        print("Tail has been in \(log.count) unique positions in this simulation")

        let tenKnotRope = Rope(knotCount: 10, moves: lines)
        tenKnotRope.simulate()
        let log2 = tenKnotRope.tails.last!.logger
        print("Tail of ten-knot rope has been in \(log2.count) unique positions in this simulation")
    }
}