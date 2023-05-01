using TMPro;
using UnityEngine;
using YG;

public class AddMoneyForAdversiting : MonoBehaviour
{
    [SerializeField] private int addedMoney;
    [SerializeField] private TMP_Text addedMoneyText;

    private void OnValidate()
    {
        if (addedMoneyText != null)
        {
            addedMoneyText.text = $"+{addedMoney}";
        }
        if(addedMoney < 0)
        {
            addedMoney = 0;
        }
    }

    public void AddedMoneyForAds()
    {
        RewardAdsShower.instance.ShowRevardAds(AddMoney);
    }

    private void AddMoney()
    {
        Player.instance.PlayerData.Money += addedMoney;
        YandexGame.SaveProgress();
    }
}
