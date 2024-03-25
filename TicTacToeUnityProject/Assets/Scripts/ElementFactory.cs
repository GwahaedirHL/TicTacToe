using System.Collections.Generic;
using System.Linq;

public class ElementFactory
{
    GameSettings settings;
    CellView.Factory cellFactory;
    ZeroToken.Factory zeroFactory;
    CrossToken.Factory crossFactory;

    public ElementFactory(GameSettings settings, CellView.Factory cellFactory, ZeroToken.Factory zeroFactory, CrossToken.Factory crossFactory)
    {
        this.settings = settings;
        this.cellFactory = cellFactory;
        this.zeroFactory = zeroFactory;
        this.crossFactory = crossFactory;
    }

    public List<CellView> CreateBoardWithCells(GameBoardState state)
    {
        var cellsList = new List<CellView>();
        for (int x = 0; x < settings.GameBoardSideSize; x++)
        {
            for (int y = 0; y < settings.GameBoardSideSize; y++)
            {
                cellsList.Add(cellFactory.CreateCell(x, y, settings.PosOffset));
            }
        }

        for (int i = 0; i < state.boardState.Length; i++)
        {
            Cell cell = state.GetCell(i);
            TokenType type = (TokenType)state.boardState[i];

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

