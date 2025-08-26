// Minimal Prism.js implementation for C# syntax highlighting
(function() {
    'use strict';

    // Global Prism object
    window.Prism = window.Prism || {};
    
    // Token types for C#
    const csharpTokens = {
        'comment': /\/\/.*|\/\*[\s\S]*?\*\//g,
        'string': /"(?:[^"\\]|\\.)*"|'(?:[^'\\]|\\.)*'/g,
        'keyword': /\b(?:abstract|as|base|bool|break|byte|case|catch|char|checked|class|const|continue|decimal|default|delegate|do|double|else|enum|event|explicit|extern|false|finally|fixed|float|for|foreach|goto|if|implicit|in|int|interface|internal|is|lock|long|namespace|new|null|object|operator|out|override|params|private|protected|public|readonly|ref|return|sbyte|sealed|short|sizeof|stackalloc|static|string|struct|switch|this|throw|true|try|typeof|uint|ulong|unchecked|unsafe|ushort|using|virtual|void|volatile|while|yield)\b/g,
        'number': /\b0x[\da-f]+\b|\b\d+\.?\d*(?:e[+-]?\d+)?[flmu]?\b/gi,
        'operator': /[-+*/%=!<>&|^~?:]+/g,
        'punctuation': /[{}[\];(),.:]/g,
        'class-name': /\b[A-Z]\w*\b/g
    };

    // Highlight function
    function highlight(code, grammar) {
        let tokens = [];
        let lastIndex = 0;

        // Process each token type
        Object.keys(grammar).forEach(tokenType => {
            const pattern = grammar[tokenType];
            pattern.lastIndex = 0;
            
            let match;
            while ((match = pattern.exec(code)) !== null) {
                tokens.push({
                    type: tokenType,
                    content: match[0],
                    index: match.index
                });
            }
        });

        // Sort tokens by position
        tokens.sort((a, b) => a.index - b.index);

        // Remove overlapping tokens (keep the first one)
        const cleanTokens = [];
        let lastEnd = 0;
        
        tokens.forEach(token => {
            if (token.index >= lastEnd) {
                cleanTokens.push(token);
                lastEnd = token.index + token.content.length;
            }
        });

        // Build highlighted HTML
        let result = '';
        let currentIndex = 0;

        cleanTokens.forEach(token => {
            // Add any text before this token
            if (token.index > currentIndex) {
                result += escapeHtml(code.slice(currentIndex, token.index));
            }
            
            // Add the highlighted token
            result += `<span class="token ${token.type}">${escapeHtml(token.content)}</span>`;
            currentIndex = token.index + token.content.length;
        });

        // Add any remaining text
        if (currentIndex < code.length) {
            result += escapeHtml(code.slice(currentIndex));
        }

        return result;
    }

    // Escape HTML
    function escapeHtml(text) {
        const div = document.createElement('div');
        div.textContent = text;
        return div.innerHTML;
    }

    // Main highlight function
    Prism.highlight = function(code, grammar, language) {
        if (language === 'csharp' || language === 'cs') {
            return highlight(code, csharpTokens);
        }
        return escapeHtml(code);
    };

    // Highlight all code blocks
    Prism.highlightAll = function() {
        const codeBlocks = document.querySelectorAll('code[class*="language-"]');
        
        codeBlocks.forEach(block => {
            const className = block.className;
            const languageMatch = className.match(/language-(\w+)/);
            
            if (languageMatch) {
                const language = languageMatch[1];
                const code = block.textContent;
                
                if (language === 'csharp' || language === 'cs') {
                    block.innerHTML = highlight(code, csharpTokens);
                }
            }
        });
    };

    // Grammar for C#
    Prism.languages = Prism.languages || {};
    Prism.languages.csharp = csharpTokens;
    Prism.languages.cs = csharpTokens;

})();