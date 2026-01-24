package com.advancedprogramming.modules.energy;

import com.advancedprogramming.core.adapters.WeatherServiceAdapter;

/**
 * FACADE PATTERN
 * Purpose: Simplified energy management interface
 */
public class EnergyFacade {
    private PowerGrid powerGrid;
    private EnergyMonitor monitor;
    private WeatherServiceAdapter weatherAdapter;

    public void initialize() {
        System.out.println("  [Energy] Initializing energy system...");
        powerGrid = new PowerGrid();
        monitor = new EnergyMonitor();
        weatherAdapter = new WeatherServiceAdapter();

        powerGrid.start();
        monitor.start();
        System.out.println("  [Energy] System initialized");
    }

    public void optimizeConsumption() {
        System.out.println("Optimizing energy consumption...");

        // Using Adapter to get weather data from external service
        String weather = weatherAdapter.getCurrentConditions();
        System.out.println("  Weather conditions: " + weather);

        monitor.analyze();
        powerGrid.balance();
    }

    public void getStatus() {
        System.out.println("Energy System Status:");
        powerGrid.reportStatus();
        monitor.reportConsumption();
    }

    public void shutdown() {
        System.out.println("  [Energy] Shutting down...");
        monitor.stop();
        powerGrid.stop();
    }
}
