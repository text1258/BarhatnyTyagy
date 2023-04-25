using TMPro;
using UnityEngine;

public class LevelText : MonoBehaviour
{
    [SerializeField] private TMP_Text text;

    private void Start()
    {
        text.text = $"Level: {(Player.instance.PlayerData.LevelIndex)}";
    }
}
