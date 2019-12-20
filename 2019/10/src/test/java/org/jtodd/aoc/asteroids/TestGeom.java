package org.jtodd.aoc.asteroids;

import java.awt.geom.Point2D;

import org.junit.Assert;
import org.junit.Test;

public class TestGeom {

    @Test
    public void testVectorAngle() {
        Point2D vertex = new Point2D.Float(8.0f, 3.0f);
        Point2D start  = new Point2D.Float(8.0f, -1);
        Point2D [] ends = new Point2D [] {
            new Point2D.Float(8.0f, 0.0f),
            new Point2D.Float(9.0f, 0.0f),
            new Point2D.Float(9.0f, 1.0f),
            new Point2D.Float(10.0f, 0.0f),
            new Point2D.Float(9.0f, 2.0f),
            new Point2D.Float(11.0f, 1.0f),
            new Point2D.Float(12.0f, 1.0f),
            new Point2D.Float(11.0f, 2.0f),
            new Point2D.Float(15.0f, 1.0f),
            new Point2D.Float(12.0f, 2.0f),
            new Point2D.Float(13.0f, 2.0f),
            new Point2D.Float(14.0f, 2.0f),
            new Point2D.Float(15.0f, 2.0f),
            new Point2D.Float(12.0f, 3.0f),
            new Point2D.Float(16.0f, 4.0f),
            new Point2D.Float(15.0f, 4.0f),
            new Point2D.Float(10.0f, 4.0f),
            new Point2D.Float(4.0f, 4.0f),
            new Point2D.Float(3.0f, 4.0f),
            new Point2D.Float(3.0f, 3.0f),
            new Point2D.Float(0.0f, 2.0f),
            new Point2D.Float(1.0f, 2.0f),
            new Point2D.Float(0.0f, 1.0f),
            new Point2D.Float(1.0f, 1.0f),
            new Point2D.Float(5.0f, 2.0f),
            new Point2D.Float(1.0f, 0.0f),
            new Point2D.Float(5.0f, 1.0f),
            new Point2D.Float(6.0f, 1.0f),
            new Point2D.Float(6.0f, 0.0f),
            new Point2D.Float(7.0f, 0.0f)
        };

        Assert.assertEquals(0.0f, Geom.vectorAngle(start, vertex, ends[0]), 1e-6);
        for (int i = 1; i < ends.length; ++i) {
            Assert.assertTrue(Geom.vectorAngle(start, vertex, ends[i]) > Geom.vectorAngle(start, vertex, ends[i - 1]));
        }
    }
}