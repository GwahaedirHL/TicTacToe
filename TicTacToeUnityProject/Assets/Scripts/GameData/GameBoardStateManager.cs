using System.Linq;

public enum TokenType
{
    Empty = 0,
    Cross = 1,
    Zero = 2
}

public class GameBoardStateManager : IPlacableTokenVisitor
{
    TokenType lastPlayedToken;
    GameSettings settings;
    GameStateData cashedState;
    TokenType[] runtimeState;

    public TokenType[] CurrentState => runtimeState;
    public TokenType LastCashedToken => lastPlayedToken;
    public bool IsWin => GetColomnWin() || GetRowWin() || GetDiagonalWin();
    public bool IsDraw => runtimeState.All(token => token != TokenType.Empty);

    public GameBoardStateManager(GameSettings settings)
    {
        this.settings = settings;
    }

    public void Load()
    {
        cashedState = GameDataManager.Load();
        if (cashedState.Data == null || cashedState.Data.Length != settings.CellsCount)
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
        lastPlayedToken = TokenType.Zero;
    }

    public void UpdateGameBoardState(IPlacableToken token)
    {
        lastPlayedToken = TokenType.Empty;
        token.Accept(this);

        int index = GetIndex(token.Index);
        runtimeState[index] = lastPlayedToken;
    }

    public Cell GetCell(int index)
    {
        int x = index % settings.SideSize;
        int y = index / settings.SideSize;
        return new Cell(x, y);
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
        int step = settings.SideSize;
        bool isWin = false;

        for (int column = 0; column < settings.SideSize; column++)
        {
            if (runtimeState[column] == TokenType.Empty)
                continue;

            for (int cell = column + step; cell < settings.CellsCount; cell += step)
            {
                isWin = runtimeState[column] == runtimeState[cell];
                if (!isWin)
                    break;
            }

            if(isWin)
                break;
        }
        return isWin;
    }

    bool GetRowWin()
    {
        int step = settings.SideSize;
        bool isWin = false;

        for (int row = 0; row < settings.CellsCount; row+=step)
        {
            if (runtimeState[row] == TokenType.Empty)
                continue;

            int lastInRow = row + step;

            for (int cell = row + 1; cell < lastInRow; cell++)
            {
                isWin = runtimeState[row] == runtimeState[cell];
                if (!isWin)
                    break;
            }

            if (isWin)
                break;
        }
        return isWin;
    }

    bool GetDiagonalWin()
    {
        bool diagonalA = false;
        bool diagonalB = false;        

        int step = settings.SideSize + 1;
        for (int i = step; i < settings.CellsCount; i+= step)
        {
            if (runtimeState[0] == TokenType.Empty)
                break;

            diagonalA = runtimeState[0] == runtimeState[i];
            if (!diagonalA)
                break;
        }

        step = settings.SideSize - 1;
        for (int i = step + step; i <= settings.CellsCount-step; i += step)
        {
            if (runtimeState[step] == TokenType.Empty)
                break;

            diagonalB = runtimeState[step] == runtimeState[i];
            if (!diagonalB)
                break;
        }

        return diagonalA || diagonalB;
    }
}
