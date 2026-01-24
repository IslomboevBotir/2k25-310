<?php
/**
 * Simple autoloader for this project.
 * Keeps code modular & readable.
 */
spl_autoload_register(function ($class) {
    $baseDir = __DIR__ . '/../';
    $class = str_replace('\\', '/', $class);

    $paths = [
        $baseDir . $class . '.php',
    ];

    foreach ($paths as $p) {
        if (file_exists($p)) {
            require_once $p;
            return;
        }
    }
});
