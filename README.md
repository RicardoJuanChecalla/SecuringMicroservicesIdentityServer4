Securing .NET 6 Microservices with IdentityServer4 with OAuth2, OpenID Connect and Ocelot Api Gateway

https://mehmetozkaya.medium.com/securing-net-e335e0796d86
https://github.com/aspnetrun/run-aspnet-identityserver4
https://medium.com/aspnetrun/securing-microservices-with-identityserver4-with-oauth2-and-openid-connect-fronted-by-ocelot-api-49ea44a0cf9e
https://identityserver4.readthedocs.io/en/latest/

mkdir SecureMicroservices
cd SecureMicroservices
dotnet new sln
dotnet new webapi -au none -o Movies.Api
dotnet sln add Movies.Api
dotnet add ./Movies.Api/Movies.Api.csproj package Microsoft.EntityFrameworkCore.Tools --version 6.0.15
dotnet add ./Movies.Api/Movies.Api.csproj package Microsoft.EntityFrameworkCore.InMemory --version 6.0.15
dotnet add ./Movies.Api/Movies.Api.csproj package Microsoft.AspNetCore.Authentication.JwtBearer --version 6.0.15

dotnet new web -o IdentityServer
dotnet sln add IdentityServer
dotnet add ./IdentityServer/IdentityServer.csproj package IdentityServer4 --version 4.1.2

dotnet new mvc -o Movies.Client
dotnet sln add Movies.Client
dotnet add ./Movies.Client/Movies.Client.csproj package Microsoft.AspNetCore.Authentication.OpenIdConnect --version 6.0.15
dotnet add ./Movies.Client/Movies.Client.csproj package IdentityModel --version 6.0.0

>>D:\SamplesAPI\SecureMicroservices\IdentityServer> iex ((New-Object System.Net.WebClient).DownloadString('https://raw.githubusercontent.com/IdentityServer/IdentityServer4.Quickstart.UI/main/getmain.ps1'))

dotnet new web -o ApiGateway
dotnet sln add ApiGateway
dotnet add ./ApiGateway/ApiGateway.csproj package Ocelot --version 18.0.0
dotnet add ./ApiGateway/ApiGateway.csproj package Microsoft.AspNetCore.Authentication.JwtBearer --version 6.0.15

dotnet restore ./Movies.Api/Movies.Api.csproj
dotnet build ./Movies.Api/Movies.Api.csproj
dotnet run --project ./Movies.Api/Movies.Api.csproj

dotnet restore ./IdentityServer/IdentityServer.csproj
dotnet build ./IdentityServer/IdentityServer.csproj
dotnet run --project ./IdentityServer/IdentityServer.csproj

dotnet restore ./Movies.Client/Movies.Client.csproj
dotnet build ./Movies.Client/Movies.Client.csproj
dotnet run --project ./Movies.Client/Movies.Client.csproj

dotnet restore ./ApiGateway/ApiGateway.csproj
dotnet build ./ApiGateway/ApiGateway.csproj
dotnet run --project ./ApiGateway/ApiGateway.csproj