package com.advancedprogramming;

import com.advancedprogramming.core.CityController;
import com.advancedprogramming.core.builders.CityConfigBuilder;
import com.advancedprogramming.core.CityConfig;

public class Main {
    public static void main(String[] args) {
        System.out.println("=== SmartCity System Starting ===\n");

        // Using Builder Pattern to configure the city
        CityConfig config = new CityConfigBuilder()
                .withCityName("New York City")
                .withPopulation(1000000)
                .withLightingEnabled(true)
                .withTransportEnabled(true)
                .withSecurityEnabled(true)
                .withEnergyMonitoringEnabled(true)
                .build();

        // Using Singleton Pattern to get the city controller
        CityController controller = CityController.getInstance(config);

        System.out.println("City configured: " + config.getCityName());
        System.out.println("Population: " + config.getPopulation());
        System.out.println();

        // Initialize all subsystems
        controller.initializeAllSystems();

        // Run simulation
        runSimulation(controller);

        // Shutdown
        controller.shutdownAllSystems();

        System.out.println("\n=== SmartCity System Shutdown Complete ===");
    }

    private static void runSimulation(CityController controller) {
        System.out.println("\n--- Running City Simulation ---\n");

        // Lighting management
        System.out.println("1. Testing Lighting System:");
        controller.adjustCityLighting("night");
        controller.reportLightingStatus();
        System.out.println();

        // Transport management
        System.out.println("2. Testing Transport System:");
        controller.manageTraffic("rush_hour");
        controller.reportTransportStatus();
        System.out.println();

        // Security monitoring
        System.out.println("3. Testing Security System:");
        controller.monitorSecurity();
        controller.reportSecurityStatus();
        System.out.println();

        // Energy monitoring
        System.out.println("4. Testing Energy System:");
        controller.optimizeEnergy();
        controller.reportEnergyStatus();
        System.out.println();

        // Emergency scenario
        System.out.println("5. Testing Emergency Response:");
        controller.handleEmergency("fire_alarm", "District 5");
        System.out.println();
    }
}