func findMarkerOfSize(signal: String, size: Int) -> Int {
    var buffer: [Character] = []
    var i = 0
    for myIndex in signal.indices {
        if buffer.count >= size {
            buffer.removeFirst()
        }
        buffer.append(signal[myIndex])
        i += 1
        if Set(buffer).count == size {
            break
        }
    }
    return i
}

public struct Day_06 {
    public static func main(_ line: String) {
        let startOfPacketPosition = findMarkerOfSize(signal: line, size: 4)
        let startOfMessagePosition = findMarkerOfSize(signal: line, size: 14)

        print("start of package marker found at position \(startOfPacketPosition)")
        print("start of message marker found at position \(startOfMessagePosition)")
    }
}