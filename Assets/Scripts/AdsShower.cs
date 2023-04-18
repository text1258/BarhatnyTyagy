using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class AdsShower : MonoBehaviour
{
    public static AdsShower instance;
    private static UnityAction rewardAction;

    [SerializeField] private int adsCoolDown = 60;
    [SerializeField] private int pastadsCoolDown = 0;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            instance.ShowFullscreenAds();
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
            StartCoroutine(Timer());
        }
    }

    private IEnumerator Timer()
    {
        while (pastadsCoolDown < adsCoolDown)
        {
            yield return new WaitForSeconds(1);
            pastadsCoolDown += 1;
        }
        yield break;
    }

    public static void ShowRevardAds(UnityAction reward)
    {
#if UNITY_EDITOR || UNITY_ANDROID
        Debug.Log("ShowRewardAds");
        rewardAction = reward;
#endif
    }

    public void ShowFullscreenAds()
    {
        if (pastadsCoolDown == adsCoolDown)
        {
#if UNITY_EDITOR || UNITY_ANDROID
            Debug.Log("ShowShowFullscreenAds");
#endif
            pastadsCoolDown = 0;
            StartCoroutine(Timer());
        }
    }

    public void GetReward()
    {
        rewardAction.Invoke();
    }
}