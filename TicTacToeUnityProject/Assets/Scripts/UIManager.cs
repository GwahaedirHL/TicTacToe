using Zenject;

public class UIManager 
{
    PopupsCollection popupsCollection;
    PopupRoot popupRoot;
    [Inject]
    DiContainer container;

    public UIManager(PopupsCollection popupsCollection, PopupRoot popupRoot)
    {
        this.popupsCollection = popupsCollection;
        this.popupRoot = popupRoot;
    }

    public void OpenWinPopup(TokenType token)
    {
        var prefab = popupsCollection.GetPopupByID(PopupID.WinPopup);
        var popup = container.InstantiatePrefabForComponent<WinPopup>(prefab, popupRoot.transform);
        popup.Draw(token);
    }

    public void OpenDrawPopup()
    {
        var prefab = popupsCollection.GetPopupByID(PopupID.DrawPopup);
        container.InstantiatePrefabForComponent<DrawPopup>(prefab, popupRoot.transform);
    }
}
