using UnityEngine;

public class Shoes : MonoBehaviour
{

    public void Inherit(ShoeType needShoeType)
    {
        foreach (Shoe child in GetComponentsInChildren<Shoe>())
        {
            if (child.ShoeType == needShoeType)
            {
                child.Inherit();
            }
        }
    }
}
