using System.Linq;
using UnityEngine;

public class Footprint : MonoBehaviour
{
    [SerializeField] private float checkTrackRadius = 0.15f;

    private bool wasItVisible = false;

    private void Start()
    {
        Color footprintColor = GetComponent<Renderer>().material.color;
        footprintColor.a = Player.instance.MudFilling;
        GetComponent<Renderer>().material.color = footprintColor;
        if (Physics.OverlapSphere(transform.position, checkTrackRadius).Where(x => x.GetComponent<Track>() != null).ToList().Count == 0)
        {
            gameObject.SetActive(false);
        }
        else
        {
            Player.instance.GetComponent<PlayerMovement>().PlayStompAudio();
        }
    }

    private void OnBecameVisible()
    {
        wasItVisible = true;
    }

    private void OnBecameInvisible()
    {
        if (wasItVisible == true)
        {
            gameObject.SetActive(false);
        }
    }
}
