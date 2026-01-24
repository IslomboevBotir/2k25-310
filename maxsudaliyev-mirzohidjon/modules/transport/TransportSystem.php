<?php
namespace modules\transport;

/**
 * Transport subsystem (simple simulation).
 */
class TransportSystem
{
    private array $routes = []; // routeName => dispatchedCount

    public function addRoute(string $name): string
    {
        $name = trim($name);
        if ($name === '') return "Route nomi bo'sh.\n";
        if (!isset($this->routes[$name])) $this->routes[$name] = 0;
        return "Route qo'shildi: {$name}\n";
    }

    public function dispatchBus(string $routeName): string
    {
        if (!isset($this->routes[$routeName])) return "Route topilmadi: {$routeName}\n";
        $this->routes[$routeName]++;
        return "Bus dispatched on '{$routeName}'. Total: {$this->routes[$routeName]}\n";
    }

    public function status(): string
    {
        if (empty($this->routes)) return "No routes.\n";
        $s = "Routes:\n";
        foreach ($this->routes as $r => $c) {
            $s .= "- {$r}: dispatched={$c}\n";
        }
        return $s;
    }
}
