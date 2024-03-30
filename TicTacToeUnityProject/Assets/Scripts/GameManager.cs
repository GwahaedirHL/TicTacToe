using System.Collections.Generic;
using System.Linq;
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
        bool needStartNewGame = gameBoardState.CheckWin() || gameBoardState.CheckDraw();
        if (needStartNewGame)
            gameBoardState.RenewState();

        GenerateCells();
        currentInputState = gameBoardState.GetLastCashedToken() == TokenType.Cross ? TokenType.Zero : TokenType.Cross;
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
        if (currentInputState != TokenType.Cross && currentInputState != TokenType.Zero)
            return;

        clickedCell.DisableInput();

        if (currentInputState == TokenType.Cross)
        {
            InputCross(clickedCell);
            return;
        }

        if (currentInputState == TokenType.Zero)
        {
            InputZero(clickedCell);
            return;
        }
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
        if (gameBoardState.CheckWin())
        {
            foreach (var cell in allCells)
                cell.DisableInput();

            uiManager.OpenWinPopup(gameBoardState.GetLastCashedToken());
            return;
        }

        if (gameBoardState.CheckDraw())
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