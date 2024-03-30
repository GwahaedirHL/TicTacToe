using UnityEngine;
using Zenject;

public class Installer : MonoInstaller
{
    public CellView cellView;
    public ZeroToken zeroToken;
    public CrossToken crossToken;
    public BoardBack boardBackground;

    [Header("UI and popups")]
    public PopupRoot uiCanvas; 

    public override void InstallBindings()
    {
        BindInitialization();
        BindFabrics();
        BindUI();
    }

    void BindInitialization()
    {
        Container.BindInterfacesAndSelfTo<GameSettings>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<GameBoardStateManager>().AsSingle().NonLazy();
    }

    void BindFabrics()
    {
        Container.Bind<ElementFactory>().AsSingle().NonLazy();
        Container.BindFactory<CellView, CellView.Factory>().FromComponentInNewPrefab(cellView).UnderTransform(boardBackground.transform);
        Container.BindFactory<ZeroToken, ZeroToken.Factory>().FromComponentInNewPrefab(zeroToken);
        Container.BindFactory<CrossToken, CrossToken.Factory>().FromComponentInNewPrefab(crossToken);
        Container.BindInterfacesAndSelfTo<ZeroToken>().AsTransient();
        Container.BindInterfacesAndSelfTo<CrossToken>().AsTransient();
    }

    void BindUI()
    {
        Container.Bind<UIManager>().AsSingle().NonLazy();
        Container.Bind<PopupRoot>().FromComponentInHierarchy().AsSingle();
    }
}
