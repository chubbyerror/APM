#!/bin/bash
cd ..
cd /src/Publishers/Cortlex.APM.Health.Publishers.InfluxDb
dotnet pack -c Release --no-build -o /var/temp/pack/Cortlex.APM.Health.Publishers.InfluxDb
cd ..