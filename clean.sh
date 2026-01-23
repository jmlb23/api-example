#!/usr/bin/env bash

bazelisk clean --expunge ; 
dotnet clean api.csproj ; 
dotnet clean Features/Core/core.csproj ; 
dotnet clean Features/features.csproj ;
dotnet clean Features/Auth/auth.csproj ;
dotnet clean Features/Users/users.csproj ;
dotnet clean Features/Publications/publications.csproj ;
dotnet clean Features/Comments/comments.csproj
