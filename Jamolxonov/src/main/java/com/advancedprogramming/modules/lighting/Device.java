package com.advancedprogramming.modules.lighting;

/**
 * Base interface for devices
 * Used in Factory Method pattern
 */
public interface Device {
    void turnOn();
    void turnOff();
    void adjustIntensity(int level);
    String getStatus();
}
