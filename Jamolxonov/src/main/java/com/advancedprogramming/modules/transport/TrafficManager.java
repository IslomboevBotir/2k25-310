package com.advancedprogramming.modules.transport;

public class TrafficManager {
    private boolean active = false;
    private String currentMode = "normal";

    public void start() {
        active = true;
        System.out.println("    Traffic Manager started");
    }

    public void adjustForCondition(String condition) {
        currentMode = condition;
        switch (condition.toLowerCase()) {
            case "rush_hour":
                System.out.println("    Optimizing traffic flow for rush hour");
                System.out.println("    Extended green lights on main routes");
                break;
            case "normal":
                System.out.println("    Standard traffic management active");
                break;
            case "emergency":
                System.out.println("    Emergency traffic protocol activated");
                break;
        }
    }

    public void clearEmergencyRoute(String location) {
        System.out.println("    Creating emergency corridor to: " + location);
    }

    public void reportStatus() {
        System.out.println("  - Traffic Manager: " + (active ? "Active" : "Inactive") +
                ", Mode: " + currentMode);
    }

    public void stop() {
        active = false;
    }
}
