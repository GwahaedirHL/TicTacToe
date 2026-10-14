# Tic-Tac-Toe — Zenject test task

Two-player hot-seat Tic-Tac-Toe with save/restore between launches, built with **Zenject**.

## Task

- Every dependency is injected with Zenject
- Game state persists between app launches
- A New Game button reloads the scene and re-initializes the game; available during a match
- Two players on one device; no AI, no networking
- A result popup at the end of the game

## Implementation

- **DI.** `Installer` (`MonoInstaller`) binds settings, board state, factories and the UI manager; `PopupsCollectionInstaller` (`ScriptableObjectInstaller`) provides the popup prefabs.
- **Factories.** Cells and X / O tokens are created through Zenject `PlaceholderFactory`; `ElementFactory` builds the board and restores tokens from the saved state.
- **Visitor pattern.** Tokens implement `IPlacableToken`; `GameBoardStateManager` visits them to record X or O without type checks.
- **Board state.** A flat `TokenType` array. Win detection checks every row, column and both diagonals and works for any N×N size; the size is set in `GameSettings` (currently 6×6).
- **Persistence.** The board and the last turn are saved to JSON after every move and restored on launch; a finished game starts fresh.
- **UI.** `UIManager` creates the Win / Draw popups through the DI container from a prefab collection.

## Running

- Unity **2021.1.22f1**
- Open the `TicTacToeUnityProject/` folder in Unity Hub, then `Assets/Scenes/MainScene.unity`
