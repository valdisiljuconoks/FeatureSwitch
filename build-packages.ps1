Add-Type -assembly "system.io.compression.filesystem"

cd .\.nuget

$moduleName = "FeatureSwitch.EPiServer"
$source = $PSScriptRoot + "\" + $moduleName + "\modules\_protected\" + $moduleName
$destination = $PSScriptRoot + "\" + $moduleName + "\" + $moduleName + ".zip"

If(Test-path $destination) {Remove-item $destination}
[io.compression.zipfile]::CreateFromDirectory($Source, $destination)


.\nuget.exe pack ..\FeatureSwitch\FeatureSwitch.csproj -Properties Configuration=Release -IncludeReferencedProjects
.\nuget.exe pack ..\FeatureSwitch.AspNet.Mvc5\FeatureSwitch.AspNet.Mvc5.csproj -Properties Configuration=Release -IncludeReferencedProjects
.\nuget.exe pack ..\FeatureSwitch.StructureMap\FeatureSwitch.StructureMap.csproj -Properties Configuration=Release -IncludeReferencedProjects
.\nuget.exe pack ..\FeatureSwitch.EPiServer\FeatureSwitch.EPiServer.csproj -Properties Configuration=Release -IncludeReferencedProjects

cd ..\