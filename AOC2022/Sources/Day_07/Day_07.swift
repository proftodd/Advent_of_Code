import Foundation

func readScript(_ lines: [String]) -> Directory {
    let fs = FileSystem()
    for line in lines {
        // print(line)
        switch line {
            case "": break
            case "$ ls": break
            case "$ cd /": fs.goToRoot()
            case "$ cd ..": fs.goToParent()
            case let str where str.starts(with: "$ cd"):
                let parts = str.components(separatedBy: .whitespaces)
                fs.goToDirectory(parts.last!)
            case let str where str.starts(with: "dir"):
                let parts = str.components(separatedBy: .whitespaces)
                fs.addDirectory(parts.last!)
            default:
                let parts = line.components(separatedBy: .whitespaces)
                fs.addFile(name: parts.last!, size: Int(parts.first!)!)
        }
    }
    return fs.root
}

public struct Day_07 {
    public static func main(_ lines: [String]) {
        let diskSize = 70_000_000
        let minimumSpaceNeeded = 30_000_000

        let root = readScript(lines)
        let usedSpace = root.size
        let availableSpace = diskSize - usedSpace
        let neededSpace = minimumSpaceNeeded - availableSpace

        var hits: [Directory] = []
        root.findMatchingDirectories(collector: &hits) { $0.size < 100_000 }
        let sum = hits.map { $0.size }.reduce(0, +)
        print("sum of sizes of matching directories = \(sum)")

        var allDirectories: [Directory] = []
        root.findMatchingDirectories(collector: &allDirectories) { _ in return true }
        let dirToDelete = allDirectories
            .sorted { $1.size > $0.size }
            .first(where: { $0.size >= neededSpace })!

        print("total disk size = \(diskSize)")
        print("minimum space needed = \(minimumSpaceNeeded)")
        print("used space = \(usedSpace)")
        print("available space = \(availableSpace)")
        print("needed space = \(neededSpace)")
        print("smallest directory that will free the needed space is \(dirToDelete.name)")
        print(" . its size is \(dirToDelete.size)")
    }
}