param($installPath, $toolsPath, $package, $project)

$project.Object.References | Where-Object { $_.Name -eq 'WpfControls' } | ForEach-Object { $_.Remove() }