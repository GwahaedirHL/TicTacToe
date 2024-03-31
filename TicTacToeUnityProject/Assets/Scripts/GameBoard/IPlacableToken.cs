public interface IPlacableToken
{
    public void Accept(IPlacableTokenVisitor visitor);
    public Cell Index { get; set; }
}

