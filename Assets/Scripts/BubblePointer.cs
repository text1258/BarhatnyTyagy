using UnityEngine;

public class BubblePointer : InteractiveObject
{
    [SerializeField] private PointerType bubblePointerType;
    [SerializeField] private BubbleBonus bonus;
    [SerializeField] private GameObject BubblePointerGroup;

    private void OnValidate()
    {
        if (bonus != null)
        {
            Instantiate(bonus.gameObject, transform.position, transform.rotation, parent: transform);
        }
    }

    public override void Action(Player Initiator)
    {
        if (bubblePointerType == PointerType.left)
        {
            Initiator.transform.Rotate(0, -90, 0);
        }
        else if (bubblePointerType == PointerType.right)
        {
            Initiator.transform.Rotate(0, 90, 0);
        }
        if (bonus != null)
        {
            bonus.Action(Initiator);
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
