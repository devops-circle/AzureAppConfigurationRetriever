param(
    [ValidateSet('Debug', 'Release')]
    [string]
    $Configuration = 'Debug',
    [string]
    $Version,
    [Switch]
    $Full,
    $NetCoreVersion = 'net6.0',
    [Switch]
    $Zip
)

$ProjectRoot = "$PSScriptRoot/.."
$outPath = "$ProjectRoot/out/AzureAppConfigurationRetriever.PS"
$commonPath = "$outPath/Dependencies"

if (Test-Path $outPath) {
    Write-Host "Clean up output path $outPath" -ForegroundColor Magenta
    Remove-Item -Path $outPath -Recurse
}
New-Item -Path $outPath -ItemType Directory
New-Item -Path $commonPath -ItemType Directory

$Path = "$ProjectRoot/AzureAppConfigurationRetriever.Core"
Push-Location -Path $Path
Write-Host $Path -ForegroundColor 'Magenta'

Write-Host "Start publish AzureAppConfigurationRetriever.Core" -ForegroundColor Magenta
if ($Full) {
    Write-Host "Full Cleaning..." -ForegroundColor DarkYellow
    dotnet build-server shutdown
    dotnet clean
}
Write-Host "Publishing in $Configuration mode" -ForegroundColor DarkYellow
dotnet publish -c $Configuration
Pop-Location

$Path = "$ProjectRoot/AzureAppConfigurationRetriever.PS"
Push-Location -Path $Path
Write-Host $Path -ForegroundColor 'Magenta'

Write-Host "Start building AzureAppConfigurationRetriever.PS" -ForegroundColor Magenta
if ($Full) {
    Write-Host "Full Cleaning..." -ForegroundColor DarkYellow
    dotnet build-server shutdown
    dotnet clean
}
Write-Host "Publishing in $Configuration mode" -ForegroundColor DarkYellow
dotnet publish -c $Configuration -f $NetCoreVersion
Pop-Location

$commonFiles = [System.Collections.Generic.HashSet[string]]::new()

Write-Host "Start copying dependencies files to path $commonPath" -ForegroundColor Magenta
Get-ChildItem -Path "$ProjectRoot/AzureAppConfigurationRetriever.Core/bin/$Configuration/netstandard2.1/publish" |
Where-Object { $_.Extension -in '.dll', '.pdb' } |
ForEach-Object { 
    [void]$commonFiles.Add($_.Name); 
    Copy-Item -LiteralPath $_.FullName -Destination $commonPath 
}

Write-Host "Start copying main files to path $outPath" -ForegroundColor Magenta
Get-ChildItem -Path "$ProjectRoot/AzureAppConfigurationRetriever.PS/bin/$Configuration/$NetCoreVersion/publish" |
Where-Object { $_.Extension -in '.dll', '.pdb' -and -not $commonFiles.Contains($_.Name) } |
ForEach-Object { 
    Copy-Item -LiteralPath $_.FullName -Destination $outPath
}

Write-Host "Copy module manifest to $outPath" -ForegroundColor Magenta
Copy-Item -Path "$ProjectRoot/AzureAppConfigurationRetriever.PS/Manifest/AzureAppConfigurationRetriever.PS.psd1" -Destination $outPath

if (-not $PSBoundParameters.ContainsKey('Version')) {
    try {
        $Version = gitversion /showvariable LegacySemVerPadded
    }
    catch {
        $Version = [string]::Empty
    }
}

if($Version) {
    Write-Host "Setting Version $Version in module" -ForegroundColor Magenta
    $SemVer, $PreReleaseTag = $Version.Split('-')
    Update-ModuleManifest -Path "$outPath/AzureAppConfigurationRetriever.PS.psd1" -ModuleVersion $SemVer -Prerelease $PreReleaseTag
}

if($Zip) {
    Write-Host "Zipping to $outPath" -ForegroundColor Magenta
    Compress-Archive -Path $outPath -DestinationPath "$ProjectRoot/AzureAppConfigurationRetriever.PS.zip" -Force
}