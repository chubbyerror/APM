FROM microsoft/dotnet:2.1-sdk AS build

WORKDIR /

COPY Cortlex.APM.sln /
COPY src/ /src
COPY scripts/ /scripts

RUN dotnet restore Cortlex.APM.sln
RUN dotnet build Cortlex.APM.sln --no-restore -c Release


FROM build as pack
WORKDIR /scripts
RUN chmod +x pack.entrypoint.sh
WORKDIR /