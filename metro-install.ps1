# create shortcut for desktop

$shell = New-Object -ComObject WScript.Shell
$desktop = [System.Environment]::GetFolderPath('Desktop')
$shortcut = $shell.CreateShortcut("$desktop\Metropolis.lnk")
$shortcut.TargetPath = $env:APPDATA + "\npm\node_modules\metropolis\dist\metropolis.exe"
$shortcut.WorkingDirectory = $env:APPDATA + "\npm\node_modules\metropolis\dist\"
$shortcut.IconLocation = $env:APPDATA + "\npm\node_modules\metropolis\dist\metropolis.exe,0"
$shortcut.Save()