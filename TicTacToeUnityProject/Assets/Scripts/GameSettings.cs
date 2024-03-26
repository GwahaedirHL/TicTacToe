using UnityEngine;
using Zenject;

public class GameSettings: IInitializable
{
    public Vector2 BoardCenter { get; private set; }
    public int SideSize => sideSize;
    public int PosOffset => (int)Mathf.Floor(sideSize / 2);
    public int CellsCount => sideSize * sideSize;

    int sideSize = 3;

    public void Initialize()
    {
        BoardCenter = new Vector2(sideSize / 2f - 0.5f, sideSize / 2f - 0.5f);
        Camera.main.transform.localPosition = new Vector3(BoardCenter.x, BoardCenter.x, -10f);
    }
}

