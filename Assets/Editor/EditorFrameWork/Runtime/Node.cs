using System.Collections.Generic;

internal interface INode
{
    void SetContext(Context context);
    void Draw();
    void AddChildNode(INode node);
}

internal abstract class BaseNode : INode
{
    protected Context context;
    protected List<INode> nodes;
    protected INode parentNode;
    public int depth { get; protected set; }


    public virtual void AddChildNode(INode node)
    {
        if (!nodes.Contains(node))
        {
            nodes.Add(node);
            if (node is BaseNode childNode)
            {
                childNode.depth = this.depth + 1;
                childNode.parentNode = this;
            }
        }
    }

    public virtual void Draw()
    {
        foreach (var node in nodes) node.Draw();
    }

    public virtual void SetContext(Context context)
    {
        foreach (var node in nodes) node.SetContext(context);
    }

    public void SetParent(INode node)
    {
        this.parentNode = node;
    }

}

internal class RootNode : BaseNode
{
    public RootNode()
    {
        this.depth = 0;
    }
}

internal abstract class Node : BaseNode
{
    public override void Draw()
    {
        DrawElements();
        base.Draw();
    }
    public abstract void DrawElements();
}