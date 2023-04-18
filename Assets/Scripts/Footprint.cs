using UnityEngine;

public class Footprint : MonoBehaviour
{
    private void Start()
    {
        Color footprintColor = GetComponent<Renderer>().material.color;
        footprintColor.a = (float)Player.instance.MudCount / (float)Player.instance.MaxMud;
        GetComponent<Renderer>().material.color = footprintColor;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
