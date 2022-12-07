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
        let root = readScript(lines)
        var hits: [Directory] = []
        root.findMatchingDirectories(collector: &hits) { $0.size < 100_000 }
        let sum = hits.map { $0.size }.reduce(0, +)
        print("sum of sizes of matching directories = \(sum)")
    }
}