using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private Image asyncLoadProgressBar;

    public IEnumerator AsyncLoadScene(int sceneIndex, Image progressBar = null)
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

    public void LoadPlayerLevel()
    {
       StartCoroutine(AsyncLoadScene(Player.instance.PlayerData.Level, asyncLoadProgressBar));
    }

    public static void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
