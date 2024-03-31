public interface IPlacableTokenVisitor
{
    public void Visit(CrossToken token);
    public void Visit(ZeroToken token);
}

