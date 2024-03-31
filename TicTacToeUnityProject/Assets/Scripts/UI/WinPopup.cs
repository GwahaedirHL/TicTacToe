using UnityEngine;

public class WinPopup : Popup
{
    [SerializeField]
    GameObject crossView;

    [SerializeField]
    GameObject zeroView;

    public void Draw(TokenType token)
    {
        crossView.SetActive(false);
        zeroView.SetActive(false);

        if (token == TokenType.Cross)
            crossView.SetActive(true);

        if (token == TokenType.Zero)
            zeroView.SetActive(true);
    }
}
