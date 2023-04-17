using UnityEngine;

public class BubblePointer : InteractiveObject
{
    [SerializeField] private PointerType bubblePointerType;
    [SerializeField] private GameObject BubblePointerGroup;

    public override void Action()
    {
        if (bubblePointerType == PointerType.left)
        {
            Player.instance.transform.Rotate(0, -90, 0);
        }
        else if (bubblePointerType == PointerType.right)
        {
            Player.instance.transform.Rotate(0, 90, 0);
        }
        Destroy(BubblePointerGroup);
    }
}

public enum PointerType
{
    forward,
    right,
    left,
}
