package com.advancedprogramming.modules.security;

public class AlarmSystem {
    private boolean armed = false;
    private boolean alarmActive = false;

    public void arm() {
        armed = true;
        System.out.println("    Alarm System armed");
    }

    public void trigger(String reason, String location) {
        if (armed) {
            alarmActive = true;
            System.out.println("    ⚠ ALARM TRIGGERED: " + reason + " at " + location);
            System.out.println("    Notifying emergency services...");
        }
    }

    public void reportStatus() {
        System.out.println("  - Alarm System: " + (armed ? "Armed" : "Disarmed") +
                (alarmActive ? " [ALERT ACTIVE]" : ""));
    }

    public void disarm() {
        armed = false;
        alarmActive = false;
    }
}
