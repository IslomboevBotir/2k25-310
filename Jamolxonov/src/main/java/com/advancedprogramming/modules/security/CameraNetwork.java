package com.advancedprogramming.modules.security;

public class CameraNetwork {
    private boolean active = false;
    private int camerasOnline = 0;

    public void activate() {
        active = true;
        camerasOnline = 50;
        System.out.println("    Camera Network activated: " + camerasOnline + " cameras online");
    }

    public void scan() {
        if (active) {
            System.out.println("    Scanning all camera feeds... No threats detected");
        }
    }

    public void reportStatus() {
        System.out.println("  - Camera Network: " + camerasOnline + " cameras online");
    }

    public void deactivate() {
        active = false;
        camerasOnline = 0;
    }
}
