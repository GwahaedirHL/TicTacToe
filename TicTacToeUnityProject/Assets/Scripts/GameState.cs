using System;
using System.Collections.Generic;

public enum TokenType
{
    Empty = 0,
    Cross = 1,
    Zero = 2
}

[Serializable]
public class GameBoardState : IPlacableTokenVisitor
{
    //Because I'm too lazy to implement full serialization
    [NonSerialized]
    TokenType tokenType;

    public Dictionary<Cell, IPlacableToken> gameState;

    public int[] boardState = new int[9];

    public GameBoardState()
    {
        for (int i = 0; i < boardState.Length; i++)
        {
            boardState[i] = 0;
        }
    }

    public void UpdateGameBoardState(IPlacableToken token)
    {
        tokenType = TokenType.Empty;
        token.Accept(this);
        int index = GetIndex(token.Index);
        boardState[index] = (int)tokenType;
    }

    int GetIndex(Cell cell)
    {
        return cell.X + (cell.Y * 3);
    }

    public Cell GetCell(int index)
    {
        int x = index % 3;
        int y = index / 3;
        return new Cell(x, y);
    }

    public bool CheckWin() 
    {
        return GetColomnWin() || GetRowWin() || GetDiagonalWin();
    }


    bool GetColomnWin()
    {
        for (int i = 0; i < 3; i++)
        {
            if (boardState[i] != 0 && boardState[i] == boardState[i + 3] && boardState[i] == boardState[i + 6])
                return true;
        }
        return false;
    }

    bool GetRowWin()
    {
        for (int i = 0; i < boardState.Length; i+=3)
        {
            if (boardState[i] != 0 && boardState[i] == boardState[i + 1] && boardState[i] == boardState[i + 2])
                return true;
        }
        return false;
    }

    bool GetDiagonalWin()
    {
        bool diagonalA;
        bool diagonalB;

        int middle = boardState[4];
        if(middle != 0)
        {
            diagonalA = middle == boardState[0] && middle == boardState[8];
            diagonalB = middle == boardState[2] && middle == boardState[6];
            return diagonalA || diagonalB;
        }

        return false;
    }

    public void Visit(CrossToken token)
    {
        tokenType = TokenType.Cross;
    }

    public void Visit(ZeroToken token)
    {
        tokenType = TokenType.Zero;
    }
}

