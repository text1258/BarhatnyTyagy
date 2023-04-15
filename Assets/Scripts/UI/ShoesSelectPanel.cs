using UnityEngine;
using UnityEngine.UI;

public class ShoesSelectPanel : MonoBehaviour
{
    public static ShoesSelectPanel instance;
    [SerializeField] private ScrollRect scrollZone;
    [SerializeField] private Color openedShoesBackgroundColor;
    [SerializeField] private Color closeForMoneyShoesBackgroundColor;
    [SerializeField] private Color closForAdsShoesBackgroundColor;
    [SerializeField] private ShoesSelectPanelCell cellPrefab;

    public Color OpenedShoesBackgroundColor { get => openedShoesBackgroundColor; set => openedShoesBackgroundColor = value; }
    public Color CloseForMoneyShoesBackgroundColor { get => closeForMoneyShoesBackgroundColor; set => closeForMoneyShoesBackgroundColor = value; }
    public Color ClosForAdsShoesBackgroundColor { get => closForAdsShoesBackgroundColor; set => closForAdsShoesBackgroundColor = value; }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        CreateSelectShoesCells();
    }

    public void CreateSelectShoesCells()
    {
        for (int i = 0; i < Player.instance.AllShoes.Shoes.Count; i++)
        {
             Instantiate(cellPrefab, parent: scrollZone.content).GetComponent<ShoesSelectPanelCell>().IndexInAllShoes = i;
        }
    }
}