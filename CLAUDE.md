# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Build & Run Commands

- **Build:** `dotnet build`
- **Run:** `dotnet run --project ClaudeTestRepo.API`
- **Run (specific profile):** `dotnet run --project ClaudeTestRepo.API --launch-profile https`

No test project exists yet. When one is added, use `dotnet test` to run all tests.

## Architecture

This is a .NET 9 minimal API solution with a single project:

- **ClaudeTestRepo.API** — ASP.NET Core Web API using minimal API pattern (no controllers). All endpoints are defined inline in `Program.cs`. The `WeatherForecast` record is also defined in `Program.cs`, with a duplicate class in `Models/WeatherForecast.cs`.

The API runs on `http://localhost:5230` (or `https://localhost:7003` with the https profile).
