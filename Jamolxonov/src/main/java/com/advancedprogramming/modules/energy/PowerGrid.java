package com.advancedprogramming.modules.energy;

public class PowerGrid {
    private boolean active = false;
    private double currentLoad = 0.0;

    public void start() {
        active = true;
        currentLoad = 75.5;
        System.out.println("    Power Grid online");
    }

    public void balance() {
        System.out.println("    Balancing power distribution across sectors");
        currentLoad = 68.2;
    }

    public void reportStatus() {
        System.out.println("  - Power Grid: Load at " + currentLoad + "%");
    }

    public void stop() {
        active = false;
    }
}
