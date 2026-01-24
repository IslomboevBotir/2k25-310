package com.advancedprogramming.modules.lighting;

import com.advancedprogramming.modules.lighting.factories.DeviceFactory;
import java.util.ArrayList;
import java.util.List;

/**
 * FACADE PATTERN
 * Purpose: Provide simplified interface to a complex lighting subsystem
 * Hides complexity of device management from the main controller
 */
public class LightingFacade {
    private List<Device> streetLights;
    private List<Device> trafficLights;

    public LightingFacade() {
        streetLights = new ArrayList<>();
        trafficLights = new ArrayList<>();
    }

    public void initialize() {
        System.out.println("  [Lighting] Initializing lighting system...");

        // Create streetlights
        streetLights.add(DeviceFactory.createDevice("streetlight", "Main St"));
        streetLights.add(DeviceFactory.createDevice("streetlight", "Park Ave"));
        streetLights.add(DeviceFactory.createDevice("streetlight", "5th Avenue"));

        // Create traffic lights
        trafficLights.add(DeviceFactory.createDevice("trafficlight", "Main-Park Intersection"));
        trafficLights.add(DeviceFactory.createDevice("trafficlight", "Central Square"));

        // Turn on all devices
        for (Device light : streetLights) {
            light.turnOn();
        }
        for (Device light : trafficLights) {
            light.turnOn();
        }

        System.out.println("  [Lighting] System initialized with " +
                streetLights.size() + " street lights and " +
                trafficLights.size() + " traffic lights");
    }

    public void adjustLighting(String timeOfDay) {
        System.out.println("Adjusting lighting for: " + timeOfDay);

        switch (timeOfDay.toLowerCase()) {
            case "day":
                for (Device light : streetLights) {
                    light.adjustIntensity(30);
                }
                break;
            case "night":
                for (Device light : streetLights) {
                    light.adjustIntensity(100);
                }
                break;
            case "evening":
                for (Device light : streetLights) {
                    light.adjustIntensity(70);
                }
                break;
        }
    }

    public void enableEmergencyMode(String location) {
        System.out.println("  [Lighting] Emergency mode enabled near: " + location);
        for (Device light : streetLights) {
            light.adjustIntensity(100);
        }
    }

    public void getStatus() {
        System.out.println("Lighting System Status:");
        for (Device light : streetLights) {
            System.out.println("  - " + light.getStatus());
        }
        for (Device light : trafficLights) {
            System.out.println("  - " + light.getStatus());
        }
    }

    public void shutdown() {
        System.out.println("  [Lighting] Shutting down...");
        for (Device light : streetLights) {
            light.turnOff();
        }
        for (Device light : trafficLights) {
            light.turnOff();
        }
    }
}
