using UnityEngine;
using UnityEngine.UI;

public class MudIndicator : MonoBehaviour
{
    public static MudIndicator instance;

    [SerializeField] private Image indicator;

    public Image Indicator { get => indicator; set => indicator = value; }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Indicator.fillAmount = (float)Player.instance.MudCount / (float)Player.instance.MaxMud;
    }
}