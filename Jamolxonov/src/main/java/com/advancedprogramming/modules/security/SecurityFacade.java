package com.advancedprogramming.modules.security;

import com.advancedprogramming.core.proxy.SecurityProxy;
/**
 * FACADE PATTERN
 * Purpose: Simplified security subsystem management
 */
public class SecurityFacade {
    private CameraNetwork cameraNetwork;
    private AlarmSystem alarmSystem;
    private SecurityProxy securityProxy;

    public void initialize() {
        System.out.println("  [Security] Initializing security system...");
        cameraNetwork = new CameraNetwork();
        alarmSystem = new AlarmSystem();

        // Using Proxy for controlled access to critical security operations
        securityProxy = new SecurityProxy(alarmSystem);

        cameraNetwork.activate();
        alarmSystem.arm();
        System.out.println("  [Security] System initialized");
    }

    public void startMonitoring() {
        System.out.println("Starting security monitoring...");
        cameraNetwork.scan();
    }

    public void handleEmergency(String type, String location) {
        System.out.println("  [Security] Responding to " + type + " at " + location);
        securityProxy.triggerAlarm(type, location);
    }

    public void getStatus() {
        System.out.println("Security System Status:");
        cameraNetwork.reportStatus();
        alarmSystem.reportStatus();
    }

    public void shutdown() {
        System.out.println("  [Security] Shutting down...");
        cameraNetwork.deactivate();
        alarmSystem.disarm();
    }
}
