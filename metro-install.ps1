# create shortcut for desktop

$shell = New-Object -ComObject WScript.Shell
$desktop = [System.Environment]::GetFolderPath('Desktop')
$shortcut = $shell.CreateShortcut("$desktop\Metropolis.lnk")
$shortcut.TargetPath = $env:APPDATA + "\npm\node_modules\metropolis-core\dist\metropolis.exe"
$shortcut.IconLocation = $env:APPDATA + "\npm\node_modules\metropolis-core\dist\metropolis.exe,0"
$shortcut.Save()