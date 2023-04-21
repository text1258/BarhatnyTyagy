using System.Linq;
using UnityEngine;

public class Footprint : MonoBehaviour
{
    [SerializeField] private float lifeTime;
    [SerializeField] private float checkTrackRadius;

    private void Start()
    {
        Color footprintColor = GetComponent<Renderer>().material.color;
        footprintColor.a = (float)Player.instance.MudCount / (float)Player.instance.MaxMud;
        GetComponent<Renderer>().material.color = footprintColor;
        if (Physics.OverlapSphere(transform.position, checkTrackRadius).Where(x => x.GetComponent<Track>() != null).ToList().Count == 0)
        {
            Destroy(gameObject);
        }
        Destroy(gameObject, lifeTime);
    }
}
