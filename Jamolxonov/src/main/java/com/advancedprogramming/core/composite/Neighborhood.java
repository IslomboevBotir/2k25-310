package com.advancedprogramming.core.composite;

/**
 * Leaf: Individual zone that cannot contain other zones
 */

public class Neighborhood extends CityZone{
    private int deviceCount;
    private boolean active = false;

    public Neighborhood(String name, int deviceCount) {
        super(name);
        this.deviceCount = deviceCount;
    }

    @Override
    public void activate() {
        active = true;
        System.out.println("  Neighborhood '" + name + "' activated (" + deviceCount + " devices)");
    }

    @Override
    public void deactivate() {
        active = false;
        System.out.println("  Neighborhood '" + name + "' deactivated");
    }

    @Override
    public void reportStatus() {
        System.out.println("  - " + name + ": " + (active ? "Active" : "Inactive") +
                ", Devices: " + deviceCount);
    }

    @Override
    public int getDeviceCount() {
        return deviceCount;
    }
}
