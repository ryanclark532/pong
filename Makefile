SOLUTION_NAME = pong.sln
PROJECT_DIR = pong

all: build

build:
	@echo "Building the solution..."
	dotnet build $(SOLUTION_NAME)

run:
	@echo "Running the application..."
	dotnet run --$(PROJECT_DIR)

clean:
	@echo "Cleaning build artifacts..."
	dotnet clean $(SOLUTION_NAME)

restore:
	@echo "Restoring NuGet packages..."
	dotnet restore $(SOLUTION_NAME)

rebuild: clean restore build

.PHONY: all build run clean restore rebuild help
