<?php
namespace core\builders;

class CityReport
{
    private array $lines;

    public function __construct(array $lines)
    {
        $this->lines = $lines;
    }

    public function __toString(): string
    {
        return implode("\n", $this->lines) . "\n";
    }
}
