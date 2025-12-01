rm CSharpDiscordWebhook/bin/Release/netstandard2.0/*.nupkg

dotnet pack CSharpDiscordWebhook -c Release
dotnet nuget push CSharpDiscordWebhook/bin/Release/netstandard2.0/*.nupkg --api-key $NUGET_API_KEY --source https://api.nuget.org/v3/index.json --skip-duplicate
