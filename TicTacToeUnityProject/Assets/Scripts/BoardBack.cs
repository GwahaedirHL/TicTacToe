using UnityEngine;
using Zenject;

public class BoardBack : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer sprite;

    [Inject]
    GameSettings settings;

    private void Start()
    {
        transform.localPosition = settings.BoardCenter;
        sprite.size = new Vector2(settings.GameBoardSideSize, settings.GameBoardSideSize);        
    }
}

