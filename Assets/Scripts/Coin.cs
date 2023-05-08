using System.Collections.Generic;
using UnityEngine;
using static Utils;

[RequireComponent(typeof(SphereCollider))]
public class Coin : InteractiveObject
{
    [SerializeField] private int addedMoney;
    [SerializeField] private AudioSource coinAudioSource;
    [SerializeField] private List<AudioClip> coinClips;
    [SerializeField] private Animator coinAnimator;

    private void Awake()
    {
        coinAnimator = GetComponent<Animator>();
        coinAnimator.enabled = false;
    }

    private void OnBecameVisible()
    {
        coinAnimator.enabled = true;
    }

    public override void Action() 
    {
        coinAudioSource.clip = coinClips[Random.Range(0, coinClips.Count)];
        coinAudioSource.Play();
        coinAudioSource.transform.SetParent(null);
        Player.instance.AddedMoney += addedMoney;
        StartCoroutine(DisactiveMediately(gameObject, 1));
        gameObject.SetActive(false);
    }
}
