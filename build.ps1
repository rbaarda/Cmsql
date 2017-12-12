param(
    [string]$target = "test",
    [string]$configuration = "Release",
    [string]$buildAssemblyVersion = "",
    [string]$buildSemanticVersion = ""
)

Set-StrictMode -Version 2
Set-Location $PSScriptRoot

$packageOutputFolder = (join-path (Get-Location) "artifacts\packages")

function Exec([string] $command, [string] $message = "") {
    if ($message -eq "") {
        $message = $command
    }
    Write-Host -ForegroundColor DarkGray ("EXEC: " + $message)
    Write-Host ""
    Invoke-Expression $command
    Write-Host ""

    if ($LASTEXITCODE -ne 0) {
        exit 1
    }
}

function MsBuild(
	[string] $project,
	[string] $configuration,
	[string] $target = "build",
	[string] $verbosity = "minimal",
	[string] $message = "",
	[string] $binlogFile = "") {
    $cmd = '&"C:\Program Files (x86)\Microsoft Visual Studio\2017\Enterprise\MSBuild\15.0\bin\msbuild" ' + $project + ' /t:' + $target + ' /p:Configuration=' + $configuration + ' /v:' + $verbosity + ' /m /nologo'
    if ($binlogFile -ne "") {
        $cmd = $cmd + " /bl:" + $binlogFile
    }
    Exec $cmd $message
}

function NugetPack {
    param(
	    [System.IO.FileInfo[]][Parameter(ValueFromPipeLine=$True)] $nuspecFiles,
	    [string] $outputFolder,
	    [string] $configuration)
    process {
        $nuspecFiles | ForEach-Object {
            Exec ('c:\dev\nuget\nuget.exe pack "' + $_.FullName + '" -NonInteractive -NoPackageAnalysis -OutputDirectory "' + $outputFolder + '" -Properties Configuration=' + $configuration)
        }
    }
}

function BuildStep([string] $message) {
    Write-Host -ForegroundColor White $("==> " + $message + " <==")
    Write-Host ""
}

function CreatePackages() {
    BuildStep "Creating NuGet packages"
    Get-ChildItem -Recurse -Filter *.nuspec | NugetPack -outputFolder $packageOutputFolder -configuration $configuration
}

function MakeDir([string] $path) {
    if ((test-path $path) -eq $false) {
        New-Item -Type directory -Path $path | out-null
    }
}

MakeDir $packageOutputFolder

MsBuild "Cql.sln" $configuration

CreatePackages
