using UnityEngine;
using UnityEngine.Events;

class AdsShower : MonoBehaviour
{
    public static void ShowRevardAds(UnityAction reward)
    {
        reward.Invoke();
    }
}