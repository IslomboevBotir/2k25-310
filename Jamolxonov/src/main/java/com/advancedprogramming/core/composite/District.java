package com.advancedprogramming.core.composite;

import java.util.ArrayList;
import java.util.List;
/**
 * Composite: Can contain other zones
 */
public class District extends CityZone {
    private List<CityZone> zones = new ArrayList<>();

    public District(String name) {
        super(name);
    }

    public void addZone(CityZone zone) {
        zones.add(zone);
    }

    public void removeZone(CityZone zone) {
        zones.remove(zone);
    }

    @Override
    public void activate() {
        System.out.println("Activating district: " + name);
        for (CityZone zone : zones) {
            zone.activate();
        }
    }

    @Override
    public void deactivate() {
        System.out.println("Deactivating district: " + name);
        for (CityZone zone : zones) {
            zone.deactivate();
        }
    }

    @Override
    public void reportStatus() {
        System.out.println("District " + name + " Status:");
        for (CityZone zone : zones) {
            zone.reportStatus();
        }
    }

    @Override
    public int getDeviceCount() {
        int total = 0;
        for (CityZone zone : zones) {
            total += zone.getDeviceCount();
        }
        return total;
    }
}
