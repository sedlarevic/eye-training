.PHONY: all frontend backend lint test lint-fix docker-lint \
        frontend-lint frontend-test backend-lint backend-test

# Frontend

frontend: frontend-lint frontend-test

frontend-lint:
	cd frontend/eye-training && npm install && npm run lint

frontend-test:
	cd frontend/eye-training && npm test


# Backend

backend: backend-lint backend-test

backend-lint:
	find backend -name "*.csproj" -print0 | xargs -0 -n1 dotnet format --verify-no-changes

backend-test:
	find backend -name "*.csproj" -print0 | xargs -0 -n1 dotnet restore
	dotnet test backend/EyeTraining.Tests/EyeTraining.Tests.csproj


# Linting

lint: frontend-lint backend-lint

lint-fix:
	cd frontend/eye-training && npm run lint -- --fix || true
	find backend -name "*.csproj" -print0 | xargs -0 -n1 dotnet format


# Test

test: frontend-test backend-test


# Docker

docker-lint:
	hadolint frontend/eye-training/Dockerfile
	hadolint backend/Dockerfile


# All

all: frontend backend

