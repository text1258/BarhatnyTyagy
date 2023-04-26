using UnityEngine;
using UnityEngine.Events;
using YG;

public class Player : MonoBehaviour
{
    public static Player instance;
    [SerializeField] private SavesYG playerData;
    [SerializeField] private UnityEvent onPlayerDataLoad;
    [SerializeField] private int addedMoney = 0;
    [SerializeField] private int maxMud;
    [SerializeField] private int mudCount;
    [SerializeField] private AllShoes allShoes;

    public SavesYG PlayerData
    {
        get
        {
            if (MoneyText.instance != null)
            {
                MoneyText.instance.Text.text = (playerData.Money + addedMoney).ToString();
            }
            return playerData;
        }
        set => playerData = value;
    }

    public AllShoes AllShoes { get => allShoes; set => allShoes = value; }

    public int AddedMoney
    {
        get => addedMoney;
        set
        {
            addedMoney = value;
            MoneyText.instance.Text.text = (playerData.Money + AddedMoney).ToString();
        }
    }

    public int MaxMud => maxMud;

    public int MudCount
    {
        get => mudCount; 
        set
        {
            mudCount = value;
            MudIndicator.instance.Indicator.fillAmount = MudFilling;
        }
    }

    public float MudFilling => (float)mudCount / maxMud;

    private void OnEnable() => YandexGame.GetDataEvent += GetLoad;

    private void OnDisable() => YandexGame.GetDataEvent -= GetLoad;

    private void Awake()
    {
        if (instance != null & instance != this)
        {
            PlayerData = instance.PlayerData;
            Destroy(instance.gameObject);
            onPlayerDataLoad.Invoke();
        }
        else
        {
            if (YandexGame.SDKEnabled == true)
            {
                GetLoad();
            }
        }
        DontDestroyOnLoad(gameObject);
        instance = this;
    }

    private void OnTriggerEnter(Collider other)
    {
        InteractiveObject collidedInteractiveObject = other.GetComponent<InteractiveObject>();
        if (collidedInteractiveObject != null)
        {
            collidedInteractiveObject.Action();
        }
    }
    private void GetLoad()
    {
        playerData = YandexGame.savesData;
        onPlayerDataLoad.Invoke();
    }
}