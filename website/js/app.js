// Main application JavaScript
class CSharpPracticeApp {
    constructor() {
        this.exercises = [];
        this.currentExercise = null;
        this.init();
    }

    async init() {
        await this.loadExerciseData();
        this.updateNavigation();
        this.setupEventListeners();
        this.showOverview();
    }

    updateNavigation() {
        const dropdownContent = document.querySelector('.dropdown-content');
        if (dropdownContent && this.exercises.length > 0) {
            dropdownContent.innerHTML = this.exercises.map(exercise => 
                `<li><a href="#${exercise.id}">${exercise.title}</a></li>`
            ).join('');
        }
    }

    async loadExerciseData() {
        try {
            const response = await fetch('data/exercises.json');
            this.exercises = await response.json();
        } catch (error) {
            console.error('Failed to load exercise data:', error);
            this.exercises = [];
        }
    }

    setupEventListeners() {
        // Navigation links
        document.querySelectorAll('.nav-link').forEach(link => {
            link.addEventListener('click', (e) => {
                e.preventDefault();
                const href = link.getAttribute('href');
                this.navigate(href);
            });
        });

        // Dropdown links
        document.querySelectorAll('.dropdown-content a').forEach(link => {
            link.addEventListener('click', (e) => {
                e.preventDefault();
                const href = link.getAttribute('href');
                this.navigate(href);
            });
        });

        // Handle browser back/forward
        window.addEventListener('popstate', (e) => {
            if (e.state) {
                this.navigate(e.state.section, false);
            }
        });
    }

    navigate(section, pushState = true) {
        // Update active nav link
        document.querySelectorAll('.nav-link').forEach(link => {
            link.classList.remove('active');
        });

        const activeLink = document.querySelector(`[href="${section}"]`);
        if (activeLink) {
            activeLink.classList.add('active');
        }

        // Show content
        if (section === '#overview') {
            this.showOverview();
        } else if (section.startsWith('#exercise')) {
            const exerciseId = section.substring(1);
            this.showExercise(exerciseId);
        }

        // Update URL
        if (pushState) {
            history.pushState({section}, '', section);
        }
    }

    showOverview() {
        const content = document.getElementById('content');
        content.innerHTML = `
            <div class="overview">
                <h1>C# Practice</h1>
                <p class="lead">C# 初級から中級者向け練習問題集</p>
                
                <div class="overview-section">
                    <h2>概要</h2>
                    <p>このリポジトリは、C#プログラミングを学習する初級から中級者向けの練習問題集です。12の段階的な演習問題を通じて、C#の基本概念から高度な機能まで学習できます。</p>
                </div>

                <div class="overview-section">
                    <h2>演習一覧</h2>
                    <div class="exercise-grid">
                        ${this.generateExerciseGrid()}
                    </div>
                </div>

                <div class="overview-section">
                    <h2>特徴</h2>
                    <ul>
                        <li><strong>段階的学習</strong>: 基礎から上級まで段階的に学習できる構成</li>
                        <li><strong>実践的な問題</strong>: 実際の開発で使用する技術を習得</li>
                        <li><strong>完全な解答例</strong>: 各問題に対して詳細な解答例を提供</li>
                        <li><strong>複数の問題</strong>: 各演習には1-3個の問題が含まれ、段階的にスキルアップ</li>
                        <li><strong>日本語対応</strong>: すべての問題と解答が日本語で記述</li>
                    </ul>
                </div>

                <div class="overview-section">
                    <h2>使用方法</h2>
                    <ol>
                        <li>各演習の問題を読んで内容を理解</li>
                        <li>自分で解答を作成</li>
                        <li>解答例と比較して理解を深める</li>
                        <li>理解度を確認して次の演習に進む</li>
                    </ol>
                </div>

                <div class="overview-section">
                    <h2>推奨学習順序</h2>
                    <ol>
                        <li>Exercise 01 から順番に取り組むことを推奨</li>
                        <li>各演習の問題を順番に解いていく</li>
                        <li>解答例を参考にしながら理解を深める</li>
                        <li>分からない部分は前の演習に戻って復習</li>
                    </ol>
                </div>

                <div class="overview-section">
                    <h2>実行環境</h2>
                    <ul>
                        <li>.NET 6.0 以上</li>
                        <li>C# 10.0 以上</li>
                    </ul>
                </div>
            </div>
        `;
    }

    generateExerciseGrid() {
        return this.exercises.map(exercise => `
            <div class="exercise-card" onclick="app.navigate('#${exercise.id}')">
                <div class="exercise-header">
                    <h3>${exercise.title}</h3>
                    <div class="badges">
                        <span class="difficulty-badge difficulty-${exercise.difficulty}">難易度: ${exercise.difficulty}</span>
                    </div>
                </div>
                <p>${exercise.description}</p>
            </div>
        `).join('');
    }

    async showExercise(exerciseId) {
        const content = document.getElementById('content');
        content.innerHTML = '<div class="loading"><div class="spinner"></div></div>';

        try {
            // Load exercise content
            const exerciseData = await this.loadExerciseContent(exerciseId);
            content.innerHTML = this.renderExercise(exerciseData);
            
            // Apply syntax highlighting
            setTimeout(() => {
                applySyntaxHighlighting();
            }, 100);
        } catch (error) {
            content.innerHTML = `<div class="error">エラー: 演習データを読み込めませんでした。</div>`;
            console.error('Error loading exercise:', error);
        }
    }

    async loadExerciseContent(exerciseId) {
        // Find the exercise in the loaded data
        const exercise = this.exercises.find(ex => ex.id === exerciseId);
        if (!exercise) {
            throw new Error(`Exercise ${exerciseId} not found`);
        }
        
        return {
            id: exerciseId,
            title: exercise.title,
            readme: exercise.readme,
            solutions: exercise.solutions
        };
    }

    async loadMarkdownContent(exerciseId) {
        // This method is no longer needed since we load from JSON
        return '';
    }

    async loadSolutions(exerciseId) {
        // This method is no longer needed since we load from JSON
        return [];
    }

    renderExercise(exerciseData) {
        const markdownHtml = parseMarkdown(exerciseData.readme);
        const solutionsHtml = this.renderSolutions(exerciseData.solutions);
        
        return `
            <div class="exercise-content">
                <div class="exercise-readme">
                    ${markdownHtml}
                </div>
                ${solutionsHtml}
            </div>
        `;
    }

    renderSolutions(solutions) {
        if (!solutions || solutions.length === 0) {
            return '<div class="no-solutions"><p>解答例は準備中です。</p></div>';
        }

        return `
            <div class="solutions-section">
                <h2>解答例</h2>
                ${solutions.map((solution, index) => `
                    <div class="solution-section">
                        <div class="solution-header">
                            <h3>${solution.filename}</h3>
                            <button class="toggle-solution" onclick="this.parentElement.parentElement.querySelector('.solution-code').style.display = this.parentElement.parentElement.querySelector('.solution-code').style.display === 'none' ? 'block' : 'none'; this.textContent = this.textContent === '解答を表示' ? '解答を隠す' : '解答を表示';">解答を表示</button>
                        </div>
                        <div class="solution-code" style="display: none;">
                            <pre><code class="language-csharp">${this.escapeHtml(solution.content)}</code></pre>
                        </div>
                    </div>
                `).join('')}
            </div>
        `;
    }

    escapeHtml(text) {
        const div = document.createElement('div');
        div.textContent = text;
        return div.innerHTML;
    }
}

// Initialize the app when DOM is loaded
let app;
document.addEventListener('DOMContentLoaded', () => {
    app = new CSharpPracticeApp();
});