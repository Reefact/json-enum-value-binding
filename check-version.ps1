param(
    [string]$latestVersion,
    [string]$nuspecVersion
)

function Log-Error {
    param([string]$errorMessage)
    Write-Host "ERROR: $errorMessage"
}

function Validate-Version {
    $versionPattern = '^\d+\.\d+\.\d+$'

    if ($latestVersion -notmatch $versionPattern) {
        Log-Error "The latest version is not a valid version number: $latestVersion"
        
        return $false
    }

    if ($nuspecVersion -notmatch $versionPattern) {
        Log-Error "The nuspec version is not a valid version number: $nuspecVersion"
        
        return $false
    }

    $latestVersionParts = $latestVersion -split '\.'
    $nuspecVersionParts = $nuspecVersion -split '\.'

    for ($i = 0; $i -lt 3; $i++) {
        if ($nuspecVersionParts[$i] -gt $latestVersionParts[$i]) {
            return $true
        } elseif ($nuspecVersionParts[$i] -lt $latestVersionParts[$i]) {
            Log-Error "nuspecVersion is not greater than latestVersion"
            
            return $false
        }
    }

    return $false
}

# Call the function and output the result
$validationResult = Validate-Version -latestVersion $latestVersion -nuspecVersion $nuspecVersion
$validationResult
