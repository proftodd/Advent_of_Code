public struct Day_12 {
	public static func main(_ lines: [String]) {
        let terrain = Terrain(lines)
        print("Shortest route has \(terrain.findRoutes()[0].count) steps")
    }
}
