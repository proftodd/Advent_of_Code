package org.jtodd.aoc.asteroids;

import java.awt.geom.Line2D;
import java.awt.geom.Point2D;
import java.util.Arrays;
import java.util.List;

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
}