package com.advancedprogramming.core;

/**
 * Configuration class for SmartCity
 * Used with Builder Pattern
 */
public class CityConfig {
    private final String cityName;
    private final int population;
    private final boolean lightingEnabled;
    private final boolean transportEnabled;
    private final boolean securityEnabled;
    private final boolean energyMonitoringEnabled;

    public CityConfig(String cityName, int population, boolean lightingEnabled,
                      boolean transportEnabled, boolean securityEnabled,
                      boolean energyMonitoringEnabled) {
        this.cityName = cityName;
        this.population = population;
        this.lightingEnabled = lightingEnabled;
        this.transportEnabled = transportEnabled;
        this.securityEnabled = securityEnabled;
        this.energyMonitoringEnabled = energyMonitoringEnabled;
    }

    public String getCityName() { return cityName; }
    public int getPopulation() { return population; }
    public boolean isLightingEnabled() { return lightingEnabled; }
    public boolean isTransportEnabled() { return transportEnabled; }
    public boolean isSecurityEnabled() { return securityEnabled; }
    public boolean isEnergyMonitoringEnabled() { return energyMonitoringEnabled; }
}

