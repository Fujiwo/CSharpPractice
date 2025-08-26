// Minimal marked.js implementation for markdown parsing
(function() {
    'use strict';

    // Simple markdown parser
    function parseMarkdown(md) {
        // Remove leading/trailing whitespace
        md = md.trim();

        // Convert headers
        md = md.replace(/^### (.*$)/gim, '<h3>$1</h3>');
        md = md.replace(/^## (.*$)/gim, '<h2>$1</h2>');
        md = md.replace(/^# (.*$)/gim, '<h1>$1</h1>');

        // Convert bold
        md = md.replace(/\*\*(.*)\*\*/gim, '<strong>$1</strong>');

        // Convert italic
        md = md.replace(/\*(.*)\*/gim, '<em>$1</em>');

        // Convert code blocks (```language\ncode\n```)
        md = md.replace(/```(\w+)?\n([\s\S]*?)\n```/gim, function(match, lang, code) {
            lang = lang || '';
            return `<pre><code class="language-${lang}">${escapeHtml(code)}</code></pre>`;
        });

        // Convert inline code
        md = md.replace(/`([^`]+)`/gim, '<code>$1</code>');

        // Convert unordered lists
        md = md.replace(/^\* (.+$)/gim, '<li>$1</li>');
        md = md.replace(/(<li>.*<\/li>)/s, '<ul>$1</ul>');

        // Convert ordered lists
        md = md.replace(/^\d+\. (.+$)/gim, '<li>$1</li>');

        // Convert paragraphs (simple approach)
        const lines = md.split('\n');
        let result = '';
        let inList = false;
        let currentParagraph = '';

        for (let i = 0; i < lines.length; i++) {
            const line = lines[i].trim();
            
            if (line === '') {
                if (currentParagraph && !inList) {
                    result += `<p>${currentParagraph}</p>\n`;
                    currentParagraph = '';
                }
                continue;
            }
            
            if (line.startsWith('<h') || line.startsWith('<pre>') || line.startsWith('<ul>') || line.startsWith('<ol>')) {
                if (currentParagraph && !inList) {
                    result += `<p>${currentParagraph}</p>\n`;
                    currentParagraph = '';
                }
                result += line + '\n';
                inList = line.startsWith('<ul>') || line.startsWith('<ol>');
            } else if (line.startsWith('</ul>') || line.startsWith('</ol>')) {
                result += line + '\n';
                inList = false;
            } else if (line.startsWith('<li>') || line.includes('</code></pre>')) {
                result += line + '\n';
            } else {
                if (currentParagraph) {
                    currentParagraph += ' ' + line;
                } else {
                    currentParagraph = line;
                }
            }
        }

        if (currentParagraph && !inList) {
            result += `<p>${currentParagraph}</p>\n`;
        }

        return result;
    }

    // Escape HTML
    function escapeHtml(text) {
        const div = document.createElement('div');
        div.textContent = text;
        return div.innerHTML;
    }

    // Global marked object
    window.marked = {
        parse: parseMarkdown
    };

})();