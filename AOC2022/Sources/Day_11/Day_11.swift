public struct Day_11 {
    public static func main(_ lines: [String]) {
        let rounds = 20
        let barrel = Barrel(lines, worryModerator: 3)
        barrel.act(rounds)
        print("After \(rounds) rounds the monkey business level is \(barrel.monkeyBusiness())")

        let rounds2 = 10_000
        let barrel2 = Barrel(lines, worryModerator: nil)
        barrel2.act(rounds2)
        print("After \(rounds2) rounds the monkey business level is \(barrel2.monkeyBusiness())")
    }
}
