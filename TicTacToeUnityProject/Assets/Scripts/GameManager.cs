using UnityEngine;
using Zenject;

public class GameManager : MonoBehaviour
{
    CellFactory cellFactory;
    GameSettings settings;
    GameState state;

    [Inject]
    public void Construct(CellFactory cellFactory, GameSettings settings, GameState state)
    {
        this.cellFactory = cellFactory;
        this.settings = settings;
        this.state = state;
    }

    private void Start()
    {
        GenerateCells();
    }

    void GenerateCells()
    {
        cellFactory.CreateCell();
        
    }
}

