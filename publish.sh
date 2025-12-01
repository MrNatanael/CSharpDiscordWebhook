rm CSharpDiscordWebhook/bin/Release/*.nupkg

dotnet pack CSharpDiscordWebhook -c Release
dotnet nuget push CSharpDiscordWebhook/bin/Release/*.nupkg --api-key $NUGET_API_KEY --source https://api.nuget.org/v3/index.json --skip-duplicate
