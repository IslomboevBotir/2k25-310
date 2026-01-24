package com.advancedprogramming.core.builders;

import com.advancedprogramming.core.CityConfig;

/**
 * BUILDER PATTERN
 * Purpose: Construct complex CityConfig objects step by step
 * Allows flexible configuration without telescoping constructors
 */
public class CityConfigBuilder {
    private String cityName = "DefaultCity";
    private int population = 100000;
    private boolean lightingEnabled = true;
    private boolean transportEnabled = true;
    private boolean securityEnabled = true;
    private boolean energyMonitoringEnabled = true;

    public CityConfigBuilder withCityName(String cityName) {
        this.cityName = cityName;
        return this;
    }

    public CityConfigBuilder withPopulation(int population) {
        this.population = population;
        return this;
    }

    public CityConfigBuilder withLightingEnabled(boolean enabled) {
        this.lightingEnabled = enabled;
        return this;
    }

    public CityConfigBuilder withTransportEnabled(boolean enabled) {
        this.transportEnabled = enabled;
        return this;
    }

    public CityConfigBuilder withSecurityEnabled(boolean enabled) {
        this.securityEnabled = enabled;
        return this;
    }

    public CityConfigBuilder withEnergyMonitoringEnabled(boolean enabled) {
        this.energyMonitoringEnabled = enabled;
        return this;
    }

    public CityConfig build() {
        return new CityConfig(cityName, population, lightingEnabled,
                transportEnabled, securityEnabled, energyMonitoringEnabled);
    }
}
