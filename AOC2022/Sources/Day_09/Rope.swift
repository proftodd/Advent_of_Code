import Foundation

public struct Coordinate: Equatable, Hashable {
    var x: Int
    var y: Int

    public static func +(lhs: Coordinate, rhs: Coordinate) -> Coordinate {
        return Coordinate(x: lhs.x + rhs.x, y: lhs.y + rhs.y)
    }

    public static func ==(lhs: Coordinate, rhs: Coordinate) -> Bool {
        return lhs.x == rhs.x && lhs.y == rhs.y
    }
}

public enum Direction: String {
    case up = "U"
    case right = "R"
    case down = "D"
    case left = "L"

    var transform: Coordinate {
        switch (self) {
            case .up: return Coordinate(x: 0, y: 1)
            case .right: return Coordinate(x: 1, y: 0)
            case .down: return Coordinate(x: 0, y: -1)
            case .left: return Coordinate(x: -1, y: 0)
        }
    }
}

public class Head {
    var position: Coordinate

    init() {
        position = Coordinate(x: 0, y: 0)
    }

    public func move(_ direction: String) {
        position = position + Direction(rawValue: direction)!.transform
        // print("Head now at \(position)")
    }
}

public class Tail {
    var position: Coordinate
    var head: Head
    var logger: Set<Coordinate>

    init(_ head: Head) {
        position = Coordinate(x: 0, y: 0)
        self.head = head
        logger = Set<Coordinate>()
        logger.insert(position)
    }

    public func move() {
        if !isTouchingHead() {
            if head.position.x > self.position.x {
                self.position.x += 1
            } else if head.position.x < self.position.x {
                self.position.x -= 1
            }
            if head.position.y > self.position.y {
                self.position.y += 1
            } else if head.position.y < self.position.y {
                self.position.y -= 1
            }
            // print("Tail now at \(position)")
            logger.insert(position)
        }
    }

    private func isTouchingHead() -> Bool {
        return abs(head.position.x - self.position.x) < 2 &&
               abs(head.position.y - self.position.y) < 2
    }
}

public class Rope {
    var head: Head
    var tail: Tail
    var moves: [String]

    init(_ moves: [String]) {
        self.moves = moves
        self.head = Head()
        self.tail = Tail(self.head)
    }

    func simulate() {
        for instruction in moves {
            if instruction == "" {
                continue
            }
            let parts = instruction.components(separatedBy: .whitespaces)
            let direction = parts[0]
            let number = Int(parts[1])!
            for _ in 1...number {
                head.move(direction)
                tail.move()
            }
        }
    }
}