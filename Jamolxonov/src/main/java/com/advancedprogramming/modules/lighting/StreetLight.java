package com.advancedprogramming.modules.lighting;

public class StreetLight implements Device{
    private boolean isOn = false;
    private int intensity = 0;
    private final String location;

    public StreetLight(String location) {
        this.location = location;
    }

    @Override
    public void turnOn() {
        isOn = true;
        intensity = 100;
        System.out.println("StreetLight at " + location + " turned ON");
    }

    @Override
    public void turnOff() {
        isOn = false;
        intensity = 0;
        System.out.println("StreetLight at " + location + " turned OFF");
    }

    @Override
    public void adjustIntensity(int level) {
        if (isOn) {
            intensity = Math.max(0, Math.min(100, level));
            System.out.println("StreetLight at " + location + " intensity: " + intensity + "%");
        }
    }

    @Override
    public String getStatus() {
        return String.format("StreetLight[%s]: %s, Intensity: %d%%",
                location, isOn ? "ON" : "OFF", intensity);
    }
}
