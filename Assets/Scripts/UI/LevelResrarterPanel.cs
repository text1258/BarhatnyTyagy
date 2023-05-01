using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class LevelResrarterPanel : MonoBehaviour
{
    [SerializeField] private Button addEarnedMoney;
    [SerializeField] private TMP_Text earnedMoney;

    private void Awake()
    {
        if (Player.instance.AddedMoney == 0)
        {
            addEarnedMoney.gameObject.SetActive(false);
        }
        else
        {
            earnedMoney.text = $"+{Player.instance.AddedMoney}";
        }
    }

    public void GetEarnedMoneyForAds()
    {
        RewardAdsShower.instance.ShowRevardAds(GiveEarnedMoneyToPlayer);
    }

    private void GiveEarnedMoneyToPlayer()
    {
        Player.instance.PlayerData.Money += Player.instance.AddedMoney;
        YandexGame.SaveProgress();
        addEarnedMoney.gameObject.SetActive(false);
    }
}