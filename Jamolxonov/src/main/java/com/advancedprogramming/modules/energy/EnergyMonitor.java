package com.advancedprogramming.modules.energy;

public class EnergyMonitor {
    private boolean active = false;

    public void start() {
        active = true;
        System.out.println("    Energy Monitor started");
    }

    public void analyze() {
        System.out.println("    Analyzing consumption patterns...");
        System.out.println("    Recommendation: Reduce lighting in low-traffic areas");
    }

    public void reportConsumption() {
        System.out.println("  - Current Consumption: 1250 kWh");
        System.out.println("  - Efficiency: 87%");
    }

    public void stop() {
        active = false;
    }
}
