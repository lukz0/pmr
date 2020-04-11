#!/bin/bash
dotnet run -p "backend/backend.csproj" --launch-profile "backend_dev" 1>/dev/null &
if [ -x "$(command -v yarn)" ]; then
	(cd frontend; yarn serve) &>/dev/null &
	(cd frontend; yarn test)
else
	(cd frontend; npm run serve) &>/dev/null &
	(cd frontend; npm run test)
fi

kill -n 15 $(jobs -p)
lsof -i tcp:8080 | awk 'NR!=1 {print $2}' | xargs kill
