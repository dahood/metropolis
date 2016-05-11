$shell = New-Object -ComObject WScript.Shell
$desktop = [System.Environment]::GetFolderPath('Desktop')
$shortcut = $shell.CreateShortcut("$desktop\Metropolis.lnk")
$shortcut.TargetPath = "~AppData\Roaming\npm\metropolis\dist\metropolis.exe"
$shortcut.IconLocation = "~AppData\Roaming\npm\metropolis\dist\metropolis.exe,0"
$shortcut.Save()