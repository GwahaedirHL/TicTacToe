using System.Collections.Generic;
using System.Linq;

public class ElementFactory
{
    GameSettings settings;
    CellView.Factory cellFactory;
    ZeroToken.Factory zeroFactory;
    CrossToken.Factory crossFactory;
    GameBoardStateManager stateManager;

    public ElementFactory(GameSettings settings, GameBoardStateManager state, CellView.Factory cellFactory, ZeroToken.Factory zeroFactory, CrossToken.Factory crossFactory)
    {
        this.settings = settings;
        this.stateManager = state;
        this.cellFactory = cellFactory;
        this.zeroFactory = zeroFactory;
        this.crossFactory = crossFactory;
    }

    public List<CellView> CreateBoardWithCells()
    {
        var cellsList = new List<CellView>();
        for (int x = 0; x < settings.SideSize; x++)
        {
            for (int y = 0; y < settings.SideSize; y++)
            {
                cellsList.Add(cellFactory.CreateCell(x, y, settings.PosOffset));
            }
        }

        var currentState = stateManager.GetCurrentState();
        for (int i = 0; i < settings.CellsCount; i++)
        {
            Cell cell = stateManager.GetCell(i);
            TokenType type = currentState[i];

            var cellView = cellsList.First(x => x.Cell == cell);

            if (cellView && type != TokenType.Empty)
            {
                CreateTokenFromType(type, cellView);
            }

        }

        return cellsList;
    }

    public CrossToken CreateCrossAt(CellView cell)
    {
        return crossFactory.CreateTokenOnCell(cell);
    }

    public ZeroToken CreateZeroAt(CellView cell)
    {
        return zeroFactory.CreateTokenOnCell(cell);
    }

    void CreateTokenFromType(TokenType type, CellView cell)
    {
        switch (type)
        {
            case TokenType.Empty:
                break;
            case TokenType.Cross:
                CreateCrossAt(cell);
                break;
            case TokenType.Zero:
                CreateZeroAt(cell);
                break;
            default:
                break;
        }
    }
}

