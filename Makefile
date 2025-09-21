.PHONY: frontend backend all lint lint-fix

frontend:
	cd frontend && npm install && npm run lint && npm test

backend:
	find backend -name "*.csproj" -print0 | xargs -0 -n1 dotnet restore
	find backend -name "*.csproj" -print0 | xargs -0 -n1 dotnet format --verify-no-changes
	dotnet test backend/EyeTraining.Tests/EyeTraining.Tests.csproj

lint-fix:
	cd frontend && npm run lint -- --fix || true
	find backend -name "*.csproj" -print0 | xargs -0 -n1 dotnet format

docker-lint:
	hadolint frontend/Dockerfile
	hadolint backend/Dockerfile

all: frontend backend
