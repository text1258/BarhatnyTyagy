using UnityEngine;

public class Coin : InteractiveObject
{
    [SerializeField] private int addedMoney;
    public override void Action() 
    {
        Player.instance.AddedMoney += addedMoney;
        Destroy(gameObject);
    }
}
