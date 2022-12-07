func findMarkerAndPosition(_ signal: String) -> (String, Int) {
    var buffer: [Character] = []
    var i = 0
    for myIndex in signal.indices {
        if buffer.count >= 4 {
            buffer.removeFirst()
        }
        buffer.append(signal[myIndex])
        // print("Character \(i): \(line[myIndex])")
        i += 1
        if Set(buffer).count == 4 {
            break
        }
    }
    return (String(buffer), i)
}

public struct Day_06 {
    public static func main(_ line: String) {
        let (marker, position) = findMarkerAndPosition(line)

        print("marker found at position \(position)")
    }
}