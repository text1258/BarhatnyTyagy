using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Finish : InteractiveObject
{
    [SerializeField] private Image finishPanel;
    [SerializeField] int earnedMoneyScaleAdsFactor;
    [SerializeField] int earnedMoneyScaleMudMaxFactor;
    [SerializeField] TMP_Text earningMoney;
    [SerializeField] Button finishLevelButton;
    [SerializeField] private GameObject mudPanel;
    [SerializeField] private Image mudScale;
    [SerializeField] private float mudScaleFilingTime;
    [SerializeField] Button scaleEarningMoneyButtonAds;
    [SerializeField] TMP_Text scaleAdsFactor;
    [SerializeField] TMP_Text maxScaleMudFactor;
    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private UnityEvent onCalculate;

    private void OnValidate()
    {
        if (earnedMoneyScaleAdsFactor < 1)
        {
            earnedMoneyScaleAdsFactor = 1;
        }
        if (earnedMoneyScaleMudMaxFactor < 1)
        {
            earnedMoneyScaleMudMaxFactor = 1;
        }
    }

    public override void Action()
    {
        GetComponent<Collider>().enabled = false;
        Player.instance.gameObject.isStatic = true;
        Player.instance.GetComponent<PlayerMovement>().SideSpeed = 0;
        Player.instance.GetComponent<PlayerMovement>().ForwardSpeed = 0;
        Player.instance.GetComponent<Rigidbody>().isKinematic = true;
        scaleAdsFactor.text = $"×{earnedMoneyScaleAdsFactor}";
        maxScaleMudFactor.text = $"×{earnedMoneyScaleMudMaxFactor}";
        earningMoney.text = $"+{Player.instance.AddedMoney}";
        StartCoroutine(MudCalculate());
    }

    private IEnumerator MudCalculate()
    {
        Player.instance.GetComponent<PlayerMovement>().ShoesSpawnPoint.SetActive(false);
        onCalculate.Invoke();
        MudIndicator.instance.EnableCountingMode();
        float pastFilling = 0f;
        mudPanel.SetActive(true);
        mudScale.fillAmount = 0f;
        finishPanel.enabled = true;
        while (pastFilling < (float)Player.instance.MudCount / (float)Player.instance.MaxMud)
        {
            pastFilling += Time.deltaTime / mudScaleFilingTime;
            mudScale.fillAmount = pastFilling;
            MudIndicator.instance.Indicator.fillAmount = 1 - pastFilling - (1 - (float)Player.instance.MudCount / (float)Player.instance.MaxMud);
            yield return null;
        }
        Player.instance.AddedMoney *= (int)(earnedMoneyScaleMudMaxFactor * ((float)Player.instance.MudCount / (float)Player.instance.MaxMud));
        earningMoney.text = $"+{Player.instance.AddedMoney}";
        finishLevelButton.gameObject.SetActive(true);
        scaleAdsFactor.gameObject.SetActive(true);
        earningMoney.gameObject.SetActive(true);
        if (Player.instance.AddedMoney > 0)
        {
            scaleEarningMoneyButtonAds.gameObject.SetActive(true);
        }
        yield break;
    }

    public void ScaleEarnedMoney()
    {
        RewardAdsShower.instance.ShowRevardAds(ScaleAddedMoney);
    }

    private void ScaleAddedMoney()
    {
        Player.instance.AddedMoney *= earnedMoneyScaleAdsFactor;
        earningMoney.text = $"+{Player.instance.AddedMoney}";
        scaleEarningMoneyButtonAds.gameObject.SetActive(false);
        scaleAdsFactor.gameObject.SetActive(false);
    }

    public void FinishLevel()
    {
        Player.instance.PlayerData.Money += Player.instance.AddedMoney;
        Player.instance.AddedMoney = 0;
        if (Player.instance.PlayerData.LevelIndex + 1 < SceneManager.sceneCountInBuildSettings)
        {
            Player.instance.PlayerData.LevelIndex += 1;
        }
        sceneLoader.LoadMainMenu();

    }
}
