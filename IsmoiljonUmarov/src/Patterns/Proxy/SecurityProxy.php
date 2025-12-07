<?php
declare(strict_types=1);

namespace SmartCity\Patterns\Proxy;

use SmartCity\Core\CentralHub;

class SecurityProxy implements SystemAccessInterface
{
    private ?RealSystemAccess $realSystem = null;

    public function __construct(
        private string $userRole
    ) {}

    public function grantAccess(string $password): bool
    {
        if ($this->userRole !== 'admin') {
            CentralHub::getInstance()->log('SECURITY', 'Access Denied: Insufficient Role');
            return false;
        }

        if ($password !== 'root123') {
            CentralHub::getInstance()->log('SECURITY', 'Access Denied: Wrong Password');
            return false;
        }

        if ($this->realSystem === null) {
            $this->realSystem = new RealSystemAccess();
        }

        return $this->realSystem->grantAccess($password);
    }
}