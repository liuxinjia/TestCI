using UnityEngine.UIElements;

internal abstract class NodeView
{
    protected abstract INode nodeTarget{get;}
    protected VisualElement parentElement;

}