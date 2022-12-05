import Foundation

public struct Interval: Equatable {
    var start: Int
    var end: Int

    init(start: Int, end: Int) {
        self.start = start
        self.end = end
    }

    public func contains(_ other: Interval) -> Bool {
        return self.start <= other.start && self.end >= other.end
    }

    public func overlaps(_ other: Interval) -> Bool {
        return self.start <= other.start && self.end >= other.start
    }
}

public func parseLine(_ line: String) -> (Interval, Interval) {
    let intervals = line.components(separatedBy: ",")
    let first = intervals[0].components(separatedBy: "-")
    let second = intervals[1].components(separatedBy: "-")
    let a = Interval(start: Int(first[0])!, end: Int(first[1])!)
    let b = Interval(start: Int(second[0])!, end: Int(second[1])!)
    return (a, b)
}

public func sortIntervals(a: Interval, b: Interval) -> (Interval, Interval) {
    if (a.start == b.start) {
        if (a.end >= b.end) {
            return (a, b)
        } else {
            return (b, a)
        }
    } else if (a.start < b.start) {
        return (a, b)
    } else {
        return (b, a)
    }
}

public struct Day_04 {
    public static func main(lines: [String]) {
        let sortedIntervals: [(Interval, Interval)] = lines
            .filter { $0 != "" }
            .map { parseLine($0) }
            .map { sortIntervals(a: $0.0, b: $0.1) }
        
        let degenerateCount: Int = sortedIntervals.reduce(0) { $1.0.contains($1.1) ? $0 + 1 : $0 }
        let overlapCount: Int = sortedIntervals.reduce(0) { $1.0.overlaps($1.1) ? $0 + 1 : $0 }
        
        print("Count of intervals that contain the other one: \(degenerateCount)")
        print("Count of intervals that overlap: \(overlapCount)")
    }
}