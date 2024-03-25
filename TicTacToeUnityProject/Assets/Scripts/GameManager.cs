using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameManager : MonoBehaviour
{
    enum InputState
    {
        CrossInput = 0,
        ZeroInput = 1,
        CrossWin = 2,
        ZeroWin = 3,
        Draw = 4,
        WaitingCrossInput = 5,
        WaitingZeroInput = 6,
        Checking = 7
    } 


    ElementFactory elementFactory;
    GameBoardState gameBoardState;
    List<CellView> allCells;
    InputState currentInputState;

    [Inject]
    public void Construct(ElementFactory elementFactory, GameBoardState state)
    {
        this.elementFactory = elementFactory;
        gameBoardState = state;
        currentInputState = InputState.WaitingCrossInput;
    }

    private void Start()
    {
        GenerateCells(GameDataManager.Load());
    }

    void GenerateCells(GameBoardState gameBoardState)
    {
        allCells = elementFactory.CreateBoardWithCells(gameBoardState);

        foreach (CellView cell in allCells)
            if(!cell.HasToken)
                cell.CellClicked += RegisterInputCell;

    }

    void RegisterInputCell(CellView clickedCell)
    {
        if (currentInputState != InputState.WaitingCrossInput && currentInputState != InputState.WaitingZeroInput)
            return;

        clickedCell.CellClicked -= RegisterInputCell;

        if (currentInputState == InputState.WaitingCrossInput)
        {
            InputCross(clickedCell);
            return;
        }

        if (currentInputState == InputState.WaitingZeroInput)
        {
            InputZero(clickedCell);
            return;
        }
    }

    void InputZero(CellView cell)
    {
        IPlacableToken token = elementFactory.CreateZeroAt(cell);
        UpdateBoardState(token);
        currentInputState = InputState.WaitingCrossInput;
    }

    void InputCross(CellView cell) 
    {
        IPlacableToken token = elementFactory.CreateCrossAt(cell);
        UpdateBoardState(token);
        currentInputState = InputState.WaitingZeroInput;
    }

    void UpdateBoardState(IPlacableToken token)
    {
        gameBoardState.UpdateGameBoardState(token);
        GameDataManager.Save(gameBoardState);
        CheckForWin();
    }

    void CheckForWin()
    {
        if (gameBoardState.CheckWin())
        {

        }
    }

    public void StartNewGame()
    {
        gameBoardState = new GameBoardState();
        foreach (CellView cellview in allCells)
            Destroy(cellview.gameObject);

        GenerateCells(gameBoardState);
        GameDataManager.Save(gameBoardState);
        currentInputState = InputState.WaitingCrossInput;
    }
}