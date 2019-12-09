package org.jtodd.ss;

import java.io.IOException;
import java.nio.file.FileSystems;
import java.nio.file.Files;
import java.nio.file.Path;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.HashSet;
import java.util.List;
import java.util.Map;
import java.util.Set;
import java.util.stream.Collectors;

public class SolarSystem {

    private Map<String, Orbiter> bodies;

    public SolarSystem() {
        bodies = new HashMap<>();
    }

    private void readBodies(String filename) {
        Path p = FileSystems.getDefault().getPath(filename);
        try {
            for (String line : Files.readAllLines(p)) {
                String [] bodyNames = line.split("\\)");
                String parentName = bodyNames[0].trim();
                String satelliteName = bodyNames[1].trim();
                bodies.putIfAbsent(parentName, new Orbiter(parentName));
                bodies.putIfAbsent(satelliteName, new Orbiter(satelliteName));
                Orbiter parent = bodies.get(parentName);
                Orbiter satellite = bodies.get(satelliteName);
                parent.addSatellite(satellite);
                satellite.setParent(parent);
            }
        } catch (IOException e) {
            e.printStackTrace();
            throw new RuntimeException(e);
        }
    }

    // I used the discussion and pseudocode of Dijkstra's Algorithm from
    // https://www.vogella.com/tutorials/JavaAlgorithmsDijkstra/article.html
    public int distanceBetween(Orbiter from, Orbiter to) {
        Map<Orbiter, Integer> distance = new HashMap<>();
        for (Orbiter o : bodies.values()) {
            if (o == from) {
                distance.put(o, 0);
            } else {
                distance.put(o, Integer.MAX_VALUE);
            }
        }
        Set<Orbiter> unsettled = new HashSet<>();
        unsettled.add(from);
        Set<Orbiter> settled = new HashSet<>();
        Orbiter evaluate;

        while (!unsettled.isEmpty()) {
            evaluate = unsettled.stream()
                .min((e1, e2) -> distance.get(e1).compareTo(distance.get(e2)))
                .get();
            unsettled.remove(evaluate);
            settled.add(evaluate);
            evaluateNeighbors(evaluate, distance, unsettled, settled);
        }

        return distance.get(to);
    }

    private void evaluateNeighbors(Orbiter evaluate, Map<Orbiter, Integer> distance, Set<Orbiter> unsettled, Set<Orbiter> settled) {
        List<Orbiter> neighbors = new ArrayList<>(evaluate.getSatellites());
        if (evaluate.getParent() != null) {
            neighbors.add(evaluate.getParent());
        }
        List<Orbiter> notEvaluatedNeighbors = neighbors
            .stream()
            .filter(n -> !settled.contains(n))
            .collect(Collectors.toList());
        int newDistance = distance.get(evaluate) + 1;
        for (Orbiter n : notEvaluatedNeighbors) {
            if (distance.get(n) > newDistance) {
                distance.put(n, newDistance);
                unsettled.add(n);
            }
        }
    }

    public static void main(String [] args) {
        String filename = args[0];
        SolarSystem ss = new SolarSystem();
        ss.readBodies(filename);
        int totalOrbits = 0;
        for (Orbiter o : ss.bodies.values()) {
            totalOrbits += o.countSatellites();
        }
        System.out.printf("System has %d direct and indirect orbits\n", totalOrbits);

        Orbiter you = ss.bodies.get("YOU");
        Orbiter santa = ss.bodies.get("SAN");
        System.out.printf("You are currently orbiting %s\n", you.getParent().getName());
        System.out.printf("Santa is currently orbiting %s\n", santa.getParent().getName());
        int jumps = ss.distanceBetween(you.getParent(), santa.getParent());
        System.out.printf("There are %d jumps between Santa and you\n", jumps);
        System.exit(0);
    }
}