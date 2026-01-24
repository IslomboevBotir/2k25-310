package com.advancedprogramming.modules.lighting;

public class TrafficLight implements Device{
    private boolean isOn = false;
    private int intensity = 100;
    private final String intersection;
    private String currentSignal = "RED";

    public TrafficLight(String intersection) {
        this.intersection = intersection;
    }

    @Override
    public void turnOn() {
        isOn = true;
        System.out.println("TrafficLight at " + intersection + " activated");
    }

    @Override
    public void turnOff() {
        isOn = false;
        System.out.println("TrafficLight at " + intersection + " deactivated");
    }

    @Override
    public void adjustIntensity(int level) {
        intensity = Math.max(0, Math.min(100, level));
    }

    public void changeSignal(String signal) {
        if (isOn) {
            currentSignal = signal;
            System.out.println("TrafficLight at " + intersection + " changed to: " + signal);
        }
    }

    @Override
    public String getStatus() {
        return String.format("TrafficLight[%s]: %s, Signal: %s",
                intersection, isOn ? "ON" : "OFF", currentSignal);
    }
}
