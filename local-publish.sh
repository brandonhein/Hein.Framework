powershell Remove-Item './.publish' -Recurse

dotnet clean ./Configuration/Hein.Framework.Configuration/Hein.Framework.Configuration.csproj
powershell Remove-Item './Configuration/Hein.Framework.Configuration/bin' -Recurse
dotnet publish ./Configuration/Hein.Framework.Configuration/Hein.Framework.Configuration.csproj -o ./.publish/Hein.Framework.Configuration
powershell Compress-Archive -Path './.publish/Hein.Framework.Configuration/*' -DestinationPath './.publish/Hein.Framework.Configuration.zip'
powershell Remove-Item './.publish/Hein.Framework.Configuration' -Recurse
powershell Move-Item -Path ./Configuration/Hein.Framework.Configuration/bin/Debug/*.nupkg -Destination ./.publish

dotnet clean ./DependencyInjection/Hein.Framework.DependencyInjection/Hein.Framework.DependencyInjection.csproj
powershell Remove-Item './DependencyInjection/Hein.Framework.DependencyInjection/bin' -Recurse
dotnet publish ./DependencyInjection/Hein.Framework.DependencyInjection/Hein.Framework.DependencyInjection.csproj -o ./.publish/Hein.Framework.DependencyInjection
powershell Compress-Archive -Path './.publish/Hein.Framework.DependencyInjection/*' -DestinationPath './.publish/Hein.Framework.DependencyInjection.zip'
powershell Remove-Item './.publish/Hein.Framework.DependencyInjection' -Recurse
powershell Move-Item -Path ./DependencyInjection/Hein.Framework.DependencyInjection/bin/Debug/*.nupkg -Destination ./.publish

dotnet clean ./Extensions/Hein.Framework.Extensions/Hein.Framework.Extensions.csproj
powershell Remove-Item './Extensions/Hein.Framework.Extensions/bin' -Recurse
dotnet publish ./Extensions/Hein.Framework.Extensions/Hein.Framework.Extensions.csproj -o ./.publish/Hein.Framework.Extensions
powershell Compress-Archive -Path './.publish/Hein.Framework.Extensions/*' -DestinationPath './.publish/Hein.Framework.Extensions.zip'
powershell Remove-Item './.publish/Hein.Framework.Extensions' -Recurse
powershell Move-Item -Path ./Extensions/Hein.Framework.Extensions/bin/Debug/*.nupkg -Destination ./.publish

dotnet clean ./Http/Hein.Framework.Http/Hein.Framework.Http.csproj
powershell Remove-Item './Http/Hein.Framework.Http/bin' -Recurse
dotnet publish ./Http/Hein.Framework.Http/Hein.Framework.Http.csproj -o ./.publish/Hein.Framework.Http
powershell Compress-Archive -Path './.publish/Hein.Framework.Http/*' -DestinationPath './.publish/Hein.Framework.Http.zip'
powershell Remove-Item './.publish/Hein.Framework.Http' -Recurse
powershell Move-Item -Path ./Http/Hein.Framework.Http/bin/Debug/*.nupkg -Destination ./.publish

dotnet clean ./Processing/Hein.Framework.Processing/Hein.Framework.Processing.csproj
powershell Remove-Item './Processing/Hein.Framework.Processing/bin' -Recurse
dotnet publish ./Processing/Hein.Framework.Processing/Hein.Framework.Processing.csproj -o ./.publish/Hein.Framework.Processing
powershell Compress-Archive -Path './.publish/Hein.Framework.Processing/*' -DestinationPath './.publish/Hein.Framework.Processing.zip'
powershell Remove-Item './.publish/Hein.Framework.Processing' -Recurse
powershell Move-Item -Path ./Processing/Hein.Framework.Processing/bin/Debug/*.nupkg -Destination ./.publish

dotnet clean ./Serialization/Hein.Framework.Serialization/Hein.Framework.Serialization.csproj
powershell Remove-Item './Serialization/Hein.Framework.Serialization/bin' -Recurse
dotnet publish ./Serialization/Hein.Framework.Serialization/Hein.Framework.Serialization.csproj -o ./.publish/Hein.Framework.Serialization
powershell Compress-Archive -Path './.publish/Hein.Framework.Serialization/*' -DestinationPath './.publish/Hein.Framework.Serialization.zip'
powershell Remove-Item './.publish/Hein.Framework.Serialization' -Recurse
powershell Move-Item -Path ./Serialization/Hein.Framework.Serialization/bin/Debug/*.nupkg -Destination ./.publish

dotnet clean ./ValueObject/Hein.Framework.ValueObject/Hein.Framework.ValueObject.csproj
powershell Remove-Item './ValueObject/Hein.Framework.ValueObject/bin' -Recurse
dotnet publish ./ValueObject/Hein.Framework.ValueObject/Hein.Framework.ValueObject.csproj -o ./.publish/Hein.Framework.ValueObject
powershell Compress-Archive -Path './.publish/Hein.Framework.ValueObject/*' -DestinationPath './.publish/Hein.Framework.ValueObject.zip'
powershell Remove-Item './.publish/Hein.Framework.ValueObject' -Recurse
powershell Move-Item -Path ./ValueObject/Hein.Framework.ValueObject/bin/Debug/*.nupkg -Destination ./.publish