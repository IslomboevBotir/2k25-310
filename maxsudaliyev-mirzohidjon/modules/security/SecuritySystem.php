<?php
namespace modules\security;

use core\proxy\ISecurity;

/**
 * Security subsystem (Real Subject).
 */
class SecuritySystem implements ISecurity
{
    private bool $armed = false;
    private array $cameras = ["Cam-01", "Cam-02", "Cam-03"];

    public function arm(): string
    {
        $this->armed = true;
        return "Security ARMED.\n";
    }

    public function disarm(): string
    {
        $this->armed = false;
        return "Security DISARMED.\n";
    }

    public function viewCameras(): string
    {
        $s = "Cameras:\n";
        foreach ($this->cameras as $c) {
            $s .= "- {$c} : OK\n";
        }
        return $s;
    }

    public function status(): string
    {
        return "armed=" . ($this->armed ? "true" : "false") . "\n";
    }
}
