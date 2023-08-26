function Compare-SemanticVersions {
    param (
        [string]$version1,
        [string]$version2
    )

    $version1Array = $version1 -split '\.'
    $version2Array = $version2 -split '\.'

    $length = [Math]::Max($version1Array.Length, $version2Array.Length)

    for ($i = 0; $i -lt $length; $i++) {
        $part1 = if ($i -lt $version1Array.Length) { [int]$version1Array[$i] } else { 0 }
        $part2 = if ($i -lt $version2Array.Length) { [int]$version2Array[$i] } else { 0 }

        if ($part1 -lt $part2) {
            return -1
        }
        elseif ($part1 -gt $part2) {
            return 1
        }
    }

    return 0
}
