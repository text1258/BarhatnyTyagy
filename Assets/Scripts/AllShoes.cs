using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AllShoes", menuName = "Data/AllShoes")]
public class AllShoes : ScriptableObject
{
    [SerializeField] private List<ShoesInfo> shoes;

    public List<ShoesInfo> Shoes { get => shoes; set => shoes = value; }
}
