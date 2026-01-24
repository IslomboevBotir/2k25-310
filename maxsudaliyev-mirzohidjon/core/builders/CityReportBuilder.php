<?php
namespace core\builders;

/**
 * PATTERN: Builder
 * Purpose: Step-by-step build of a CityReport (flexible report creation).
 */
class CityReportBuilder
{
    private array $lines = [];

    public function addHeader(string $title): self
    {
        $this->lines[] = "==============================";
        $this->lines[] = $title;
        $this->lines[] = "==============================";
        return $this;
    }

    public function addLine(string $line): self
    {
        $this->lines[] = $line;
        return $this;
    }

    public function build(): CityReport
    {
        return new CityReport($this->lines);
    }
}
