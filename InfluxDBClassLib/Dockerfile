FROM microsoft/dotnet:1.1.2-sdk
LABEL Name=influxdbclasslib Version=0.0.1 
WORKDIR /app
COPY . /usr/share/dotnet/sdk/influxjob
WORKDIR /usr/share/dotnet/sdk/influxjob
RUN dotnet restore 
RUN dotnet build 
ENTRYPOINT dotnet run 
