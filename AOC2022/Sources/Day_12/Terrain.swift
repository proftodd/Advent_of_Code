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
	public var x: Int
	public var y: Int
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

public class DijkstraCell: Hashable {
	public var coord: Coordinate
	public var visited: Bool
	public var distance: Int

	init(coord: Coordinate) {
		self.coord = coord
		visited = false
		distance = Int.max
	}

    public static func == (lhs: DijkstraCell, rhs: DijkstraCell) -> Bool {
        return lhs.coord == rhs.coord
    }

    public func hash(into hasher: inout Hasher) {
        hasher.combine(coord.x)
        hasher.combine(coord.y)
    }
}

public class Terrain {
	public var height: Int
	public var width: Int
	public var lines: [String]
	lazy var start: Coordinate = findLocation("S")
	lazy var end: Coordinate = findLocation("E")
	lazy var routes: [[Coordinate]] = findRoutesRecursive()
	lazy var shortestRouteLength = findShortestRouteLength()

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

	func findRoutesRecursive() -> [[Coordinate]] {
		func extendRoute(currentRoute: [Coordinate], direction: Direction) -> [[Coordinate]] {
			let currentPoint = currentRoute.last!
			let nextPoint = currentPoint + direction.transform

			if currentRoute.contains(where: { $0 == nextPoint }) {
				return []
			}

			guard let currentElevation = getElevation(currentPoint) else {
				return []
			}

			guard let nextElevation = getElevation(nextPoint) else {
				return []
			}

			if Int(exactly: nextElevation.asciiValue!)! - Int(exactly: currentElevation.asciiValue!)! > 1 {
				return []
			}

			let extendedRoute = currentRoute + [nextPoint]

			if nextPoint == end {
				return [extendedRoute]
			}

			return Direction.allCases
				.filter { $0 != direction }
				.flatMap { extendRoute(currentRoute: extendedRoute, direction: $0) }
		}

		let startingRoute = [start]
		return Direction.allCases
			.flatMap { extendRoute(currentRoute: startingRoute, direction: $0) }
			.sorted(by: { $0.count < $1.count })
	}

	func findShortestRouteLength() -> Int {
// 		print("findShortestRouteLength started")
		var cells: [[DijkstraCell]] = []

		for j in 0..<lines.count {
			var thisRow: [DijkstraCell] = []
			for i in 0..<lines[j].count {
				let c = Coordinate(i, j)
// 				print("creating Dijkstra cell with coordinate \(c)")
				thisRow.append(DijkstraCell(coord: c))
			}
			cells.append(thisRow)
		}

// 		print("Starting with consideration of \(self.start)")
        let startingCell = cells[self.start.y][self.start.x]
        startingCell.distance = 0
		var underConsideration:Set<DijkstraCell> = [startingCell]
// 		print("under consideration: \(underConsideration.count) cells")
		let endCell = cells[self.end.y][self.end.x]
// 		print("Looking for distance to \(endCell.coord)")

		while !endCell.visited {
			let current = underConsideration.min(by: { $0.distance < $1.distance })!
			underConsideration.remove(current)
			let currentElevation = Int(exactly: getElevation(current.coord)!.asciiValue!)!
			// print("current cell: \(current.coord) elevation: \(currentElevation) distance: \(current.distance) under consideration: \(underConsideration.count)")
			for d in Direction.allCases {
				let neighborCoord = current.coord + d.transform
				if neighborCoord.x < 0 || neighborCoord.x >= width ||
				   neighborCoord.y < 0 || neighborCoord.y >= height {
				   continue
			    }
				let neighbor = cells[neighborCoord.y][neighborCoord.x]
				if neighbor.visited {
					continue
				}
				let neighborElevation = getElevation(neighbor.coord)!
				if Int(exactly: neighborElevation.asciiValue!)! - currentElevation <= 1 {
					neighbor.distance = min(neighbor.distance, current.distance + 1)
					underConsideration.insert(neighbor)
				}
			}
			current.visited = true
		}

		return endCell.distance
	}
}
