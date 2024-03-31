using UnityEngine;
using System;

[Serializable]
public struct SerializedPopup
{
    [SerializeField]
    Popup popup;

    [SerializeField]
    PopupID popupID;

    public PopupID ID => popupID;

    public Popup PopupPrefab => popup;
}
