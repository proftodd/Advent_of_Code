public struct Day_11 {
    public static func main(_ lines: [String]) {
        let rounds = 20
        let barrel = Barrel(lines)
        for _ in 1...rounds {
            barrel.act()
        }
        print("After \(rounds) rounds the monkey business level is \(barrel.monkeyBusiness())")
    }
}
