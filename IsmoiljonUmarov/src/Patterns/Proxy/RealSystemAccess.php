<?php
declare(strict_types=1);

namespace SmartCity\Patterns\Proxy;

class RealSystemAccess implements SystemAccessInterface
{
    public function grantAccess(string $password): bool
    {
        return true;
    }
}