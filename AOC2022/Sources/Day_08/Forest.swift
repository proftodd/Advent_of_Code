public enum Direction: CaseIterable {
    case north
    case east
    case south
    case west
}

func getTransform(_ direction: Direction) -> (Int, Int) {
    switch (direction) {
        case .north: return (0, -1)
        case .east: return (1, 0)
        case .south: return (0, 1)
        case .west: return (-1, 0)
    }
}

public class Tree {
    var coordinates: (Int, Int)
    var height: Int
    var forest: Forest
    lazy var visible = determineVisibility()

    init(coordinates: (Int, Int), height: Int, forest: Forest) {
        self.coordinates = coordinates
        self.height = height
        self.forest = forest
    }

    func determineVisibility() -> Bool {
        return Direction.allCases
            .map { getTransform($0) }
            .map { self.forest.getSightline(coord: self.coordinates, direction: $0) }
            .map { $0.allSatisfy({ $0 < self.height }) }
            .contains(where: { $0 })
    }
}

public class Forest {
    private var _height: Int
    private var _width: Int
    private var _trees: [[Tree]]
    public var height: Int { get { return _height } }
    public var width: Int { get { return _width }}

    init(_ map: [String]) {
        let nonEmptyMap = map.filter { $0 != "" }
        _height = nonEmptyMap.count
        _width = nonEmptyMap[0].count
        _trees = []
        for i in 0..<height {
            let thisLine = nonEmptyMap[i]
            var thisRow: [Tree] = []
            for j in 0..<width {
                let start = thisLine.startIndex
                let heightCharacter = thisLine[thisLine.index(start, offsetBy: j)]
                thisRow.append(Tree(coordinates: (j, i), height: heightCharacter.wholeNumberValue!, forest: self))
            }
            _trees.append(thisRow)
        }
    }

    public subscript(col: Int, row: Int) -> Tree {
        get {
            return _trees[row][col]
        }
    }

    public func count(_ predicate: (Tree) -> Bool) -> Int {
        var count = 0
        for y in 0..<self.height {
            for x in 0..<self.width {
                if predicate(self[x, y]) {
                    count += 1
                }
            }
        }
        return count
    }

    public func getSightline(coord: (Int, Int), direction: (Int, Int)) -> [Int] {
        var result: [Int] = []
        let (startX, startY) = coord
        let (deltaX, deltaY) = direction
        var x = startX + deltaX
        var y = startY + deltaY
        while (x >= 0 && x < width && y >= 0 && y < height) {
            result.append(self[x, y].height)
            x += deltaX
            y += deltaY
        }
        // print("Sightline from \(coord) toward \(direction): \(result); result.isEmpty = \(result.isEmpty)")
        return result
    }
}