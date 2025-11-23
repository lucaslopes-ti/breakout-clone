# Instruções para Compilar os Documentos LaTeX

Este documento explica como compilar os arquivos LaTeX para gerar PDFs visualmente agradáveis.

## 📄 Documentos Disponíveis

1. **PLANO_DE_AULA.tex** - Plano de aula completo com teoria e exercícios
2. **CADERNO_PRATICO.tex** - Caderno prático com exercícios progressivos passo a passo

## 📋 Pré-requisitos

### 1. Instalar LaTeX

**Windows:**
- Baixe e instale o **MiKTeX** ou **TeX Live**
- Download MiKTeX: https://miktex.org/download
- Download TeX Live: https://www.tug.org/texlive/

**Linux:**
```bash
# Ubuntu/Debian
sudo apt-get install texlive-full

# Fedora
sudo dnf install texlive-scheme-full
```

**macOS:**
- Instale via Homebrew: `brew install --cask mactex`
- Ou baixe MacTeX: https://www.tug.org/mactex/

### 2. Editor LaTeX (Opcional, mas Recomendado)

- **TeXstudio** (recomendado): https://www.texstudio.org/
- **Overleaf** (online): https://www.overleaf.com/
- **VS Code** com extensão LaTeX Workshop

## 🔧 Compilação

### Método 1: Linha de Comando

**Windows (PowerShell):**
```powershell
# Compilar Plano de Aula
pdflatex PLANO_DE_AULA.tex
pdflatex PLANO_DE_AULA.tex  # Execute duas vezes para referências

# Compilar Caderno Prático
pdflatex CADERNO_PRATICO.tex
pdflatex CADERNO_PRATICO.tex  # Execute duas vezes para referências
```

**Linux/macOS:**
```bash
# Compilar Plano de Aula
pdflatex PLANO_DE_AULA.tex
pdflatex PLANO_DE_AULA.tex  # Execute duas vezes para referências

# Compilar Caderno Prático
pdflatex CADERNO_PRATICO.tex
pdflatex CADERNO_PRATICO.tex  # Execute duas vezes para referências
```

### Método 2: TeXstudio

**Para Plano de Aula:**
1. Abra o arquivo `PLANO_DE_AULA.tex` no TeXstudio
2. Clique no botão **Build & View** (F5)
3. O PDF será gerado automaticamente

**Para Caderno Prático:**
1. Abra o arquivo `CADERNO_PRATICO.tex` no TeXstudio
2. Clique no botão **Build & View** (F5)
3. O PDF será gerado automaticamente

### Método 3: Overleaf (Online)

1. Acesse https://www.overleaf.com/
2. Crie uma nova conta ou faça login
3. Clique em **New Project** > **Upload Project**
4. Faça upload do arquivo desejado (`PLANO_DE_AULA.tex` ou `CADERNO_PRATICO.tex`)
5. Clique em **Recompile** (botão verde)
6. O PDF será gerado automaticamente

**Nota:** Você pode criar projetos separados para cada documento ou fazer upload de ambos no mesmo projeto.

## 📦 Pacotes Necessários

O documento usa os seguintes pacotes LaTeX (geralmente já incluídos nas distribuições completas):

- `inputenc` - Codificação de entrada UTF-8
- `babel` - Suporte a português
- `geometry` - Margens da página
- `xcolor` - Cores
- `tcolorbox` - Boxes coloridos
- `listings` - Syntax highlighting de código
- `hyperref` - Links e referências
- `fancyhdr` - Cabeçalhos e rodapés
- `titlesec` - Formatação de títulos
- `enumitem` - Listas customizadas
- `graphicx` - Imagens
- `amsmath` - Matemática
- `fontawesome5` - Ícones

### Instalar Pacotes Faltantes

**MiKTeX (Windows):**
- Os pacotes são instalados automaticamente quando necessário
- Se pedir confirmação, escolha "Install"

**TeX Live:**
```bash
# Verificar se pacote está instalado
tlmgr info tcolorbox

# Instalar pacote
sudo tlmgr install tcolorbox fontawesome5
```

## 🎨 Características do Documento

O PDF gerado terá:

- ✅ **Capa profissional** com informações do curso
- ✅ **Índice automático** (Table of Contents)
- ✅ **Cores personalizadas** (azul, verde, laranja, vermelho)
- ✅ **Boxes coloridos** para:
  - Teoria (azul)
  - Exercícios (verde)
  - Dicas (laranja)
  - Importante (vermelho)
  - Checklist (azul claro)
- ✅ **Syntax highlighting** para código GDScript e C#
- ✅ **Cabeçalhos e rodapés** personalizados
- ✅ **Links clicáveis** para recursos externos
- ✅ **Ícones FontAwesome** para melhor visualização
- ✅ **Layout profissional** e fácil de ler

## 🐛 Solução de Problemas

### Erro: "Package not found"

**Solução:**
- Instale o pacote faltante usando o gerenciador de pacotes
- No MiKTeX, os pacotes são instalados automaticamente
- No TeX Live, use `tlmgr install nome_do_pacote`

### Erro: "FontAwesome5 not found"

**Solução:**
```bash
# TeX Live
sudo tlmgr install fontawesome5

# MiKTeX: instale via Package Manager
```

### Erro: "Undefined control sequence"

**Solução:**
- Verifique se todos os pacotes estão instalados
- Execute `pdflatex` duas vezes para resolver referências

### PDF não gera

**Solução:**
1. Verifique se há erros no console
2. Execute `pdflatex` duas vezes
3. Se necessário, execute também `bibtex` (se usar bibliografia)

### Código não aparece formatado

**Solução:**
- Verifique se o pacote `listings` está instalado
- O syntax highlighting pode variar dependendo da distribuição

## 📄 Resultado Final

Após a compilação bem-sucedida, você terá:

**Para PLANO_DE_AULA.tex:**
- `PLANO_DE_AULA.pdf` - Documento final formatado
- `PLANO_DE_AULA.aux` - Arquivo auxiliar (pode ser deletado)
- `PLANO_DE_AULA.log` - Log de compilação (pode ser deletado)
- `PLANO_DE_AULA.toc` - Índice (pode ser deletado)

**Para CADERNO_PRATICO.tex:**
- `CADERNO_PRATICO.pdf` - Documento final formatado
- `CADERNO_PRATICO.aux` - Arquivo auxiliar (pode ser deletado)
- `CADERNO_PRATICO.log` - Log de compilação (pode ser deletado)
- `CADERNO_PRATICO.toc` - Índice (pode ser deletado)

**Mantenha apenas os arquivos `.pdf`!**

## 💡 Dicas

1. **Compile duas vezes**: Sempre execute `pdflatex` duas vezes para garantir que referências e índice sejam gerados corretamente

2. **Use Overleaf**: Se tiver problemas com instalação local, use Overleaf (online e gratuito)

3. **Verifique o log**: Se houver erros, verifique o arquivo `.log` para detalhes

4. **Personalização**: Você pode modificar as cores no arquivo `.tex` alterando os valores RGB nas definições de cores

## 📚 Recursos Adicionais

- [Documentação LaTeX](https://www.latex-project.org/help/documentation/)
- [Overleaf Learn](https://www.overleaf.com/learn)
- [TeX Stack Exchange](https://tex.stackexchange.com/) - Para dúvidas

---

**Boa compilação! 📄✨**

