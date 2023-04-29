using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public static void Pause()
    {
        Time.timeScale = 0;
    }

    public static void Play()
    {
        Time.timeScale = 1f;
    }
}
