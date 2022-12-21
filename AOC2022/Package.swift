// swift-tools-version: 5.7
// The swift-tools-version declares the minimum version of Swift required to build this package.

import PackageDescription

let package = Package(
    name: "AOC2022",
    dependencies: [
        // Dependencies declare other packages that this package depends on.
        // .package(url: /* package url */, from: "1.0.0"),
    ],
    targets: [
        // Targets are the basic building blocks of a package. A target can define a module or a test suite.
        // Targets can depend on other targets in this package, and on products in packages this package depends on.
        .target(
            name: "Day_01",
            dependencies: []),
        .target(
            name: "Day_02",
            dependencies: []),
        .testTarget(
            name: "Day_02Tests",
            dependencies: ["Day_02"]),
        .target(
            name: "Day_03",
            dependencies: []),
        .testTarget(
            name: "Day_03Tests",
            dependencies: ["Day_03"]),
        .target(
            name: "Day_04",
            dependencies: []),
        .testTarget(
            name: "Day_04Tests",
            dependencies: ["Day_04"]),
        .target(
            name: "Day_05",
            dependencies: []),
        .testTarget(
            name: "Day_05Tests",
            dependencies: ["Day_05"]),
        .target(
            name: "Day_06",
            dependencies: []),
        .testTarget(
            name: "Day_06Tests",
            dependencies: ["Day_06"]),
        .target(
            name: "Day_07",
            dependencies: []),
        .testTarget(
            name: "Day_07Tests",
            dependencies: ["Day_07"]),
        .target(
            name: "Day_08",
            dependencies: []),
        .testTarget(
            name: "Day_08Tests",
            dependencies: ["Day_08"]),
        .target(
            name: "Day_09",
            dependencies: []),
        .testTarget(
            name: "Day_09Tests",
            dependencies: ["Day_09"]),
        .target(
            name: "Day_10",
            dependencies: []),
        .testTarget(
            name: "Day_10Tests",
            dependencies: ["Day_10"]),
        .target(
            name: "Day_11",
            dependencies: []),
        .testTarget(
            name: "Day_11Tests",
            dependencies: ["Day_11"]),
        .target(
            name: "Day_12",
            dependencies: []),
        .testTarget(
            name: "Day_12Tests",
            dependencies: ["Day_12"]),
        .executableTarget(
            name: "AOC2022",
            dependencies: ["Day_01", "Day_02", "Day_03", "Day_04", "Day_05",
                           "Day_06", "Day_07", "Day_08", "Day_09", "Day_10",
                           "Day_11", "Day_12"]),
        .testTarget(
            name: "AOC2022Tests",
            dependencies: ["AOC2022"]),
    ]
)
