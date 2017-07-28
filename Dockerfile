
FROM microsoft/dotnet:1.1.2-sdk
LABEL Name=influxjob Version=0.0.1 
COPY . /usr/share/dotnet/sdk/influxjob
WORKDIR /usr/share/dotnet/sdk/influxjob
RUN dotnet restore 
RUN dotnet build 
ENTRYPOINT ["dotnet",  "run",  "--project",  "InfluxClassLib/InfluxDBClassLib.csproj"]



