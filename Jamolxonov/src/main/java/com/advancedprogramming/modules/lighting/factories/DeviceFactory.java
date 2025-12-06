package com.advancedprogramming.modules.lighting.factories;

import com.advancedprogramming.modules.lighting.Device;
import com.advancedprogramming.modules.lighting.StreetLight;
import com.advancedprogramming.modules.lighting.TrafficLight;

/**
 * FACTORY METHOD PATTERN
 * Purpose: Create different types of devices without specifying exact classes
 * Provides flexibility in device creation and extensibility
 */
public class DeviceFactory {

    public static Device createDevice(String type, String location) {
        switch (type.toLowerCase()) {
            case "streetlight":
                return new StreetLight(location);
            case "trafficlight":
                return new TrafficLight(location);
            default:
                throw new IllegalArgumentException("Unknown device type: " + type);
        }
    }
}
