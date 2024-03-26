using Zenject;

public class Installer : MonoInstaller
{
    public CellView cellView;
    public ZeroToken zeroToken;
    public CrossToken crossToken;
    public BoardBack boardBackground;

    public override void InstallBindings()
    {
        
        Container.Bind<GameSettings>().AsSingle().NonLazy();
        Container.Bind<GameBoardStateManager>().AsSingle().NonLazy();

        Container.Bind<ElementFactory>().AsSingle().NonLazy();
        Container.BindFactory<CellView, CellView.Factory>().FromComponentInNewPrefab(cellView).UnderTransform(boardBackground.transform);
        Container.BindFactory<ZeroToken, ZeroToken.Factory>().FromComponentInNewPrefab(zeroToken);
        Container.BindFactory<CrossToken, CrossToken.Factory>().FromComponentInNewPrefab(crossToken); 
        Container.BindInterfacesTo<ZeroToken>().AsTransient();
        Container.BindInterfacesTo<CrossToken>().AsTransient();
    }
}
