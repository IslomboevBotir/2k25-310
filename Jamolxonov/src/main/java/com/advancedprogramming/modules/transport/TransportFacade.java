package com.advancedprogramming.modules.transport;

/**
 * FACADE PATTERN
 * Purpose: Simplified interface for transport management
 */
public class TransportFacade {
    private TrafficManager trafficManager;
    private PublicTransportSystem publicTransport;

    public void initialize() {
        System.out.println("  [Transport] Initializing transport system...");
        trafficManager = new TrafficManager();
        publicTransport = new PublicTransportSystem();
        trafficManager.start();
        publicTransport.start();
        System.out.println("  [Transport] System initialized");
    }

    public void manageTraffic(String condition) {
        System.out.println("Managing traffic for condition: " + condition);
        trafficManager.adjustForCondition(condition);
        publicTransport.adjustSchedule(condition);
    }

    public void clearRoute(String location) {
        System.out.println("  [Transport] Clearing route to: " + location);
        trafficManager.clearEmergencyRoute(location);
    }

    public void getStatus() {
        System.out.println("Transport System Status:");
        trafficManager.reportStatus();
        publicTransport.reportStatus();
    }

    public void shutdown() {
        System.out.println("  [Transport] Shutting down...");
        trafficManager.stop();
        publicTransport.stop();
    }
}
