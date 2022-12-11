public struct Day_10 {
    public static func main(_ lines: [String]) {
        let cpu = Cpu(lines)
        let signal = cpu.execute()
        print("Signal = \(signal)")
    }
}