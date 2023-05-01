using UnityEngine;
using UnityEngine.Events;

public class Finish : InteractiveObject
{
    [SerializeField] private UnityEvent onCalculate;

    public override void Action()
    {
        GetComponent<Collider>().enabled = false;
        Player.instance.transform.position = transform.position;
        Player.instance.GetComponent<PlayerMovement>().StopMovement();
        Player.instance.GetComponent<Rigidbody>().isKinematic = true;
        StartCoroutine(FinishPanel.instance.MudCalculate(onCalculate));
    }
}
