using NaughtyAttributes;
using UnityEngine;

[System.Serializable]
public class ShoesInfo
{
    [SerializeField] private int indexInSelectPanel;
    [SerializeField] private string shoesName;
    [SerializeField] private GameObject prefab;
    [SerializeField] private GameObject model;
    [SerializeField] private Sprite picture;
    [SerializeField] private OpenShoesType openShoesType;
    [ShowIf("openShoesType", OpenShoesType.forMoney), AllowNesting, SerializeField] int price;

    public int IndexInSelectPanel { get => indexInSelectPanel; set => indexInSelectPanel = value; }
    public string ShoesName { get => shoesName; set => shoesName = value; }
    public GameObject Prefab { get => prefab; set => prefab = value; }
    public GameObject Model { get => model; set => model = value; }
    public Sprite Picture { get => picture; set => picture = value; }
    public OpenShoesType OpenShoesType { get => openShoesType; set => openShoesType = value; }
    public int Price { get => price; set => price = value; }
}

public enum OpenShoesType
{
    forMoney,
    forAds,
}
