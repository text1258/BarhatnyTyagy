using UnityEngine;
using UnityEngine.UI;

public class PausePanel : MonoBehaviour
{
    [SerializeField] private Image panel;
    [SerializeField] private Button playPauseButton;
    [SerializeField] private Sprite pauseSprite;
    [SerializeField] private Sprite playSprite;
    [SerializeField] private Button homeButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private AudioVolumeSwitcherButton audioVolumeSwitcherButton;

    [ContextMenu("PauseGame")]
    public void Pause()
    {
        audioVolumeSwitcherButton.gameObject.SetActive(true);
        playPauseButton.image.sprite = playSprite;
        playPauseButton.onClick.RemoveAllListeners();
        playPauseButton.onClick.AddListener(Play);
        panel.enabled = true;
        homeButton.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        PauseGame.Pause();
    }

    [ContextMenu("PlayGame")]
    public void Play()
    {
        audioVolumeSwitcherButton.gameObject.SetActive(false);
        playPauseButton.image.sprite = pauseSprite;
        playPauseButton.onClick.RemoveAllListeners();
        playPauseButton.onClick.AddListener(Pause);
        panel.enabled = false;
        homeButton.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
        PauseGame.Play();
    }
}
