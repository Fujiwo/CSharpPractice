// Data loader script to extract content from the repository files
const fs = require('fs');
const path = require('path');

class DataExtractor {
    constructor() {
        this.basePath = path.join(__dirname, '..');
        this.exercisesPath = path.join(this.basePath, 'Exercises');
        this.websitePath = path.join(this.basePath, 'website');
    }

    async extractAllData() {
        const exercises = [];
        const exerciseDirs = fs.readdirSync(this.exercisesPath)
            .filter(dir => dir.startsWith('Exercise'))
            .sort();

        for (const exerciseDir of exerciseDirs) {
            const exerciseData = await this.extractExerciseData(exerciseDir);
            exercises.push(exerciseData);
        }

        // Save extracted data
        const dataPath = path.join(this.websitePath, 'data');
        if (!fs.existsSync(dataPath)) {
            fs.mkdirSync(dataPath, { recursive: true });
        }

        fs.writeFileSync(
            path.join(dataPath, 'exercises.json'),
            JSON.stringify(exercises, null, 2),
            'utf8'
        );

        console.log(`Extracted data for ${exercises.length} exercises`);
        return exercises;
    }

    async extractExerciseData(exerciseDir) {
        const exercisePath = path.join(this.exercisesPath, exerciseDir);
        const readmePath = path.join(exercisePath, 'README.md');
        const solutionsPath = path.join(exercisePath, 'Solutions');

        const exerciseData = {
            id: exerciseDir.toLowerCase(),
            directory: exerciseDir,
            title: '',
            difficulty: 1,
            description: '',
            readme: '',
            solutions: []
        };

        // Read README.md
        if (fs.existsSync(readmePath)) {
            exerciseData.readme = fs.readFileSync(readmePath, 'utf8');
            
            // Extract title and difficulty from README
            const titleMatch = exerciseData.readme.match(/^# (.+)/);
            if (titleMatch) {
                exerciseData.title = titleMatch[1];
                
                // Extract difficulty
                const difficultyMatch = exerciseData.title.match(/難易度:\s*(\d+)/);
                if (difficultyMatch) {
                    exerciseData.difficulty = parseInt(difficultyMatch[1]);
                }
            }

            // Extract description from overview section
            const overviewMatch = exerciseData.readme.match(/## 概要\n(.*?)\n\n/s);
            if (overviewMatch) {
                exerciseData.description = overviewMatch[1].trim();
            }
        }

        // Read solution files
        if (fs.existsSync(solutionsPath)) {
            const solutionFiles = fs.readdirSync(solutionsPath)
                .filter(file => file.endsWith('.cs'))
                .sort();

            for (const solutionFile of solutionFiles) {
                const solutionPath = path.join(solutionsPath, solutionFile);
                const content = fs.readFileSync(solutionPath, 'utf8');
                
                exerciseData.solutions.push({
                    filename: solutionFile,
                    content: content
                });
            }
        }

        return exerciseData;
    }
}

// Run if this script is executed directly
if (require.main === module) {
    const extractor = new DataExtractor();
    extractor.extractAllData().catch(console.error);
}

module.exports = DataExtractor;