using UnityEngine;

public class AudioVolumeSwitch
{
    public static void VolumeOn()
    {
        AudioListener.volume = 1f;
    }

    public static void VolumeOff()
    {
        AudioListener.volume = 0f;
    }
}