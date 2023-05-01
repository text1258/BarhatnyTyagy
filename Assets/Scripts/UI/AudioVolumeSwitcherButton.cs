using UnityEngine;
using UnityEngine.UI;
using YG;

public class AudioVolumeSwitcherButton : MonoBehaviour
{
    [SerializeField] private Sprite volumeOn;
    [SerializeField] private Sprite volumeOff;
    [SerializeField] private Image volumeImage;

    private void Start()
    {
        SetCorrectAudioVolumeMode();
    }

    public void SwitchAudioVolume()
    {
        Player.instance.PlayerData.isVolumeOn = !Player.instance.PlayerData.isVolumeOn;
        YandexGame.SaveProgress();
        SetCorrectAudioVolumeMode();
    }

    private void SetCorrectAudioVolumeMode()
    {
        if (Player.instance.PlayerData.isVolumeOn == true)
        {
            AudioVolumeSwitch.VolumeOn();
            volumeImage.sprite = volumeOn;
        }
        else
        {
            AudioVolumeSwitch.VolumeOff();
            volumeImage.sprite = volumeOff;
        }
    }
}