package org.jtodd.ss;

import java.util.ArrayList;
import java.util.List;
import java.util.stream.Collectors;

public class Orbiter {

    private String name;
    private List<Orbiter> satellites;

    public Orbiter(String name) {
        this.name = name;
        satellites = new ArrayList<>();
    }

    public void addOrbiter(Orbiter o) {
        satellites.add(o);
    }

    public List<Orbiter> getSatellites() {
        return satellites;
    }

    public int countSatellites() {
        int satelliteCount = 0;
        for (Orbiter o : satellites) {
            satelliteCount += o.countSatellites();
        }
        return satelliteCount + satellites.size();
    }

    @Override
    public int hashCode() {
        return name.hashCode();
    }

    @Override
    public String toString() {
        return String.format(
            "%s[%s]",
            this.name,
            this.satellites.stream()
                .map(Orbiter::toString)
                .collect(Collectors.joining(","))
        );
    }
}