using UnityEngine;

public class LevelRestarter : InteractiveObject
{
    [SerializeField] private LevelResrarterPanel levelResrarterPanel;

    public override void Action()
    {
        Player.instance.transform.GetChild(0).gameObject.SetActive(false);
        Player.instance.GetComponent<PlayerMovement>().StopMovement();
        levelResrarterPanel.gameObject.SetActive(true);
    }
}