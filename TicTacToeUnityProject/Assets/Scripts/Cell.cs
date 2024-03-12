using UnityEngine;
using Zenject;

public class Cell : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer cellBack;

    public float side => cellBack.bounds.size.x;

    public void SetScale(float scale)
    {
        transform.localScale = new Vector2(scale, scale);
    }

    public void SetPosition(Vector2 pos)
    {
        transform.localPosition = pos;
    }

    public class Factory : PlaceholderFactory<Cell> { }
}

