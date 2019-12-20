package org.jtodd.aoc.asteroids;

import java.awt.geom.Line2D;
import java.awt.geom.Point2D;

public class Geom {

    public static double vectorAngle(Point2D start, Point2D vertex, Point2D end) {
        Point2D tv1 = translateVector(start, vertex);
        Point2D tv2 = translateVector(end, vertex);
        double angle = Math.atan2(crossProduct(tv1, tv2), dotProduct(tv1, tv2));
        return angle < 0 ? 2 * Math.PI + angle : angle;
    }

    public static Point2D translateVector(Point2D end, Point2D vertex) {
        return new Point2D.Double(end.getX() - vertex.getX(), end.getY() - vertex.getY());
    }

    public static double dotProduct(Point2D tv1, Point2D tv2) {
        return tv1.getX() * tv2.getX() + tv1.getY() * tv2.getY();
    }

    public static double crossProduct(Point2D tv1, Point2D tv2) {
        return tv1.getX() * tv2.getY() - tv1.getY() * tv2.getX();
    }
}