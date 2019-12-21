package org.jtodd.aoc.asteroids;

import java.awt.geom.Point2D;
import java.io.IOException;
import java.nio.file.FileSystems;
import java.nio.file.Files;
import java.nio.file.Path;
import java.util.ArrayList;
import java.util.Collection;
import java.util.Collections;
import java.util.Iterator;
import java.util.List;
import java.util.PriorityQueue;
import java.util.TreeMap;
import java.util.function.Function;
import java.util.stream.Collectors;
import java.util.stream.Stream;

public class LaserStation {

    public static void main(String [] args) {
        Path p = FileSystems.getDefault().getPath(args[0]);
        List<String> lines;
        try {
            lines = Files.readAllLines(p);
        } catch (IOException e) {
            throw new RuntimeException(e);
        }
        List<Point2D> field = MonitoringStation.getAsteroidField(lines);
        int laserX = Integer.parseInt(args[1]);
        int laserY = Integer.parseInt(args[2]);
        Point2D vertex = new Point2D.Double(laserX, laserY);
        field.remove(vertex);
        Point2D start = new Point2D.Double(vertex.getX(), -1);

        TreeMap<Double, PriorityQueue<Point2D>> angles = new TreeMap<>();
        for (Point2D asteroid : field) {
            double angle = Geom.vectorAngle(start, vertex, asteroid);
            if (!angles.containsKey(angle)) {
                angles.put(angle, new PriorityQueue<Point2D>((p1, p2) -> Double.compare(p1.distance(vertex), p2.distance(vertex))));
            }
            angles.get(angle).add(asteroid);
        }

        List<Point2D> destroyed = new ArrayList<>();
        while (angles.size() > 0) {
            for (Iterator<Double> i = angles.keySet().iterator();
                 i.hasNext();
            ) {
                Double angle = i.next();
                Point2D nextPoint = angles.get(angle).poll();
                if (angles.get(angle).size() == 0) {
                    i.remove();
                }
                destroyed.add(nextPoint);
            }
        }
        for (int i = 0; i < destroyed.size(); ++i) {
            System.out.printf("%3d: (%.0f,%.0f)\n", i + 1, destroyed.get(i).getX(), destroyed.get(i).getY());
        }
    }
}