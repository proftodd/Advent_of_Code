package org.jtodd.aoc.asteroids;

import java.awt.geom.Line2D;
import java.awt.geom.Point2D;
import java.io.IOException;
import java.nio.file.FileSystems;
import java.nio.file.Files;
import java.nio.file.Path;
import java.util.AbstractMap;
import java.util.List;
import java.util.Map;
import java.util.function.Function;
import java.util.stream.Collectors;
import java.util.stream.IntStream;

public class MonitoringStation {

    public static void main(String [] args) {
        Path p = FileSystems.getDefault().getPath(args[0]);
        List<String> lines;
        try {
            lines = Files.readAllLines(p);
        } catch (IOException e) {
            throw new RuntimeException(e);
        }
        List<Point2D> field = getAsteroidField(lines);
        Map<Point2D, Long> scores = field.stream().collect(
            Collectors.toMap(Function.identity(), a -> viewableAsteroids(a, field))
        );
        Map.Entry<Point2D, Long> best = scores.entrySet().stream()
            .max((e1, e2) -> e1.getValue().compareTo(e2.getValue()))
            .get();
        System.out.printf("Best is (%d,%d) with %d viewable asteroids\n", (int) best.getKey().getX(), (int) best.getKey().getY(), best.getValue());
    }
    
    public static List<Point2D> getAsteroidField(List<String> lines) {        
        return IntStream.range(0, lines.size())
            .mapToObj(Integer::valueOf)
            .flatMap(j -> 
                IntStream.range(0, lines.get(j).length())
                    .filter(i -> lines.get(j).charAt(i) == '#')
                    .mapToObj(i -> new Point2D.Float(i, j))
            )
            .collect(Collectors.toList());
    }

    public static long viewableAsteroids(Point2D asteroid, List<Point2D> field) {
        return field.stream().filter(a -> canViewFrom(asteroid, a, field)).count();
    }

    public static boolean canViewFrom(Point2D source, Point2D target, List<Point2D> field) {
        if (source == target) {
            return false;
        }
        Line2D lineOfSight = new Line2D.Float(source, target);
        return field.stream()
            .filter(a -> a != source)
            .filter(a -> a != target)
            .noneMatch(a -> lineContains(lineOfSight, a));
    }

    public static boolean lineContains(Line2D line, Point2D point) {
        double minX = line.getP1().getX() <= line.getP2().getX() ? line.getP1().getX() : line.getP2().getX();
        double maxX = minX == line.getP1().getX() ? line.getP2().getX() : line.getP1().getX();
        double minY = line.getP1().getY() <= line.getP2().getY() ? line.getP1().getY() : line.getP2().getY();
        double maxY = minY == line.getP1().getY() ? line.getP2().getY() : line.getP1().getY();
        return point.getX() >= minX &&
               point.getX() <= maxX &&
               point.getY() >= minY &&
               point.getY() <= maxY &&
               line.ptLineDist(point) == 0.0;
    }
}