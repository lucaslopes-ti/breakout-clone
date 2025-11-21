# 🎮 Breakout Clone

Um jogo clássico estilo Breakout desenvolvido em **Godot 4.5** usando **GDScript**.

> 📚 **Projeto Acadêmico**  
> Este projeto foi desenvolvido como parte das aulas de **Programação de Jogos Digitais** no **SENAI** (Serviço Nacional de Aprendizagem Industrial).

## 📋 Sobre o Jogo

Breakout Clone é uma recriação do clássico jogo Breakout, onde o objetivo é destruir todos os blocos na tela usando uma bola que rebate em uma raquete controlada pelo jogador. O jogo apresenta física realista, sistema de pontuação, vidas e uma experiência de jogo fluida.

## 🎯 Objetivo do Jogo

- **Destrua todos os blocos** na tela usando a bola
- **Evite que a bola caia** pela parte inferior da tela
- **Acumule pontos** destruindo os blocos
- **Complete o nível** destruindo todos os 50 blocos (5 linhas × 10 colunas)

## 🕹️ Como Jogar

### Controles

- **A** ou **←** (Seta Esquerda): Move a raquete para a esquerda
- **D** ou **→** (Seta Direita): Move a raquete para a direita

### Mecânicas do Jogo

1. **Movimento da Bola**: A bola se move automaticamente após 1 segundo do início do jogo
2. **Rebatida na Raquete**: O ângulo de rebatida depende de onde a bola bate na raquete:
   - Centro da raquete: rebatida mais vertical
   - Extremidades: rebatida mais inclinada (até 45 graus)
3. **Destruição de Blocos**: Quando a bola colide com um bloco, ele é destruído e você ganha pontos
4. **Vidas**: Você começa com **3 vidas**. Se a bola cair pela parte inferior, você perde uma vida
5. **Vitória**: Destrua todos os blocos para vencer!
6. **Game Over**: Se perder todas as vidas, o jogo termina

### Dicas

- Tente controlar o ângulo da rebatida usando as extremidades da raquete
- A bola acelera levemente a cada rebatida na raquete (até um limite)
- Mantenha a bola em movimento para evitar que ela fique presa

## 🚀 Como Abrir e Executar o Jogo

### Pré-requisitos

- **Godot Engine 4.5** ou superior
  - Download: [https://godotengine.org/download](https://godotengine.org/download)
  - Escolha a versão **Standard** (não precisa de .NET)

### Instruções de Instalação

1. **Clone ou baixe o repositório**
   ```bash
   git clone https://github.com/seu-usuario/breakout-clone.git
   cd breakout-clone
   ```

2. **Abra o projeto no Godot**
   - Abra o Godot Engine
   - Clique em **"Import"** ou **"Importar"**
   - Navegue até a pasta do projeto
   - Selecione o arquivo `project.godot`
   - Clique em **"Import & Edit"** ou **"Importar e Editar"**

3. **Execute o jogo**
   - No editor do Godot, pressione **F5** ou clique no botão **▶️ Play** no canto superior direito
   - Ou vá em **Project > Run Project** (Projeto > Executar Projeto)
   - A cena principal (`Main.tscn`) será executada automaticamente

### Estrutura do Projeto

```
breakout-clone/
├── scenes/           # Cenas do jogo
│   ├── Main.tscn     # Cena principal do jogo
│   ├── Ball.tscn     # Cena da bola
│   ├── Paddle.tscn   # Cena da raquete
│   └── Block.tscn    # Cena do bloco
├── scripts/          # Scripts GDScript
│   ├── Ball.gd       # Lógica da bola
│   ├── Paddle.gd     # Lógica da raquete
│   ├── Block.gd      # Lógica dos blocos
│   └── GameManager.gd # Gerenciador principal do jogo
├── project.godot      # Arquivo de configuração do projeto
└── README.md         # Este arquivo
```

## 🎨 Características

- ✅ Física de colisão realista
- ✅ Sistema de pontuação
- ✅ Sistema de vidas (3 vidas)
- ✅ Interface de usuário (UI) com pontuação e vidas
- ✅ Tela de Game Over e Vitória
- ✅ Botão de reiniciar
- ✅ Blocos coloridos por linha
- ✅ Velocidade progressiva da bola
- ✅ Rebatida dinâmica baseada na posição de impacto

## 🛠️ Tecnologias Utilizadas

- **Godot Engine 4.5**
- **GDScript**
- **Godot Physics Engine**

## 📝 Notas de Desenvolvimento

Este projeto foi desenvolvido como parte do curso de **Programação de Jogos Digitais** no **SENAI**, com o objetivo de aprender e praticar:
- Desenvolvimento de jogos 2D no Godot Engine
- Implementação de física de colisão
- Gerenciamento de estados de jogo
- Criação de interfaces de usuário (UI)
- Programação orientada a objetos com GDScript

O projeto é um clone do clássico jogo Breakout, focado em:
- Física de colisão precisa
- Experiência de jogo fluida
- Código limpo e bem documentado

## 🤝 Contribuindo

Contribuições são bem-vindas! Sinta-se à vontade para:
- Reportar bugs
- Sugerir melhorias
- Enviar pull requests

## 📄 Licença

Este projeto é de código aberto e está disponível para uso livre.

## 👤 Autor

Desenvolvido como projeto acadêmico para as aulas de **Programação de Jogos Digitais** no **SENAI**.

Este projeto faz parte do aprendizado prático de desenvolvimento de jogos usando a engine Godot, abordando conceitos fundamentais de programação de jogos, física, colisões e gerenciamento de estados.

---

**Divirta-se jogando! 🎮✨**

