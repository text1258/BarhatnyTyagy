using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class LevelRestarter : InteractiveObject
{
    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private GameObject restartScreen;
    [SerializeField] private Button addEarnedMoney;
    [SerializeField] private TMP_Text earnedMoney;

    public override void Action()
    {
        Player.instance.GetComponent<PlayerMovement>().enabled = false;
        Player.instance.transform.GetChild(0).gameObject.SetActive(false);
        Player.instance.GetComponent<Rigidbody>().isKinematic = true;
        restartScreen.gameObject.SetActive(true);
        if (Player.instance.AddedMoney == 0)
        {
            addEarnedMoney.gameObject.SetActive(false);
        }
        else
        {
            earnedMoney.text = $"+{Player.instance.AddedMoney}";
        }
    }

    public void RestartLevel()
    {
        sceneLoader.LoadPlayerLevel();
    }

    public void GetEarnedMoney()
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