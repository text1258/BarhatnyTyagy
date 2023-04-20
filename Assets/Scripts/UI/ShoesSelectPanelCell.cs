using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShoesSelectPanelCell : MonoBehaviour
{
    [SerializeField] private int indexInAllShoes;
    [SerializeField] private Image picure;
    [SerializeField] private ShoeSelectPanelCellType type;
    [SerializeField] private Image adsMark;
    [SerializeField] private TMP_Text priceText;

    public int IndexInAllShoes { get => indexInAllShoes; set => indexInAllShoes = value; }
    public Image Mark { get => adsMark; set => adsMark = value; }

    private void Start()
    {
        SetType();
        picure.sprite = Player.instance.AllShoes.Shoes[IndexInAllShoes].Picture;
    }

    public void OnSelect()
    {
        if (type == ShoeSelectPanelCellType.opened)
        {
            SelectThisShoes();
        }
        else if (type == ShoeSelectPanelCellType.closedForMoney & Player.instance.AllShoes.Shoes[IndexInAllShoes].Price <= Player.instance.PlayerData.Money)
        {
            Player.instance.PlayerData.Money -= Player.instance.AllShoes.Shoes[IndexInAllShoes].Price;
            OpenThisShoes();
        }
        else if (type == ShoeSelectPanelCellType.closedForAds)
        {
            AdsShower.instance.ShowRevardAds(OpenThisShoes);
        }
    }

    private void SelectThisShoes()
    {
        ShoesDemonstration.instance.DemonstrationObject = Player.instance.AllShoes.Shoes[IndexInAllShoes].Model.transform;
        Player.instance.PlayerData.SelectedShoesIndex = IndexInAllShoes;
    }

    private void OpenThisShoes()
    {
        Player.instance.PlayerData.OpenedShoesIndexes.Add(IndexInAllShoes);
        SelectThisShoes();
        SetType();
    }

    private void SetType()
    {
        if (Player.instance.PlayerData.OpenedShoesIndexes.Contains(IndexInAllShoes))
        {
            type = ShoeSelectPanelCellType.opened;
            GetComponent<Image>().color = ShoesSelectPanel.instance.OpenedShoesBackgroundColor;
            priceText.text = "";
            adsMark.gameObject.SetActive(false);
        }
        else
        {
            if (Player.instance.AllShoes.Shoes[IndexInAllShoes].OpenShoesType == OpenShoesType.forMoney)
            {
                type = ShoeSelectPanelCellType.closedForMoney;
                GetComponent<Image>().color = ShoesSelectPanel.instance.CloseForMoneyShoesBackgroundColor;
                priceText.text = Player.instance.AllShoes.Shoes[IndexInAllShoes].Price.ToString();
                adsMark.gameObject.SetActive(false);
            }
            else if(Player.instance.AllShoes.Shoes[IndexInAllShoes].OpenShoesType == OpenShoesType.forAds)
            {
                type = ShoeSelectPanelCellType.closedForAds;
                GetComponent<Image>().color = ShoesSelectPanel.instance.ClosForAdsShoesBackgroundColor;
                priceText.text = "";
                adsMark.gameObject.SetActive(true);
            }
        }
    }
}

public enum ShoeSelectPanelCellType
{
    opened,
    closedForMoney,
    closedForAds,
}