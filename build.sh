#!/bin/bash
$(cd frontend; yarn build)
dotnet publish backend/backend.csproj -c Release -f netcoreapp3.1 -o output --self-contained true -r $1
# use tar -zxvf output.tar.gz output to output as folder
tar -zcvf output.tar.gz output/*
rm -r output
