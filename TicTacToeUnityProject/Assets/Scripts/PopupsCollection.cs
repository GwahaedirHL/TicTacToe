using System;
using System.Linq;
using UnityEngine;

public enum PopupID
{
    WinPopup = 0,
    DrawPopup = 1
}

[Serializable]
public class PopupsCollection
{
    [SerializeField]
    public SerializedPopup[] popupsCollection;

    public Popup GetPopupByID(PopupID popupID)
    {
        return popupsCollection.First(x => x.ID == popupID).PopupPrefab;
    }
}
