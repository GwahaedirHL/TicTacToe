using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class GameManager : MonoBehaviour
{
    ElementFactory elementFactory;
    GameBoardStateManager gameBoardState;
    UIManager uiManager;
    List<CellView> allCells;
    TokenType currentInputState;

    [Inject]
    public void Construct(ElementFactory elementFactory, GameBoardStateManager state, UIManager uiManager)
    {
        this.elementFactory = elementFactory;
        this.uiManager = uiManager;
        gameBoardState = state;
    }

    private void Start()
    {
        gameBoardState.Load();
        bool needStartNewGame = gameBoardState.IsWin|| gameBoardState.IsDraw;
        if (needStartNewGame)
            gameBoardState.RenewState();

        GenerateCells();
        currentInputState = gameBoardState.NextMoveToken;
    }

    void GenerateCells()
    {
        allCells = elementFactory.CreateBoardWithCells();

        foreach (CellView cell in allCells)
            if(!cell.HasToken)
                cell.CellClicked += RegisterInputCell;
    }

    void RegisterInputCell(CellView clickedCell)
    {
        if (currentInputState == TokenType.Empty)
            return;

        clickedCell.DisableInput();

        if (currentInputState == TokenType.Cross)
        {
            InputCross(clickedCell);
            return;
        }
        else
            InputZero(clickedCell);
    }

    void InputZero(CellView cell)
    {
        IPlacableToken token = elementFactory.CreateZeroAt(cell);
        UpdateBoardState(token);
        currentInputState = TokenType.Cross;
    }

    void InputCross(CellView cell) 
    {
        IPlacableToken token = elementFactory.CreateCrossAt(cell);
        UpdateBoardState(token);
        currentInputState = TokenType.Zero;
    }

    void UpdateBoardState(IPlacableToken token)
    {
        gameBoardState.UpdateGameBoardState(token);
        gameBoardState.Save();
        CheckForWin();
    }

    void CheckForWin()
    {
        if (gameBoardState.IsWin)
        {
            foreach (var cell in allCells)
                cell.DisableInput();

            uiManager.OpenWinPopup(gameBoardState.LastCashedToken);
            return;
        }

        if (gameBoardState.IsDraw)
            uiManager.OpenDrawPopup();
    }

    public void StartNewGame()
    {
        gameBoardState.RenewState();
        gameBoardState.Save();

        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
}