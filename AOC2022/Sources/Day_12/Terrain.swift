public enum Direction: CaseIterable {
	case north
	case east
	case south
	case west

	var transform: Coordinate {
		switch (self) {
			case .north: return Coordinate(0, -1)
			case .east: return Coordinate(1, 0)
			case .south: return Coordinate(0, 1)
			case .west: return Coordinate(-1, 0)
		}
	}	
}

public class Coordinate: CustomStringConvertible, Equatable {
	var x: Int
	var y: Int
	public var description: String { return "Coordinate: (\(x),\(y))" }

	init(_ x: Int, _ y: Int) {
		self.x = x
		self.y = y
	}

	public static func ==(_ lhs: Coordinate, _ rhs: Coordinate) -> Bool {
		return lhs.x == rhs.x && lhs.y == rhs.y
	}

	public static func +(_ lhs: Coordinate, _ rhs: Coordinate) -> Coordinate {
		return Coordinate(lhs.x + rhs.x, lhs.y + rhs.y)
	}
}

public class Terrain {
	public var height: Int
	public var width: Int
	public var lines: [String]
	lazy var start: Coordinate = findLocation("S")
	lazy var end: Coordinate = findLocation("E")
	lazy var routes: [[Coordinate]] = findRoutes()

	init(_ lines: [String]) {
		let filteredLines = lines.filter { $0 != "" }
		self.height = filteredLines.count
		self.width = filteredLines[0].count
		self.lines = filteredLines
	}

	func findLocation(_ target: Character) -> Coordinate {
		for i in 0..<height {
// 			print("checking \(lines[i]) for '\(target)'")
			if let startIndex = lines[i].firstIndex(of: target) {
				let index = lines[i].distance(from: lines[i].startIndex, to: startIndex)
				return Coordinate(index, i)
			}
		}
		return Coordinate(-1, -1)
	}

	func getElevation(_ coord: Coordinate) -> Character? {
		if coord.x < 0 || coord.x >= width ||
		   coord.y < 0 || coord.y >= height {
		   return nil
	    } else if coord == start {
			return "a"
		} else if coord == end {
			return "z"
		} else {
			return lines[coord.y][lines[coord.y].index(lines[coord.y].startIndex, offsetBy: coord.x)]
		}
	}

	func findRoutes() -> [[Coordinate]] {
		func extendRoute(currentRoute: [Coordinate], direction: Direction) -> [Coordinate]? {
			let currentPoint = currentRoute.last!
			let nextPoint = currentPoint + direction.transform
//			print("\t\tchecking elevation of current point \(currentPoint)")
            guard let currentElevation = getElevation(currentPoint) else {
//                print("\t\t\tit's off the map")
                return nil
            }
//            print("\t\t\tit's \(currentElevation)")
//            print("\t\tchecking elevation of next point \(nextPoint)")
			guard let nextElevation = getElevation(nextPoint) else {
//                print("\t\t\tit's off the map")
				return nil
			}
//            print("\t\t\tit's \(nextElevation)")
            if currentRoute.contains(where: { $0 == nextPoint }) {
                return nil
            } else if Int(exactly: nextElevation.asciiValue!)! - Int(exactly: currentElevation.asciiValue!)! > 1 {
				return nil
			} else {
				return currentRoute + [nextPoint]
			}
		}

		var routes: [[Coordinate]] = [[start]]
		while routes.contains(where: { $0.last! != end }) {
//		for i in 1...3 {
			let newRoutes = routes.flatMap { (oldRoute: [Coordinate]) -> [[Coordinate]] in
//				print("trying oldRoute \(oldRoute)")
                if oldRoute.last! == end {
                    return [oldRoute]
                }
				var someRoutes: [[Coordinate]] = []
				for d in Direction.allCases {
					if let aRoute = extendRoute(currentRoute: oldRoute, direction: d) {
//						print("\textendedRoute: \(aRoute)")
						someRoutes.append(aRoute)
					}
				}
				return someRoutes
			}
//			print("After \(i) extension cycles, found \(newRoutes.count) routes")
			routes = newRoutes
		}

        return routes.sorted(by: { $0.count < $1.count })
	}
}
