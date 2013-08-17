
param($psakeScriptToDraw, $outputDirectory)

import-module .\psake.psm1

$properties = @{};

if ($psakeScriptToDraw) {
    $properties.psakeScriptToDraw = $psakeScriptToDraw;
}

if ($outputDirectory) {
    $properties.outputDirectory = $outputDirectory;
}

.\psake.ps1 .\default.ps1 -properties $properties