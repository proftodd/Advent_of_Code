package org.jtodd.aoc.asteroids;

import java.awt.geom.Line2D;
import java.awt.geom.Point2D;
import java.io.IOException;
import java.nio.file.FileSystems;
import java.nio.file.Files;
import java.nio.file.Path;
import java.util.Arrays;
import java.util.List;
import java.util.Map;
import java.util.function.Function;
import java.util.stream.Collectors;

import org.junit.Assert;
import org.junit.Before;
import org.junit.Test;

public class TestMonitoringStation {

    List<Point2D> field;

    @Before
    public void setUp() {
        String [] lineArray = new String [] {
            ".#..#",
            ".....",
            "#####",
            "....#",
            "...##"
        };
        List<String> lines = Arrays.asList(lineArray);
        field = MonitoringStation.getAsteroidField(lines);
    }

    @Test
    public void testReadsLinesToAsteroids() {
        Assert.assertEquals(10, field.size());
    }

    @Test
    public void testViewableAsteroids() {
        Point2D anAsteroid = field.get(8);
        Assert.assertEquals(8L, MonitoringStation.viewableAsteroids(anAsteroid, field));
    }

    @Test
    public void testCanViewFrom() {
        Point2D source = field.get(8);
        Point2D target1 = field.get(4);
        Point2D target2 = field.get(0);
        Assert.assertTrue(MonitoringStation.canViewFrom(source, target1, field));
        Assert.assertFalse(MonitoringStation.canViewFrom(source, target2, field));
    }

    @Test
    public void testLineContains() {
        Line2D line = new Line2D.Float(field.get(9), field.get(1));
        Point2D point1 = field.get(7);
        Point2D point2 = new Point2D.Float(4.0f, 5.0f);
        Point2D point3 = field.get(0);
        Assert.assertTrue(MonitoringStation.lineContains(line, point1));
        Assert.assertFalse(MonitoringStation.lineContains(line, point2));
        Assert.assertFalse(MonitoringStation.lineContains(line, point3));
    }

    @Test
    public void testExample01() throws IOException {
        String filename = "example_01.txt";
        Map.Entry<Point2D, Long> best = getBest(filename);
        Assert.assertEquals(3.0, best.getKey().getX(), 1e-6);
        Assert.assertEquals(4.0, best.getKey().getY(), 1e-6);
        Assert.assertEquals(8L, best.getValue().longValue());
    }
    
    @Test
    public void testExample02() throws IOException {
        String filename = "example_02.txt";
        Map.Entry<Point2D, Long> best = getBest(filename);
        Assert.assertEquals(5.0, best.getKey().getX(), 1e-6);
        Assert.assertEquals(8.0, best.getKey().getY(), 1e-6);
        Assert.assertEquals(33L, best.getValue().longValue());
    }

    @Test
    public void testExample03() throws IOException {
        String filename = "example_03.txt";
        Map.Entry<Point2D, Long> best = getBest(filename);
        Assert.assertEquals(1.0, best.getKey().getX(), 1e-6);
        Assert.assertEquals(2.0, best.getKey().getY(), 1e-6);
        Assert.assertEquals(35L, best.getValue().longValue());
    }

    @Test
    public void testExample04() throws IOException {
        String filename = "example_04.txt";
        Map.Entry<Point2D, Long> best = getBest(filename);
        Assert.assertEquals(6.0, best.getKey().getX(), 1e-6);
        Assert.assertEquals(3.0, best.getKey().getY(), 1e-6);
        Assert.assertEquals(41L, best.getValue().longValue());
    }

    @Test
    public void testExample05() throws IOException {
        String filename = "example_05.txt";
        Map.Entry<Point2D, Long> best = getBest(filename);
        Assert.assertEquals(11.0, best.getKey().getX(), 1e-6);
        Assert.assertEquals(13.0, best.getKey().getY(), 1e-6);
        Assert.assertEquals(210L, best.getValue().longValue());
    }

    private static Map.Entry<Point2D, Long> getBest(String filename) throws IOException {
        Path p = FileSystems.getDefault().getPath(filename);
        List<String> lines = Files.readAllLines(p);
        List<Point2D> field = MonitoringStation.getAsteroidField(lines);
        Map<Point2D, Long> scores = field.stream().collect(
            Collectors.toMap(Function.identity(), a -> MonitoringStation.viewableAsteroids(a, field))
        );
        Map.Entry<Point2D, Long> best = scores.entrySet().stream()
            .max((e1, e2) -> e1.getValue().compareTo(e2.getValue()))
            .get();
        return best;
    }
}