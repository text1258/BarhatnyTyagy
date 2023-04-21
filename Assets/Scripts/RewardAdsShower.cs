using UnityEngine;
using UnityEngine.Events;
using YG;

public class RewardAdsShower : MonoBehaviour
{
    public static RewardAdsShower instance;
    private static UnityAction rewardAction;

    private void OnEnable()
    {
        YandexGame.RewardVideoEvent += GetReward;
        YandexGame.ErrorVideoEvent += GetReward;
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
    }

    public void ShowRevardAds(UnityAction reward)
    {
        rewardAction = reward;
        YandexGame.RewVideoShow(0);
    }

    public void GetReward(int id = 0) => rewardAction.Invoke();

    public void GetReward() => rewardAction.Invoke();
}