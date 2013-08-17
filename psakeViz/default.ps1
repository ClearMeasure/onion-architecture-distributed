
properties {

	$sourceDirectory = "."
    $psakeScriptToDraw = ".\default.ps1"
    $outputDirectory = ".\output\"
}

task default -depends Instructions, Draw

task Instructions -precondition { $psakeScriptToDraw -eq ".\psakeViz.ps1" } {

    "`
    You can specify any psake script to be drawn as the first parameter to runme.ps1`
" | write-host -fore green

}

task Draw {

    VisualizePSakeScript $sourceDirectory $psakeScriptToDraw $outputDirectory
}

function VisualizePSakeScript($sourceDirectory, $psakeScriptToDraw, $outputDirectory) {

    $psakeScriptFileinfo = (New-Object -TypeName "System.IO.FileInfo" -ArgumentList (gi $psakeScriptToDraw))

    $tasks = LoadTasks $psakeScriptFileinfo
    
    $global:tasks = $tasks
    
	$result = DrawTasks $tasks
	$result
    
    $outputFilename = $psakeScriptFileinfo.BaseName;
    
    if (-not (test-path $outputDirectory)) {
        $null = mkdir $outputDirectory
    }
    
    $result | & "$sourceDirectory\Graphviz2.26.3\dot.exe" -Tjpg -o (join-path $outputDirectory "$outputFilename.jpg")
    $result | & "$sourceDirectory\Graphviz2.26.3\dot.exe" -Tpdf -o (join-path $outputDirectory "$outputFilename.pdf")
}

function DrawTasks {
    $result = "`
digraph {`
    graph [rank=""source"";rankdir = ""LR""];"

    $ks = @($tasks.keys);
    [Array]::reverse($ks);

    foreach($name in $ks) {

        $task = $tasks[$name];
        
        $line = "`n    $name [ shape=""record"", label=<$name> ] ";
    
        foreach($dependency in $task.depends) {
            $line += " $name -> $dependency;"
        }
        
        $result += $line
    }
    
    $result += "`n}`n"
    $result
}

function LoadTasks([System.IO.FileInfo] $psakeScript) {

    $tasks = @{};

    function Properties {
    }

    function Task {
        
        param(
            [Parameter(Position=0,Mandatory=1)]
            [string]$name = $null,

            [Parameter(Position=1,Mandatory=0)]
            [scriptblock]$action = $null,

            [Parameter(Position=2,Mandatory=0)]
            [scriptblock]$preaction = $null,

            [Parameter(Position=3,Mandatory=0)]
            [scriptblock]$postaction = $null,

            [Parameter(Position=4,Mandatory=0)]
            [scriptblock]$precondition = {$true},

            [Parameter(Position=5,Mandatory=0)]
            [scriptblock]$postcondition = {$true},

            [Parameter(Position=6,Mandatory=0)]
            [switch]$continueOnError = $false,

            [Parameter(Position=7,Mandatory=0)]
            [string[]]$depends = @(),

            [Parameter(Position=8,Mandatory=0)]
            [string]$description = $null
        )        
        
        $tasks[$name] = @{
            name = $name;
            depends = $depends;
        };
    }
    
    $originalLocation = get-location
    set-location $psakeScript.Directory
    
    try {
    
        & (gi $psakeScript)
    } finally {
        set-location $originalLocation
    }
    
	$tasks
}