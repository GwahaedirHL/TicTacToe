using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class CellView : MonoBehaviour
{
    public bool HasToken { get; set; }

    public Cell Cell;

    public event UnityAction<CellView> CellClicked;
    
    public void OnMouseDown()
    {
        CellClicked?.Invoke(this);
    }

    public void DisableInput()
    {
        CellClicked = null;
    }

    void SetPosition(Vector2 pos)
    {
        transform.localPosition = pos;
    }
    public class Factory : PlaceholderFactory<CellView>
    {
        public CellView CreateCell(int x, int y, int posOffset)
        {
            var cell = Create();
            cell.Cell = new Cell(x, y);
            cell.SetPosition(new Vector2(x - posOffset, y - posOffset));
            return cell;
        }
    }
}

