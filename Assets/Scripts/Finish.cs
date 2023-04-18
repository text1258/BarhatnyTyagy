using System.Collections;
using TMPro;
using UnityEngine;
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
        AdsShower.instance.ShowFullscreenAds();
        GetComponent<Collider>().enabled = false;
        Player.instance.GetComponent<PlayerMovement>().ShoesSpawnPoint.transform.GetChild(0).gameObject.GetComponent<Animator>().enabled = false;
        Player.instance.gameObject.isStatic = true;
        Player.instance.GetComponent<PlayerMovement>().SideSpeed = 0;
        Player.instance.GetComponent<PlayerMovement>().ForwardSpeed = 0;
        Player.instance.GetComponent<Rigidbody>().isKinematic = true;
        scaleAdsFactor.text = $"×{earnedMoneyScaleAdsFactor}";
        maxScaleMudFactor.text = $"×{earnedMoneyScaleMudMaxFactor}";
        Player.instance.GetComponent<Animator>().SetTrigger("CalculateMud");
        earningMoney.text = $"+{Player.instance.AddedMoney}";
        StartCoroutine(MudScaling());
    }

    private IEnumerator MudScaling()
    {
        float pastFilling = 0f;
        mudPanel.SetActive(true);
        mudScale.fillAmount = 0f;
        finishPanel.enabled = false;
        while (pastFilling < (float)Player.instance.MudCount / (float)Player.instance.MaxMud)
        {
            pastFilling += Time.deltaTime / mudScaleFilingTime;
            mudScale.fillAmount = pastFilling;
            yield return null;
        }
        Player.instance.AddedMoney *= (int)(earnedMoneyScaleMudMaxFactor * ((float)Player.instance.MudCount / (float)Player.instance.MaxMud));
        earningMoney.text = $"+{Player.instance.AddedMoney}";
        Player.instance.GetComponent<Animator>().SetTrigger("FinishPose");
        finishLevelButton.gameObject.SetActive(true);
        scaleAdsFactor.gameObject.SetActive(true);
        earningMoney.gameObject.SetActive(true);
        scaleEarningMoneyButtonAds.gameObject.SetActive(true);
        finishPanel.enabled = true;
        yield break;
    }

    public void ScaleEarnedMoney()
    {
        AdsShower.ShowRevardAds(ScaleAddedMoney);
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
        if (Player.instance.PlayerData.Level + 1 < SceneManager.sceneCountInBuildSettings)
        {
            Player.instance.PlayerData.Level += 1;
        }
        sceneLoader.LoadMainMenu();

    }
}
