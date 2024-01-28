FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY ./API/bin/Release/net8.0/publish ./

# Expose the port your application will run on
EXPOSE 8080

# Start the application
ENTRYPOINT ["dotnet", "API.dll"]