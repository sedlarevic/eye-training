.PHONY: frontend backend all lint lint-fix

frontend:
	cd frontend/eye-training && npm install && npm run lint && npm test

backend:
	find backend -name "*.csproj" -print0 | xargs -0 -n1 dotnet restore
	find backend -name "*.csproj" -print0 | xargs -0 -n1 dotnet format --verify-no-changes
	dotnet test backend/EyeTraining.Tests/EyeTraining.Tests.csproj

lint-fix:
	cd frontend/eye-training && npm run lint -- --fix || true
	find backend -name "*.csproj" -print0 | xargs -0 -n1 dotnet format

docker-lint:
	hadolint frontend/eye-training/Dockerfile
	hadolint backend/Dockerfile

all: frontend backend
