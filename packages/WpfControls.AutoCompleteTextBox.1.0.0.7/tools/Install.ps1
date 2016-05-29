param($installPath, $toolsPath, $package, $project)

$project.Object.References.Add((Join-Path $installPath 'lib\net35\WpfControls.dll'))