<?php
require_once __DIR__ . '/../../modules/Security/Camera.php';

class SecurityProxy
{
    private $camera;

    public function __construct()
    {
        $this->camera = new Camera();
    }

    public function accessControl($role)
    {
        if ($role === 'admin') {
            echo $this->camera->monitor();
        } else {
            echo "Access denied. Admins only.\n";
        }
    }
}
