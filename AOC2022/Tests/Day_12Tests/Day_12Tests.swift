import XCTest

@testable import Day_12

final class CoordinateTests: XCTestCase {
	func testItCanAddCoordinates() {
		let a = Coordinate(1, 2)
		let b = Coordinate(1, 1)
		XCTAssertEqual(Coordinate(2, 3), a + b)
	}
}

final class Day_12Tests: XCTestCase {
	let lines = [
		"Sabqponm",
		"abcryxxl",
		"accszExk",
		"acctuvwj",
		"abdefghi",
		""
	]

	func testItCanFindStartAndEnd() {
		let terrain = Terrain(lines)
		let start = terrain.start
		let end = terrain.end
		XCTAssertEqual(Coordinate(0, 0), start)
		XCTAssertEqual(Coordinate(5, 2), end)
	}

	func testItCanFindElevations() {
		let terrain = Terrain(lines)
		XCTAssertEqual(terrain.getElevation(Coordinate(-1, -1)), nil)
		XCTAssertEqual(terrain.getElevation(Coordinate(3, 0)), "q")
		XCTAssertEqual(terrain.getElevation(terrain.start), "a")
		XCTAssertEqual(terrain.getElevation(terrain.end), "z")
	}

	func testItCanFindAnEasyRoute() {
		let map = ["SbcdefghijklmnopqrstuvwxyE"]
		let terrain = Terrain(map)
		let routes = terrain.findRoutes()
		XCTAssertFalse(routes.isEmpty)
		XCTAssertEqual(26, routes[0].count)
		XCTAssertEqual(routes, [[
			Coordinate(0,  0), Coordinate(1,  0), Coordinate(2,  0), Coordinate(3,  0),
			Coordinate(4,  0), Coordinate(5,  0), Coordinate(6,  0), Coordinate(7,  0),
			Coordinate(8,  0), Coordinate(9,  0), Coordinate(10, 0), Coordinate(11, 0),
			Coordinate(12, 0), Coordinate(13, 0), Coordinate(14, 0), Coordinate(15, 0),
			Coordinate(16, 0), Coordinate(17, 0), Coordinate(18, 0), Coordinate(19, 0),
			Coordinate(20, 0), Coordinate(21, 0), Coordinate(22, 0), Coordinate(23, 0),
			Coordinate(24, 0), Coordinate(25, 0)
		]])
	}

	func testItCanFindAnEasyRouteWithTurns() {
		let map = [
			"bcfgjknorsvwE",
			"Sdehilmpqtuxy"
		]
		let terrain = Terrain(map)
		let routes = terrain.findRoutes()
		XCTAssertFalse(routes.isEmpty)
		XCTAssertEqual(26, routes[0].count)
		XCTAssertEqual(routes, [[
			Coordinate(0,  1), Coordinate(0,  0), Coordinate(1,  0), Coordinate(1,  1),
			Coordinate(2,  1), Coordinate(2,  0), Coordinate(3,  0), Coordinate(3,  1),
			Coordinate(4,  1), Coordinate(4,  0), Coordinate(5,  0), Coordinate(5,  1),
			Coordinate(6,  1), Coordinate(6,  0), Coordinate(7,  0), Coordinate(7,  1),
			Coordinate(8,  1), Coordinate(8,  0), Coordinate(9,  0), Coordinate(9,  1),
			Coordinate(10, 1), Coordinate(10, 0), Coordinate(11, 0), Coordinate(11, 1),
			Coordinate(12, 1), Coordinate(12, 0)
		]])
	}

	func testItFindsRoutes() {
		let terrain = Terrain(lines)
		let routes = terrain.findRoutes()
		XCTAssertFalse(routes.isEmpty)
		XCTAssertTrue(routes.contains(where: { $0.count == 32 }))
        XCTAssertTrue(routes.contains(where: { $0 == [
            Coordinate(0, 0), Coordinate(1, 0), Coordinate(1, 1), Coordinate(2, 1),
            Coordinate(2, 2), Coordinate(2, 3), Coordinate(2, 4), Coordinate(3, 4),
            Coordinate(4, 4), Coordinate(5, 4), Coordinate(6, 4), Coordinate(7, 4),
            Coordinate(7, 3), Coordinate(7, 2), Coordinate(7, 1), Coordinate(7, 0),
            Coordinate(6, 0), Coordinate(5, 0), Coordinate(4, 0), Coordinate(3, 0),
            Coordinate(3, 1), Coordinate(3, 2), Coordinate(3, 3), Coordinate(4, 3),
            Coordinate(5, 3), Coordinate(6, 3), Coordinate(6, 2), Coordinate(6, 1),
            Coordinate(5, 1), Coordinate(4, 1), Coordinate(4, 2), Coordinate(5, 2)
        ] }))
	}
}
