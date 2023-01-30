public struct Day_12 {
	public static func main(_ lines: [String]) {
        let terrain = Terrain(lines)
        print("Shortest route has \(terrain.shortestRouteLength) steps")
    }
}
