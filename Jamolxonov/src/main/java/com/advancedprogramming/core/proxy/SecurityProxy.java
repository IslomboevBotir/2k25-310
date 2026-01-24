package com.advancedprogramming.core.proxy;

import com.advancedprogramming.modules.security.AlarmSystem;
/**
 * PROXY PATTERN
 * Purpose: Control access to sensitive alarm system operations
 * Adds logging, authentication, and validation before critical operations
 * Provides additional security layer for sensitive subsystems
 */
public class SecurityProxy {
    private AlarmSystem realAlarmSystem;
    private boolean authorized = true; // In a real system, would check credentials

    public SecurityProxy(AlarmSystem alarmSystem) {
        this.realAlarmSystem = alarmSystem;
    }

    public void triggerAlarm(String reason, String location) {
        // Pre-processing: Log the access attempt
        logAccess("ALARM_TRIGGER", reason, location);

        // Authorization check
        if (!authorized) {
            System.out.println("    ⛔ UNAUTHORIZED: Alarm trigger denied");
            return;
        }

        // Validation
        if (reason == null || reason.isEmpty()) {
            System.out.println("    ⚠ INVALID: Alarm reason required");
            return;
        }

        // Forward to a real object
        realAlarmSystem.trigger(reason, location);

        // Post-processing: Additional logging
        logAccess("ALARM_TRIGGERED", reason, location);
    }

    private void logAccess(String action, String reason, String location) {
        System.out.println("    [SECURITY LOG] " + action + " - Reason: " +
                reason + ", Location: " + location +
                ", Time: " + System.currentTimeMillis());
    }

    public void setAuthorized(boolean authorized) {
        this.authorized = authorized;
    }
}
