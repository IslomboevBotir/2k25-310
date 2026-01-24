<?php
namespace core\proxy;

use modules\security\SecuritySystem;

/**
 * PATTERN: Proxy
 * Purpose: Access control (admin-only actions) + logging wrapper.
 */
class SecurityProxy implements ISecurity
{
    private SecuritySystem $real;
    /** @var callable returns current role string */
    private $roleProvider;

    public function __construct(SecuritySystem $real, callable $roleProvider)
    {
        $this->real = $real;
        $this->roleProvider = $roleProvider;
    }

    private function role(): string
    {
        return (string)call_user_func($this->roleProvider);
    }

    private function denyIfNotAdmin(): ?string
    {
        if ($this->role() !== 'admin') {
            return "ACCESS DENIED: admin kerak.\n";
        }
        return null;
    }

    public function arm(): string
    {
        if ($msg = $this->denyIfNotAdmin()) return $msg;
        return "[Proxy Log] arm() called\n" . $this->real->arm();
    }

    public function disarm(): string
    {
        if ($msg = $this->denyIfNotAdmin()) return $msg;
        return "[Proxy Log] disarm() called\n" . $this->real->disarm();
    }

    public function viewCameras(): string
    {
        // camera view can be allowed to user too (demo), or lock it as admin-only:
        // if ($msg = $this->denyIfNotAdmin()) return $msg;
        return "[Proxy Log] viewCameras() called\n" . $this->real->viewCameras();
    }

    public function status(): string
    {
        return $this->real->status();
    }
}
