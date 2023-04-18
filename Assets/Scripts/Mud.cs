using UnityEngine;

public class Mud : InteractiveObject
{
    [SerializeField] private int mudCount;
    [SerializeField] private Material footPrint;
    [SerializeField] private ParticleSystem mudBubblesEffect;

    public override void Action()
    {
        mudBubblesEffect.Play();
        if (Player.instance.MudCount + mudCount > Player.instance.MaxMud)
        {
            Player.instance.MudCount = Player.instance.MaxMud;
        }
        else
        {
            Player.instance.MudCount += mudCount;
        }
    }
}
