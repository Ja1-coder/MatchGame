# 🧠 Jogo da Memória - Animais

Um jogo da memória desenvolvido em C# utilizando .NET e XAML, onde o objetivo é encontrar todos os pares de animais no menor tempo possível.

## 🎮 Sobre o jogo

O jogo consiste em um tabuleiro com cartas de animais viradas para baixo. O jogador deve clicar nas cartas para revelá-las e encontrar os pares correspondentes.

- Existem **8 pares de animais** (16 cartas no total)
- Ao clicar em uma carta, ela é revelada
- Ao clicar na segunda carta:
  - ✅ Se for o par correto → ambas desaparecem
  - ❌ Se não for → a primeira carta volta ao estado inicial
- O jogo termina quando todos os pares são encontrados

## ⏱️ Mecânica de tempo

O jogo possui um cronômetro que:

- Inicia ao abrir o jogo
- Conta o tempo total da partida
- Serve como critério de desempenho (quanto menor, melhor)

## 🧩 Tecnologias utilizadas

- C#
- .NET
- XAML (interface gráfica)

## 🧠 Lógica do jogo

A lógica principal do jogo funciona da seguinte forma:

1. O jogador clica na primeira carta
2. O sistema armazena essa seleção
3. O jogador clica na segunda carta
4. O sistema compara ambas:
   - Se forem iguais → remove da tela
   - Se forem diferentes → reseta a primeira carta
5. O processo se repete até finalizar todos os pares

## ▶️ Como executar o projeto

1. Clone o repositório:
```bash
git clone https://github.com/Ja1-coder/MatchGame.git
