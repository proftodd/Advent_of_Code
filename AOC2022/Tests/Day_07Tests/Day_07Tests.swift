import XCTest

@testable import Day_07

final class Day_07Tests: XCTestCase {
    let lines = [
        "$ cd /",
        "$ ls",
        "dir a",
        "14848514 b.txt",
        "8504156 c.dat",
        "dir d",
        "$ cd a",
        "$ ls",
        "dir e",
        "29116 f",
        "2557 g",
        "62596 h.lst",
        "$ cd e",
        "$ ls",
        "584 i",
        "$ cd ..",
        "$ cd ..",
        "$ cd d",
        "$ ls",
        "4060174 j",
        "8033020 d.log",
        "5626152 d.ext",
        "7214296 k",
        ""
    ]

    func testFile() {
        let name = "b.txt"
        let size = 14848514
        let file = File(name: name, size: size)
        XCTAssertEqual(file.name, name)
        XCTAssertEqual(file.size, size)
    }

    func testDirectory() {
        let emptyDirectory = Directory("/")
        XCTAssertEqual(emptyDirectory.name, "/")
        XCTAssertEqual(emptyDirectory.size, 0)

        let directoryWithFiles = Directory("a")
        directoryWithFiles.add(File(name: "aa", size: 5))
        directoryWithFiles.add(File(name: "bb", size: 8))
        XCTAssertEqual(directoryWithFiles.name, "a")
        XCTAssertEqual(directoryWithFiles.size, 13)

        let directoryWithDirs = Directory("b")
        let subdirectory = Directory("bb")
        subdirectory.add(File(name: "bbb", size: 8))
        subdirectory.add(File(name: "ccc", size: 5))
        subdirectory.add(File(name: "ccc", size: 32))
        directoryWithDirs.add(subdirectory)
        directoryWithDirs.add(File(name: "cc", size: 7))
        XCTAssertEqual(directoryWithDirs.name, "b")
        XCTAssertEqual(directoryWithDirs.size, 20)
    }

    func testRunScript() {
        let root = readScript(lines)
        var hits: [Directory] = []
        // let traveler = directoryOfMaximumSize(size: 100_000, collector: &hits)
        // root.traverse(traveler)
        root.findMatchingDirectories(collector: &hits) { $0.size < 100_000 }
        let sum = hits.map { $0.size }.reduce(0, +)
        XCTAssertEqual(sum, 95437)
    }

    // func directoryOfMaximumSize(size: Int, collector: inout [Directory]) -> (Directory) -> Void {
    //     return { if $0.size < size { collector.append($0) } }
    // }
}