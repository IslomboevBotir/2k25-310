package com.advancedprogramming.core.composite;

import java.util.ArrayList;
import java.util.List;
/**
 * COMPOSITE PATTERN (Bonus - 6th pattern)
 * Purpose: Treat individual zones and groups of zones uniformly
 * Allows hierarchical city structure (City -> Districts -> Neighborhoods)
 */
public abstract class CityZone {
    protected String name;

    public CityZone(String name) {
        this.name = name;
    }

    public abstract void activate();
    public abstract void deactivate();
    public abstract void reportStatus();
    public abstract int getDeviceCount();
}
