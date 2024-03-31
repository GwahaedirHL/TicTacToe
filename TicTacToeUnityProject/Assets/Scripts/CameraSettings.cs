using UnityEngine;
using Zenject;

public class CameraSettings : MonoBehaviour
{
    GameSettings settings;

    [Inject]
    public void Constract(GameSettings settings)
    {
        this.settings = settings;
    }

    private void Start()
    {
        Camera.main.transform.localPosition = new Vector3(settings.BoardCenter.x, settings.BoardCenter.x, -10f - settings.SideSize);
    }
}

