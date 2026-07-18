# Dots and Lines Game

A classic Windows desktop game implementation of "Dots and Lines" (also known as "Boxes") featuring an intelligent AI opponent powered by the Minimax algorithm with alpha-beta pruning.

## Overview

Dots and Lines is a strategic pen-and-paper game where players take turns drawing lines between dots on a grid. When a player completes the fourth side of a box, they score a point and get another turn. The player with the most boxes at the end wins. This implementation brings the game to a modern Windows Forms interface with a challenging computer opponent.

## Features

**Interactive GUI**
- Clean, intuitive graphical interface built with Windows Forms
- Real-time game visualization with smooth animations
- Responsive click detection for line placement

**Intelligent AI**
- Minimax algorithm with alpha-beta pruning optimization
- Adjustable difficulty levels
- Smart strategic decision-making

**Gameplay**
- Customizable grid sizes (4x4, 5x5, 6x6, etc.)
- Player vs Computer mode
- Score tracking for both players
- Color-coded moves (blue for human, red for computer)

## Requirements

- **.NET Framework 4.7.2** or higher
- **Windows OS** (Windows 7 or later)
- **Visual Studio 2022** (or any compatible C# IDE) for development

## Installation & Setup

1. **Clone the repository:**
   ```bash
   git clone https://github.com/Mateian/DotsNLines-Project.git
   cd DotsNLines-Project
   ```

2. **Open in Visual Studio:**
   - Launch Visual Studio
   - Select "Open a project or solution"
   - Navigate to `DotsNLines.sln` and open it

3. **Build the project:**
   - Press `Ctrl+Shift+B` or go to Build → Build Solution
   - Ensure all references are resolved

4. **Run the application:**
   - Press `F5` or click the Start button to launch the game

## How to Play

### Game Rules
1. Players alternate turns drawing lines between adjacent dots
2. When you complete the fourth side of a box, you score one point and take another turn
3. The game ends when all boxes are claimed
4. The player with the highest score wins

### Controls
- **Click and drag** between adjacent dots to draw a line
- **Select difficulty** from the dropdown menu before starting
- **New Game** button to reset and start fresh

### Difficulty Levels
- **Easy:** AI makes basic moves with limited lookahead
- **Medium:** AI uses moderate calculation depth for balanced play
- **Hard:** AI uses full minimax with alpha-beta pruning for optimal play

## Project Structure

```
DotsNLines/
├── Program.cs                 # Application entry point
├── Form1.cs / Form1.Designer.cs # Main UI form and layout
├── Board.cs                   # Game board logic and state management
├── Line.cs                    # Line class representing board lines
├── Box.cs                     # Box class representing board boxes
├── Minimax_alpha_beta.cs      # AI algorithm implementation
└── Properties/                # Application settings and resources
```

## Technical Implementation

### Core Components

**Board Management (`Board.cs`)**
- Maintains the game state (grid, lines, boxes, scores)
- Handles move validation and application
- Implements game-over detection

**AI Algorithm (`Minimax_alpha_beta.cs`)**
- Implements the Minimax algorithm with alpha-beta pruning
- Evaluates board positions using a scoring function
- Provides optimal move selection based on difficulty level
- Minimizes computation time through intelligent pruning

**User Interface (`Form1.cs`)**
- Renders the game board with dots and lines
- Detects player clicks and converts them to moves
- Updates display in real-time as game progresses

### Design Patterns
- **State Pattern:** Board manages and updates game state
- **Strategy Pattern:** Different AI difficulty levels use varying search depths

## Algorithm Details

The AI opponent uses the **Minimax algorithm** with **alpha-beta pruning** to determine optimal moves:

- **Minimax:** Recursively explores all possible game states assuming both players play optimally
- **Alpha-Beta Pruning:** Reduces computation by eliminating branches that cannot affect the final decision
- **Evaluation Function:** Scores positions based on the point difference (computer score - human score)

This ensures the computer provides a challenging and fair opponent.

## Performance Considerations

- Alpha-beta pruning reduces the search space significantly, enabling real-time decision-making
- Difficulty levels control search depth, allowing tuning between computation time and AI strength
- Board cloning during AI calculations ensures thread-safe operations

## Building for Release

To create a release build:

1. In Visual Studio, select **Release** configuration from the toolbar
2. Press `Ctrl+Shift+B` to build
3. The executable will be located in `bin\Release\DotsNLines.exe`

## Contributing

Contributions are welcome! If you'd like to improve the game:
- Fork the repository
- Create a feature branch
- Make your changes
- Submit a pull request

## License

This project is open source and available under the MIT License - see the LICENSE file for details.

## Acknowledgments

- Built with C# and Windows Forms
- Minimax algorithm implementation based on game theory principles
- Inspired by the classic paper-and-pencil game

## Contact

For questions or suggestions about this project, please open an issue on the [GitHub repository](https://github.com/Mateian/DotsNLines-Project).

---

**Have fun playing and may the best strategist win!**
