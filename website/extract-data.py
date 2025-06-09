#!/usr/bin/env python3
import os
import json
import re
from pathlib import Path

class DataExtractor:
    def __init__(self):
        self.base_path = Path(__file__).parent.parent
        self.exercises_path = self.base_path / 'Exercises'
        self.website_path = self.base_path / 'website'
    
    def extract_all_data(self):
        exercises = []
        exercise_dirs = sorted([d for d in os.listdir(self.exercises_path) 
                               if d.startswith('Exercise')])
        
        for exercise_dir in exercise_dirs:
            exercise_data = self.extract_exercise_data(exercise_dir)
            exercises.append(exercise_data)
        
        # Save extracted data
        data_path = self.website_path / 'data'
        data_path.mkdir(exist_ok=True)
        
        with open(data_path / 'exercises.json', 'w', encoding='utf-8') as f:
            json.dump(exercises, f, ensure_ascii=False, indent=2)
        
        print(f"Extracted data for {len(exercises)} exercises")
        return exercises
    
    def extract_exercise_data(self, exercise_dir):
        exercise_path = self.exercises_path / exercise_dir
        readme_path = exercise_path / 'README.md'
        solutions_path = exercise_path / 'Solutions'
        
        exercise_data = {
            'id': exercise_dir.lower(),
            'directory': exercise_dir,
            'title': '',
            'difficulty': 1,
            'description': '',
            'readme': '',
            'solutions': []
        }
        
        # Read README.md
        if readme_path.exists():
            with open(readme_path, 'r', encoding='utf-8') as f:
                exercise_data['readme'] = f.read()
            
            # Extract title and difficulty from README
            title_match = re.search(r'^# (.+)', exercise_data['readme'], re.MULTILINE)
            if title_match:
                exercise_data['title'] = title_match.group(1)
                
                # Extract difficulty
                difficulty_match = re.search(r'難易度:\s*(\d+)', exercise_data['title'])
                if difficulty_match:
                    exercise_data['difficulty'] = int(difficulty_match.group(1))
            
            # Extract description from overview section
            overview_match = re.search(r'## 概要\n(.*?)\n\n', exercise_data['readme'], re.DOTALL)
            if overview_match:
                exercise_data['description'] = overview_match.group(1).strip()
        
        # Read solution files
        if solutions_path.exists():
            solution_files = sorted([f for f in os.listdir(solutions_path) 
                                   if f.endswith('.cs')])
            
            for solution_file in solution_files:
                solution_path = solutions_path / solution_file
                with open(solution_path, 'r', encoding='utf-8') as f:
                    content = f.read()
                
                exercise_data['solutions'].append({
                    'filename': solution_file,
                    'content': content
                })
        
        return exercise_data

if __name__ == '__main__':
    extractor = DataExtractor()
    extractor.extract_all_data()