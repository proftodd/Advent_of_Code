protocol FileSystemObject {
    var name: String { get }
    var size: Int { get }
}

public class File: FileSystemObject {
    var name: String
    var size: Int

    init(name: String, size: Int) {
        self.name = name
        self.size = size
    }
}

public class Directory: FileSystemObject {
    var name: String
    var contents: [FileSystemObject]

    init(_ name: String) {
        self.name = name
        self.contents = []
    }

    func add(_ child: FileSystemObject) {
        if self.contents.first(where: { $0.name == child.name }) == nil {
            // print(" . adding fso \(child.name) to dir \(self.name)")
            self.contents.append(child)
            // print(" . contents = \(self.contents)")
        }
    }

    func subdir(_ name: String) -> Directory? {
        return self.contents.first(where: { (fso: FileSystemObject) in
            guard let dir = fso as? Directory else {
                return false
            }
            // print("testing \(dir.name) for match with \(name)")
            return dir.name == name
        }) as? Directory
    }

    var size: Int {
        return self.contents.map { $0.size }.reduce(0, +)
    }

    func findMatchingDirectories(collector: inout [Directory], predicate: (Directory) -> Bool) {
        if (predicate(self)) {
            collector.append(self)
        }
        let subdirs = self.contents
            .compactMap { $0 as? Directory }
        subdirs.forEach { $0.findMatchingDirectories(collector: &collector, predicate: predicate) }
    } 

    // func traverse(_ theFunc: @escaping (Directory) -> Void) -> Void {
    //     theFunc(self)
    //     contents
    //         .compactMap { $0 as? Directory }
    //         .forEach { $0.traverse(theFunc) }
    // }
}

public class FileSystem {
    var root: Directory
    var currentDirectory: Directory
    var path: [Directory]

    init() {
        root = Directory("/")
        currentDirectory = root
        path = [root]
    }

    func goToRoot() {
        self.currentDirectory = root
        self.path = [root]
    }

    func goToParent() {
        self.path.removeLast()
        self.currentDirectory = path.last!
    }

    func goToDirectory(_ name: String) {
        guard let dir = self.currentDirectory.subdir(name) else {
            print(" . Cannot find subdir \(name) in \(self.currentDirectory.name)")
            // print(" . self.contents = \(self.currentDirectory.contents)")
            return
        }
        self.currentDirectory = dir
        self.path.append(dir)
    }

    func addDirectory(_ name: String) {
        self.currentDirectory.add(Directory(name))
    }

    func addFile(name: String, size: Int) {
        self.currentDirectory.add(File(name: name, size: size))
    }
}