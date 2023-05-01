using System.Linq;
using UnityEngine;

public class Footprint : MonoBehaviour
{
    [SerializeField] private float lifeTime = 1f;
    [SerializeField] private float checkTrackRadius = 0.15f;

    private void Start()
    {
        Color footprintColor = GetComponent<Renderer>().material.color;
        footprintColor.a = Player.instance.MudFilling;
        GetComponent<Renderer>().material.color = footprintColor;
        if (Physics.OverlapSphere(transform.position, checkTrackRadius).Where(x => x.GetComponent<Track>() != null).ToList().Count == 0)
        {
            Destroy(gameObject);
        }
        else
        {
            Player.instance.GetComponent<PlayerMovement>().PlayStompAudio();
        }
        Destroy(gameObject, lifeTime);
    }
}
