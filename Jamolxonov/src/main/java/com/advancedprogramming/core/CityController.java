package com.advancedprogramming.core;

import com.advancedprogramming.modules.lighting.LightingFacade;
import com.advancedprogramming.modules.transport.TransportFacade;
import com.advancedprogramming.modules.security.SecurityFacade;
import com.advancedprogramming.modules.energy.EnergyFacade;

/**
 * SINGLETON PATTERN
 * Purpose: Ensure only one instance of CityController exists
 * Central management point for all city subsystems
 */
public class CityController {
    private static CityController instance;
    private final CityConfig config;

    // Facades for each subsystem
    private LightingFacade lightingFacade;
    private TransportFacade transportFacade;
    private SecurityFacade securityFacade;
    private EnergyFacade energyFacade;

    private boolean systemsInitialized = false;

    // Private constructor prevents external instantiation
    private CityController(CityConfig config) {
        this.config = config;
    }    /**
     * Thread-safe singleton instance retrieval
     */
    public static synchronized CityController getInstance(CityConfig config) {
        if (instance == null) {
            instance = new CityController(config);
        }
        return instance;
    }

    public static CityController getInstance() {
        if (instance == null) {
            throw new IllegalStateException("CityController not initialized with config");
        }
        return instance;
    }

    public void initializeAllSystems() {
        if (systemsInitialized) {
            System.out.println("Systems already initialized!");
            return;
        }

        System.out.println("Initializing city subsystems...");

        if (config.isLightingEnabled()) {
            lightingFacade = new LightingFacade();
            lightingFacade.initialize();
        }

        if (config.isTransportEnabled()) {
            transportFacade = new TransportFacade();
            transportFacade.initialize();
        }

        if (config.isSecurityEnabled()) {
            securityFacade = new SecurityFacade();
            securityFacade.initialize();
        }

        if (config.isEnergyMonitoringEnabled()) {
            energyFacade = new EnergyFacade();
            energyFacade.initialize();
        }

        systemsInitialized = true;
        System.out.println("All subsystems initialized successfully!\n");
    }

    // Lighting operations
    public void adjustCityLighting(String timeOfDay) {
        if (lightingFacade != null) {
            lightingFacade.adjustLighting(timeOfDay);
        }
    }

    public void reportLightingStatus() {
        if (lightingFacade != null) {
            lightingFacade.getStatus();
        }
    }

    // Transport operations
    public void manageTraffic(String condition) {
        if (transportFacade != null) {
            transportFacade.manageTraffic(condition);
        }
    }

    public void reportTransportStatus() {
        if (transportFacade != null) {
            transportFacade.getStatus();
        }
    }

    // Security operations
    public void monitorSecurity() {
        if (securityFacade != null) {
            securityFacade.startMonitoring();
        }
    }

    public void reportSecurityStatus() {
        if (securityFacade != null) {
            securityFacade.getStatus();
        }
    }

    // Energy operations
    public void optimizeEnergy() {
        if (energyFacade != null) {
            energyFacade.optimizeConsumption();
        }
    }

    public void reportEnergyStatus() {
        if (energyFacade != null) {
            energyFacade.getStatus();
        }
    }

    // Emergency handling
    public void handleEmergency(String emergencyType, String location) {
        System.out.println("EMERGENCY: " + emergencyType + " at " + location);

        if (securityFacade != null) {
            securityFacade.handleEmergency(emergencyType, location);
        }

        if (lightingFacade != null) {
            lightingFacade.enableEmergencyMode(location);
        }

        if (transportFacade != null) {
            transportFacade.clearRoute(location);
        }
    }

    public void shutdownAllSystems() {
        System.out.println("\nShutting down all subsystems...");

        if (lightingFacade != null) lightingFacade.shutdown();
        if (transportFacade != null) transportFacade.shutdown();
        if (securityFacade != null) securityFacade.shutdown();
        if (energyFacade != null) energyFacade.shutdown();

        systemsInitialized = false;
    }
}
