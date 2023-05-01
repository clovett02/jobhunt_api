FROM mcr.microsoft.com/dotnet/sdk:7.0

WORKDIR /app
COPY . .

EXPOSE 5000

RUN "dotnet restore"

CMD [ "dotnet", "run" ]