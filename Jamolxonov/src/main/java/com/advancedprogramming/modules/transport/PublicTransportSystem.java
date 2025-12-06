package com.advancedprogramming.modules.transport;

public class PublicTransportSystem {
    private boolean active = false;
    private int busesActive = 0;

    public void start() {
        active = true;
        busesActive = 15;
        System.out.println("    Public Transport System started with " + busesActive + " buses");
    }

    public void adjustSchedule(String condition) {
        if (condition.equalsIgnoreCase("rush_hour")) {
            busesActive = 25;
            System.out.println("    Increased buses to " + busesActive + " for rush hour");
        }
    }

    public void reportStatus() {
        System.out.println("  - Public Transport: " + busesActive + " buses active");
    }

    public void stop() {
        active = false;
        busesActive = 0;
    }
}
