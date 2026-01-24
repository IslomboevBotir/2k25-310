<?php
namespace core\proxy;

/**
 * PATTERN: Proxy (Subject interface)
 * Purpose: Provide same interface for real security and proxy security.
 */
interface ISecurity
{
    public function arm(): string;
    public function disarm(): string;
    public function viewCameras(): string;
    public function status(): string;
}
