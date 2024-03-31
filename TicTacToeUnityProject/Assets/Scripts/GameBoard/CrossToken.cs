using UnityEngine;
using Zenject;

public class CrossToken : MonoBehaviour, IPlacableToken
{
    Cell index;

    public Cell Index { get => index; set => index = value; }

    public void Accept(IPlacableTokenVisitor visitor)
    {
        visitor.Visit(this);
    }

    public class Factory : PlaceholderFactory<CrossToken> 
    {
        public CrossToken CreateTokenOnCell(CellView cell)
        {
            var token = Create();
            token.transform.SetParent(cell.transform);
            token.transform.position = cell.transform.position;
            token.transform.localScale = cell.transform.localScale;
            token.index = cell.Cell;
            cell.HasToken = true;
            return token;
        }
    
    }
}

