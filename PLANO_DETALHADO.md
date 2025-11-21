# 🎮 PLANO DETALHADO - JOGO BREAKOUT

## 📋 VISÃO GERAL DO PROJETO

Este é um jogo estilo Breakout desenvolvido em **Godot 4.5** usando **GDScript**. O jogo já possui uma estrutura básica implementada, mas precisa de ajustes e melhorias para estar completo e funcional.

---

## ✅ O QUE JÁ ESTÁ IMPLEMENTADO

### 1. **Estrutura Básica**
- ✅ Scripts principais (Ball.gd, Paddle.gd, Block.gd, GameManager.gd)
- ✅ Cenas básicas (Ball.tscn, Paddle.tscn, Block.tscn, Main.tscn)
- ✅ Sistema de colisões básico
- ✅ UI básica (pontuação, vidas, game over)

### 2. **Funcionalidades Parciais**
- ✅ Movimento da bola
- ✅ Movimento da raquete (mas falta configurar inputs)
- ✅ Sistema de blocos destruíveis
- ✅ Sistema de vidas e pontuação

---

## 🔧 TAREFAS NECESSÁRIAS

### **FASE 1: CONFIGURAÇÃO BÁSICA E CORREÇÕES**

#### 1.1 Configurar Inputs do Jogador
**Prioridade: ALTA** ⚠️
- [ ] Abrir o editor do Godot
- [ ] Ir em `Project > Project Settings > Input Map`
- [ ] Criar ação `move_left` e mapear para:
  - Tecla `A` ou `←` (seta esquerda)
- [ ] Criar ação `move_right` e mapear para:
  - Tecla `D` ou `→` (seta direita)
- [ ] Testar se a raquete se move corretamente

**Arquivo afetado:** `project.godot` (será modificado automaticamente pelo editor)

---

#### 1.2 Verificar e Corrigir Visual da Bola
**Prioridade: MÉDIA**
- [ ] Verificar se a bola aparece na tela (atualmente usa ColorRect quadrado)
- [ ] Considerar trocar para um `Sprite2D` com textura circular ou usar `draw_circle()` no script
- [ ] Garantir que a bola seja visível e tenha tamanho adequado (raio 10px)

**Arquivos:** `scenes/Ball.tscn`, `scripts/Ball.gd`

---

#### 1.3 Ajustar Visual dos Blocos
**Prioridade: MÉDIA**
- [ ] Verificar se os blocos estão visíveis e bem posicionados
- [ ] Garantir que as cores estão sendo aplicadas corretamente
- [ ] Verificar se o sistema de cores por linha está funcionando

**Arquivos:** `scenes/Block.tscn`, `scripts/Block.gd`, `scripts/GameManager.gd`

---

### **FASE 2: MELHORIAS DE GAMEPLAY**

#### 2.1 Sistema de Pontuação por Tipo de Bloco
**Prioridade: MÉDIA**
- [ ] Modificar `Block.gd` para ter diferentes valores de pontos por linha
  - Linha superior (vermelha): 50 pontos
  - Linha 2 (laranja): 40 pontos
  - Linha 3 (amarela): 30 pontos
  - Linha 4 (verde): 20 pontos
  - Linha 5 (azul): 10 pontos
- [ ] Atualizar `GameManager.gd` para usar os pontos do bloco ao invés de valor fixo
- [ ] Testar se a pontuação está sendo calculada corretamente

**Arquivos:** `scripts/Block.gd`, `scripts/GameManager.gd`

---

#### 2.2 Sistema de Blocos com Diferentes Resistências
**Prioridade: BAIXA** (opcional, mas interessante)
- [ ] Adicionar propriedade `hits_required` ao `Block.gd`
- [ ] Criar blocos que precisam ser atingidos 2-3 vezes antes de quebrar
- [ ] Adicionar feedback visual (mudança de cor) quando o bloco é atingido
- [ ] Atualizar sistema de pontuação para considerar múltiplos hits

**Arquivos:** `scripts/Block.gd`, `scripts/Ball.gd`

---

#### 2.3 Melhorar Sistema de Rebatida da Bola
**Prioridade: MÉDIA**
- [ ] Verificar se o cálculo de ângulo de rebatida está funcionando corretamente
- [ ] Ajustar a sensibilidade do ângulo de rebatida
- [ ] Garantir que a bola não fique presa em loops infinitos
- [ ] Adicionar velocidade mínima para evitar que a bola fique muito lenta

**Arquivos:** `scripts/Ball.gd`

---

### **FASE 3: POLIMENTO VISUAL E AUDIO**

#### 3.1 Melhorar Visual dos Elementos
**Prioridade: MÉDIA**
- [ ] Adicionar sprites/texturas ao invés de apenas ColorRect
- [ ] Criar ou importar sprites para:
  - Bola (círculo com gradiente ou textura)
  - Raquete (retângulo com bordas arredondadas)
  - Blocos (com bordas e sombras)
- [ ] Adicionar efeitos visuais quando blocos são destruídos (partículas)
- [ ] Melhorar UI com fontes melhores e layout mais atrativo

**Arquivos:** `scenes/*.tscn`, criar pasta `assets/sprites/`

---

#### 3.2 Adicionar Efeitos Sonoros
**Prioridade: BAIXA** (opcional)
- [ ] Adicionar som quando a bola rebate na raquete
- [ ] Adicionar som quando um bloco é destruído
- [ ] Adicionar som quando perde uma vida
- [ ] Adicionar música de fundo (opcional)
- [ ] Criar pasta `assets/audio/` e importar arquivos de áudio

**Arquivos:** `scripts/Ball.gd`, `scripts/Block.gd`, `scripts/GameManager.gd`

---

#### 3.3 Adicionar Animações
**Prioridade: BAIXA** (opcional)
- [ ] Animação quando bloco é destruído (escala ou fade out)
- [ ] Animação quando bola é perdida
- [ ] Efeito de "shake" na tela quando perde vida
- [ ] Transições suaves entre estados do jogo

**Arquivos:** `scripts/Block.gd`, `scripts/GameManager.gd`

---

### **FASE 4: FUNCIONALIDADES EXTRAS**

#### 4.1 Sistema de Power-ups
**Prioridade: BAIXA** (opcional, mas muito legal)
- [ ] Criar cena `PowerUp.tscn` com diferentes tipos:
  - Expandir raquete
  - Multi-bola
  - Bola lenta
  - Bola rápida
  - Raquete magnética (bola gruda na raquete)
- [ ] Fazer alguns blocos soltarem power-ups quando destruídos
- [ ] Implementar lógica de cada power-up
- [ ] Adicionar timer para power-ups temporários

**Arquivos:** Criar `scenes/PowerUp.tscn`, `scripts/PowerUp.gd`, modificar `scripts/Block.gd`, `scripts/Paddle.gd`, `scripts/Ball.gd`

---

#### 4.2 Sistema de Níveis
**Prioridade: MÉDIA**
- [ ] Criar sistema para múltiplos níveis
- [ ] Diferentes layouts de blocos para cada nível
- [ ] Aumentar dificuldade progressivamente
- [ ] Tela de transição entre níveis
- [ ] Salvar progresso (opcional)

**Arquivos:** `scripts/GameManager.gd`, criar `scripts/LevelManager.gd`

---

#### 4.3 Sistema de High Score
**Prioridade: BAIXA** (opcional)
- [ ] Salvar pontuação máxima localmente
- [ ] Exibir high score na UI
- [ ] Parabenizar quando bater recorde

**Arquivos:** `scripts/GameManager.gd`, usar `ConfigFile` ou `FileAccess` do Godot

---

#### 4.4 Menu Principal
**Prioridade: MÉDIA**
- [ ] Criar cena `Menu.tscn`
- [ ] Botão "Iniciar Jogo"
- [ ] Botão "Instruções" (opcional)
- [ ] Botão "Sair"
- [ ] Configurar como cena principal do projeto

**Arquivos:** Criar `scenes/Menu.tscn`, `scripts/Menu.gd`, modificar `project.godot`

---

### **FASE 5: TESTES E CORREÇÕES**

#### 5.1 Testes de Funcionalidade
**Prioridade: ALTA** ⚠️
- [ ] Testar movimento da raquete
- [ ] Testar colisões da bola com todos os elementos
- [ ] Testar destruição de blocos
- [ ] Testar sistema de vidas
- [ ] Testar game over e vitória
- [ ] Testar reinício do jogo
- [ ] Verificar se não há bugs de física (bola presa, etc.)

---

#### 5.2 Testes de Performance
**Prioridade: BAIXA**
- [ ] Verificar FPS durante gameplay
- [ ] Otimizar se necessário (limitar número de blocos, etc.)
- [ ] Testar em diferentes resoluções

---

#### 5.3 Correção de Bugs
**Prioridade: ALTA** ⚠️
- [ ] Corrigir qualquer bug encontrado durante testes
- [ ] Verificar se a bola não sai da tela pelas laterais
- [ ] Garantir que todos os blocos podem ser destruídos
- [ ] Verificar se o jogo funciona corretamente em diferentes tamanhos de tela

---

## 🎯 ORDEM RECOMENDADA DE EXECUÇÃO

### **PRIORIDADE CRÍTICA (Fazer Primeiro)**
1. ✅ Configurar Inputs (1.1) - **SEM ISSO O JOGO NÃO FUNCIONA**
2. ✅ Testes básicos (5.1) - **VERIFICAR O QUE ESTÁ QUEBRADO**
3. ✅ Correções de bugs críticos (5.3)

### **PRIORIDADE ALTA (Fazer Depois)**
4. ✅ Melhorar visual da bola (1.2)
5. ✅ Ajustar visual dos blocos (1.3)
6. ✅ Sistema de pontuação por tipo (2.1)
7. ✅ Melhorar rebatida da bola (2.3)

### **PRIORIDADE MÉDIA (Melhorias Importantes)**
8. ✅ Menu principal (4.4)
9. ✅ Sistema de níveis (4.2)
10. ✅ Polimento visual (3.1)

### **PRIORIDADE BAIXA (Extras e Opcionais)**
11. ✅ Blocos com múltiplas resistências (2.2)
12. ✅ Efeitos sonoros (3.2)
13. ✅ Animações (3.3)
14. ✅ Power-ups (4.1)
15. ✅ High score (4.3)

---

## 📝 NOTAS IMPORTANTES

### **Problemas Conhecidos que Precisam ser Resolvidos:**
1. **Inputs não configurados** - O jogo não funcionará até configurar as ações de input
2. **Bola usa ColorRect quadrado** - Visualmente não ideal, considerar usar Sprite2D ou desenhar círculo
3. **Falta verificar se tudo está conectado** - Pode haver referências quebradas nas cenas

### **Dicas de Implementação:**
- Use o editor visual do Godot para configurar inputs (não edite project.godot manualmente)
- Teste frequentemente após cada mudança
- Use `print()` ou `print_debug()` para debugar problemas
- Considere usar grupos do Godot para facilitar comunicação entre nós

### **Recursos Úteis:**
- Documentação do Godot: https://docs.godotengine.org/
- Tutoriais de Breakout no YouTube
- Asset Store do Godot para sprites e sons gratuitos

---

## 🚀 COMO COMEÇAR

1. **Abra o projeto no Godot**
2. **Configure os inputs primeiro** (Fase 1.1)
3. **Execute o jogo** e veja o que funciona e o que não funciona
4. **Corrija os problemas críticos**
5. **Siga a ordem de prioridade** listada acima

---

**Boa sorte com o desenvolvimento! 🎮✨**

