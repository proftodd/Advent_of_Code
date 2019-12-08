package org.jtodd.ss;

import java.io.IOException;
import java.nio.file.FileSystems;
import java.nio.file.Files;
import java.nio.file.Path;
import java.util.HashMap;
import java.util.Map;

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
                String body = bodyNames[0].trim();
                String satellite = bodyNames[1].trim();
                bodies.putIfAbsent(body, new Orbiter(body));
                bodies.putIfAbsent(satellite, new Orbiter(satellite));
                bodies.get(body).addOrbiter(bodies.get(satellite));
            }
        } catch (IOException e) {
            e.printStackTrace();
            throw new RuntimeException(e);
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
        System.out.printf("System has %d orbits\n", totalOrbits);
        System.exit(0);
    }
}