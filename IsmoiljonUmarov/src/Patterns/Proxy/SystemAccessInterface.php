<?php
declare(strict_types=1);

namespace SmartCity\Patterns\Proxy;

interface SystemAccessInterface
{
    public function grantAccess(string $password): bool;
}