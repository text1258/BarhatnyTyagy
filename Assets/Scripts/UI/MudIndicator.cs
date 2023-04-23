using UnityEngine;
using UnityEngine.UI;

public class MudIndicator : MonoBehaviour
{
    public static MudIndicator instance;

    [SerializeField] private Image indicator;
    [SerializeField] private Vector2 cointingModePosition;
    [SerializeField] private Vector2 cointingModeScale;

    public Image Indicator { get => indicator; set => indicator = value; }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Indicator.fillAmount = Player.instance.MudFilling;
    }
    public void EnableCountingMode()
    {
        GetComponent<RectTransform>().anchoredPosition = cointingModePosition;
        GetComponent<RectTransform>().localScale = cointingModeScale;
    }
}