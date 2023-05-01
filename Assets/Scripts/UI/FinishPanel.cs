using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using YG;
using System.Collections;

public class FinishPanel : MonoBehaviour
{
    [SerializeField] int earnedMoneyScaleAdsFactor;
    [SerializeField] int earnedMoneyScaleMudMaxFactor;
    [SerializeField] private float mudScaleFilingTime;
    [SerializeField] private Image finishPanel;
    [SerializeField] TMP_Text earningMoney;
    [SerializeField] Button finishLevelButton;
    [SerializeField] private Image mudScale;
    [SerializeField] private GameObject mudPanel;
    [SerializeField] Button scaleEarningMoneyButtonAds;
    [SerializeField] TMP_Text scaleAdsFactor;
    [SerializeField] TMP_Text maxScaleMudFactor;
    [SerializeField] TMP_Text minScaleMudFactor;
    [SerializeField] TMP_Text currentScaleMudFactor;
    public static FinishPanel instance;


    private void Awake()
    {
        instance = this;
    }

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

    public IEnumerator MudCalculate(UnityEvent onCalculate = null)
    {
        scaleAdsFactor.text = $"×{earnedMoneyScaleAdsFactor}";
        maxScaleMudFactor.text = $"×{earnedMoneyScaleMudMaxFactor}";
        earningMoney.text = $"+{Player.instance.AddedMoney}";
        MudIndicator.instance.EnableCountingMode();
        float pastFilling = 0f;
        mudPanel.SetActive(true);
        mudScale.fillAmount = 0f;
        while (pastFilling < Player.instance.MudFilling)
        {
            pastFilling += Time.deltaTime / mudScaleFilingTime;
            mudScale.fillAmount = pastFilling;
            MudIndicator.instance.Indicator.fillAmount = 1 - pastFilling - (1 - Player.instance.MudFilling);
            currentScaleMudFactor.text = ((int)(earnedMoneyScaleMudMaxFactor * pastFilling)).ToString();
            currentScaleMudFactor.rectTransform.position = new Vector2(currentScaleMudFactor.rectTransform.position.x, minScaleMudFactor.rectTransform.position.y + pastFilling * (maxScaleMudFactor.rectTransform.position.y - minScaleMudFactor.rectTransform.position.y));
            yield return null;
        }
        onCalculate?.Invoke();
        finishPanel.enabled = true;
        Player.instance.GetComponent<PlayerMovement>().ShoesSpawnPoint.SetActive(false);
        Player.instance.AddedMoney *= (int)(earnedMoneyScaleMudMaxFactor * Player.instance.MudFilling);
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
        YandexGame.SaveProgress();
        SceneLoader.instance.LoadMainMenu();

    }
}