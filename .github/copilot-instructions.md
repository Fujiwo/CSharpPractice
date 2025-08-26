# C# Practice Repository
C# learning repository containing 12 progressive exercises from basic console output to advanced async programming, plus a static website for browsing exercises.

Always reference these instructions first and fallback to search or bash commands only when you encounter unexpected information that does not match the info here.

## Working Effectively

### Bootstrap and Setup
- **Install .NET SDK**: This repository requires .NET 6.0 or higher
- **Verify installation**: `dotnet --version` (should show 6.0+ or 8.0+)
- **Python requirement**: Python 3 is required for website functionality

### Running C# Exercises - CRITICAL TIMING INFORMATION
**NEVER CANCEL** any dotnet commands. All builds complete in under 5 seconds. Set timeout to 60+ seconds minimum.

#### Correct Method to Run Solutions:
```bash
# Create a new console project (takes ~1.5 seconds)
dotnet new console --name TestProject --force

# Copy the solution file as Program.cs  
cp /path/to/ProblemX-Y.cs TestProject/Program.cs

# Build and run (takes ~1.5 seconds) - NEVER CANCEL
cd TestProject && dotnet run
```

#### WARNING - Incorrect Methods:
- `dotnet run ProblemX-Y.cs` -- **FAILS** - This method documented in README.md does not work
- `csc ProblemX-Y.cs` -- **FAILS** - csc compiler not available in this environment

### Website Functionality
- **Extract exercise data**: `cd website && python3 extract-data.py` (takes <1 second)
- **Start web server**: `cd website && python3 -m http.server 8080` 
- **Access website**: http://localhost:8080
- **API endpoint**: http://localhost:8080/data/exercises.json

## Build Times and Expectations
- **Project creation**: 1.5 seconds - NEVER CANCEL, set timeout 60+ seconds
- **Simple compilation**: 1.5 seconds - NEVER CANCEL, set timeout 60+ seconds  
- **Complex compilation**: <5 seconds - NEVER CANCEL, set timeout 60+ seconds
- **Data extraction**: <0.1 seconds
- **Website startup**: 2 seconds

## Validation Scenarios
Always manually validate changes by running complete user scenarios:

### Basic Exercise Validation:
1. Create new console project
2. Copy Exercise01/Solutions/Problem1-1.cs as Program.cs
3. Run `dotnet run` and verify "Hello, World!" output
4. **Expected result**: Clean compilation and correct output

### Complex Exercise Validation:
1. Create new console project  
2. Copy Exercise10/Solutions/Problem10-1.cs as Program.cs
3. Run with input: `echo "42" | dotnet run`
4. **Expected result**: Compilation warnings OK, handles input correctly

### Website Validation:
1. Run `python3 extract-data.py` in website folder
2. Start `python3 -m http.server 8080`
3. Verify `curl http://localhost:8080` returns HTML
4. Verify `curl http://localhost:8080/data/exercises.json` returns valid JSON
5. **Expected result**: Website serves 12 exercises with proper structure

## Repository Structure Navigation

### Key Directories:
- **`/Exercises/Exercise01-Exercise12/`**: 12 progressive learning exercises
- **`/Exercises/ExerciseXX/README.md`**: Problem descriptions and requirements
- **`/Exercises/ExerciseXX/Solutions/ProblemX-Y.cs`**: Complete solution files
- **`/website/`**: Static website for browsing exercises
- **`/website/data/exercises.json`**: Auto-generated exercise data

### Exercise Difficulty Progression:
- **Basic (1-2)**: Exercise01-03 - Console output, variables, conditionals
- **Beginner (2-3)**: Exercise04-05 - Loops, arrays, collections  
- **Intermediate (3-4)**: Exercise06-10 - Methods, classes, inheritance, exceptions
- **Advanced (4-5)**: Exercise11-12 - LINQ, async programming

## Common Development Tasks

### Testing a Modified Solution:
1. Create temporary project: `dotnet new console --name TestSolution --force`
2. Copy modified solution: `cp ModifiedProblem.cs TestSolution/Program.cs`
3. Build and test: `cd TestSolution && dotnet run`
4. Clean up: `rm -rf TestSolution/`

### Adding New Exercise:
1. Create new directory: `mkdir Exercises/Exercise13`
2. Create README.md with problem descriptions
3. Create Solutions/ folder with .cs solution files
4. Update website data: `cd website && python3 extract-data.py`
5. Test website: Start server and verify new exercise appears

### Debugging Compilation Issues:
- **Nullable warnings**: Normal for older C# examples, compilation succeeds
- **Interactive input**: Some examples expect user input - provide via echo or stdin
- **Async examples**: May run indefinitely - use timeout or Ctrl+C to stop

## Frequently Referenced Content

### Repository Root Structure:
```
CSharpPractice/
├── README.md              # Main documentation (has incorrect dotnet run instructions)
├── LICENSE                # MIT License
├── Exercises/             # 12 exercise folders Exercise01-Exercise12
├── website/              # Static website for browsing
└── docs/                 # Additional documentation
```

### Sample Exercise Structure (Exercise01):
```
Exercise01/
├── README.md             # Problems 1-1, 1-2, 1-3 descriptions
└── Solutions/
    ├── Problem1-1.cs     # Hello World example
    ├── Problem1-2.cs     # Name greeting
    └── Problem1-3.cs     # Multi-line output
```

### Website File Structure:
```
website/
├── index.html           # Main web interface
├── css/style.css       # Styling
├── js/app.js          # JavaScript application
├── data/exercises.json # Auto-generated exercise data
└── extract-data.py    # Data extraction script
```

## Critical Reminders
- **ALWAYS** use `dotnet new console` method for running solutions
- **NEVER CANCEL** dotnet commands - they complete in seconds
- **SET TIMEOUTS** of 60+ seconds for all dotnet operations
- **MANUAL VALIDATION** required - run actual scenarios, don't just compile
- **README.md compilation instructions are INCORRECT** - use documented method above
- **Interactive examples** may require input via echo/stdin redirection