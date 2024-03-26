using System;
using Zenject;
using System.Collections.Generic;
using System.Linq;

public enum TokenType
{
    Empty = 0,
    Cross = 1,
    Zero = 2
}

public class GameBoardStateManager : IPlacableTokenVisitor
{
    //Because I'm too lazy to implement full serialization
    TokenType lastPlayedToken;
    GameSettings settings;
    GameStateData cashedState;
    TokenType[] runtimeState;

    public GameBoardStateManager(GameSettings settings)
    {
        this.settings = settings;
    }

    public void Load()
    {
        cashedState = GameDataManager.Load();
        if (cashedState.Data == null)
        {
            cashedState.Data = new TokenType[settings.CellsCount];
            cashedState.LastTurn = TokenType.Zero;
        }

        runtimeState = cashedState.Data;
        lastPlayedToken = cashedState.LastTurn;
    }

    public void Save()
    {
        cashedState.Data = runtimeState;
        cashedState.LastTurn = lastPlayedToken;
        GameDataManager.Save(cashedState);
    }

    public void RenewState()
    {
        runtimeState = new TokenType[settings.CellsCount];
        lastPlayedToken = TokenType.Cross;
    }

    public void UpdateGameBoardState(IPlacableToken token)
    {
        lastPlayedToken = TokenType.Empty;
        token.Accept(this);

        int index = GetIndex(token.Index);
        runtimeState[index] = lastPlayedToken;
    }

    public TokenType[] GetCurrentState()
    {
        return runtimeState;
    }

    public TokenType GetLastCashedToken()
    {
        return lastPlayedToken;
    }

    public Cell GetCell(int index)
    {
        int x = index % settings.SideSize;
        int y = index / settings.SideSize;
        return new Cell(x, y);
    }

    public bool CheckWin() 
    {
        return GetColomnWin() || GetRowWin() || GetDiagonalWin();
    }

    public bool CheckDraw()
    {
        return runtimeState.All(token => token != TokenType.Empty);
    }

    public void Visit(CrossToken token)
    {
        lastPlayedToken = TokenType.Cross;
    }

    public void Visit(ZeroToken token)
    {
        lastPlayedToken = TokenType.Zero;
    }

    int GetIndex(Cell cell)
    {
        return cell.X + (cell.Y * settings.SideSize);
    }

    bool GetColomnWin()
    {
        for (int i = 0; i < settings.SideSize; i++)
        {
            if (runtimeState[i] != TokenType.Empty && runtimeState[i] == runtimeState[i + 3] && runtimeState[i] == runtimeState[i + 6])
                return true;
        }
        return false;
    }

    bool GetRowWin()
    {
        for (int i = 0; i < settings.CellsCount; i+=3)
        {
            if (runtimeState[i] != TokenType.Empty && runtimeState[i] == runtimeState[i + 1] && runtimeState[i] == runtimeState[i + 2])
                return true;
        }
        return false;
    }

    bool GetDiagonalWin()
    {
        bool diagonalA;
        bool diagonalB;

        TokenType middle = runtimeState[4];
        if(middle != TokenType.Empty)
        {
            diagonalA = middle == runtimeState[0] && middle == runtimeState[8];
            diagonalB = middle == runtimeState[2] && middle == runtimeState[6];
            return diagonalA || diagonalB;
        }

        return false;
    }
}
