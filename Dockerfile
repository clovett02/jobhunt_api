FROM mcr.microsoft.com/dotnet/aspnet:7.0

WORKDIR /app
COPY . .

EXPOSE 5000

CMD [ "dotnet", "run" ]