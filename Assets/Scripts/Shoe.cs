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
    }
}

public enum ShoeType
{
    Left,
    Right,
}