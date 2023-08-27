param(
    [string]$latestVersion,
    [string]$nuspecVersion
)

function Check-Version {
    $versionPattern = '^\d+\.\d+\.\d+$'

    if ($latestVersion -notmatch $versionPattern) {
        return "latestVersion is not a valid version number: $latestVersion"
    }

    if ($nuspecVersion -notmatch $versionPattern) {
        return "nuspecVersion is not a valid version number: $nuspecVersion"
    }

    $latestVersionParts = $latestVersion -split '\.'
    $nuspecVersionParts = $nuspecVersion -split '\.'

    for ($i = 0; $i -lt 3; $i++) {
        if ($nuspecVersionParts[$i] -gt $latestVersionParts[$i]) {
            return $null
        }
        elseif ($nuspecVersionParts[$i] -lt $latestVersionParts[$i]) {
            return "nuspecVersion is not greater than latestVersion"
        }
    }

    return "nuspecVersion is not greater than latestVersion"
}

# Call the function and output the result
$validationResult = Check-Version -latestVersion $latestVersion -nuspecVersion $nuspecVersion
$validationResult
