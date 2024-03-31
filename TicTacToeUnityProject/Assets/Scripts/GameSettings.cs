using UnityEngine;
using Zenject;

public class GameSettings: IInitializable
{
    public Vector2 BoardCenter { get; private set; }
    public float PosOffset => sideSize / 2f - 0.5f;
    public int SideSize => sideSize;
    public int CellsCount => sideSize * sideSize;

    int sideSize = 6;

    public void Initialize()
    {
        BoardCenter = new Vector2(sideSize / 2f - 0.5f, sideSize / 2f - 0.5f);
    }
}

