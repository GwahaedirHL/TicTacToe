using UnityEngine;
using Zenject;

public class CellFactory
{
    GameSettings settings;
    BoardBack boardBack;
    
    [Inject]
    Cell.Factory factory;

    [Inject]
    public void Construct(BoardBack boardBack, GameSettings settings)
    {
        this.boardBack = boardBack;
        this.settings = settings;
    }


    public void CreateCell()
    {
        for (int x = 0; x < settings.GameBoardSideSize; x++)
        {
            for (int y = 0; y < settings.GameBoardSideSize; y++)
            {
                Cell newCell = factory.Create();
                newCell.SetPosition(new Vector2(x, y));
            }
        }

        float centerCoordinate = settings.GameBoardSideSize / 2f - 0.5f;
        var center = new Vector2(centerCoordinate, centerCoordinate);
        boardBack.gameObject.transform.localPosition = center;
        boardBack.Sprite.size = new Vector2(settings.GameBoardSideSize, settings.GameBoardSideSize);

        Camera.main.transform.localPosition = new Vector3(center.x, center.x, -10f);
    }
}

