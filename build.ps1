param(
    [string]$projectName,
    [string]$buildAssemblyVersion,
    [string]$buildSemanticVersion
)

Set-StrictMode -Version 2
Set-Location $PSScriptRoot

(Get-Content $projectName\Properties\AssemblyInfo.cs -Encoding UTF8) -replace "\[assembly: AssemblyVersion\(`".*`"\)\]", "[assembly: AssemblyVersion(`"$($buildAssemblyVersion)`")]" | Set-Content $projectName\Properties\AssemblyInfo.cs -Encoding UTF8
(Get-Content $projectName\Properties\AssemblyInfo.cs -Encoding UTF8) -replace "\[assembly: AssemblyInformationalVersion\(`".*`"\)\]", "[assembly: AssemblyInformationalVersion(`"$($buildSemanticVersion)`")]" | Set-Content $projectName\Properties\AssemblyInfo.cs -Encoding UTF8

$packageOutputFolder = ".\artifacts\packages"

& nuget restore

$vswhere = ".\packages\vswhere.2.2.11\tools\vswhere.exe"
$msbuild = & $vswhere -latest -products * -requires Microsoft.Component.MSBuild -property installationPath
if ($msbuild) {
  $msbuild = join-path $msbuild "MSBuild\15.0\Bin\MSBuild.exe"
  if (test-path $msbuild) {
    & $msbuild $projectName\$projectName`.csproj /t:Rebuild /p:Configuration=Release /v:normal /m /nologo
  }
}

& nuget pack $projectName`.nuspec -NonInteractive -NoPackageAnalysis -OutputDirectory $packageOutputFolder -Properties configuration=Release`;version=$buildSemanticVersion
