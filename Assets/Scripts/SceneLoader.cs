using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private GameObject asyncLoadPanel;
    [SerializeField] private Image loadProgressBar;

    public static IEnumerator AsyncLoadScene(int sceneIndex, Image progressBar = null)
    {
        AsyncOperation loadScene = SceneManager.LoadSceneAsync(sceneIndex);
        while (loadScene.isDone == false)
        {
            if (progressBar != null)
            {
                progressBar.fillAmount = loadScene.progress;
            }
            yield return null;
        }
        yield break;
    }

    public void LoadMainMenu()
    {
        asyncLoadPanel.SetActive(true);
        StartCoroutine(AsyncLoadScene(0, loadProgressBar));
    }

    public void LoadPlayerLevel()
    {
        asyncLoadPanel.SetActive(true);
        StartCoroutine(AsyncLoadScene(Player.instance.PlayerData.Level, loadProgressBar));
    }
}
