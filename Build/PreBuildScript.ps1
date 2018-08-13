$changeSetNumber = [regex]::Match((& 'C:\Program Files (x86)\Microsoft Visual Studio 12.0\Common7\IDE\TF.exe' history . /r /noprompt /stopafter:1 /version:W), "\d+").Value
(Get-Item .\ITSK.SSS.DATA\Properties\AssemblyInfoCommon.cs).Attributes = "Normal"
(Get-Content .\ITSK.SSS.DATA\Properties\AssemblyInfoCommon.cs) -replace "9999", $changeSetNumber | Set-Content .\ITSK.SSS.DATA\Properties\AssemblyInfoCommon.cs
