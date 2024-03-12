using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Installer : MonoInstaller
{
    public Cell cell;
    public BoardBack boardBackground;
    public GameManager gameBoard;

    public override void InstallBindings()
    {
        InstallGameBoard();
    }

    void InstallGameBoard()
    {
        Container.BindFactory<Cell, Cell.Factory>().FromComponentInNewPrefab(cell).UnderTransformGroup("Cells");
        Container.Bind<GameSettings>().AsSingle().NonLazy();
        Container.Bind<GameState>().AsSingle().NonLazy();
        Container.Bind<CellFactory>().AsSingle().WithArguments(boardBackground).NonLazy();
    }
}
