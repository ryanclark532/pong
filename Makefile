SOLUTION_NAME = pong.sln
PROJECT_DIR = pong

all: build

build:
	dotnet build $(SOLUTION_NAME)

run: build
	./bin/Debug/net8.0/pong.exe multiplayer host 5678

clean:
	dotnet clean $(SOLUTION_NAME)

restore:
	dotnet restore $(SOLUTION_NAME)

rebuild: clean restore build

.PHONY: all build run clean restore rebuild help
