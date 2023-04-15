using TMPro;
using UnityEngine;

public class MoneyText : MonoBehaviour
{
    public static MoneyText instance;

    [SerializeField] private TMP_Text text;

    public TMP_Text Text { get => text; set => text = value; }

    private void Awake()
    {
        instance = this;
    }
}
