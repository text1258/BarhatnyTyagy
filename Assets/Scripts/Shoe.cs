using System.Linq;
using UnityEngine;

public class Shoe : MonoBehaviour
{
    [SerializeField] private Footprint footprint;
    [SerializeField] private ShoeType shoeType;
    [SerializeField] private float elevation = 0.001f;

    public ShoeType ShoeType { get => shoeType; set => shoeType = value; }

    public void Inherit()
    {
        GameObject instatntiatedFootprint = Instantiate(footprint.gameObject, new Vector3(transform.position.x, transform.position.y + elevation, transform.position.z), transform.rotation);
        instatntiatedFootprint.transform.localScale = transform.lossyScale;
        if (Physics.OverlapSphere(instatntiatedFootprint.transform.position, elevation * 4).Where(x => x.GetComponent<Track>() != null).ToList().Count == 0)
        {
            Destroy(instatntiatedFootprint);
        }
    }
}

public enum ShoeType
{
    Left,
    Right,
}