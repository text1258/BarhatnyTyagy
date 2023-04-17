using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public static Player instance;
    [SerializeField] private PlayerData playerData;
    [SerializeField] private UnityEvent onPlayerDataLoad;
    [SerializeField] private int addedMoney = 0;
    [SerializeField] private AllShoes allShoes;

    public PlayerData PlayerData
    {
        get
        {
            if (MoneyText.instance != null)
            {
                MoneyText.instance.Text.text = (playerData.Money + addedMoney).ToString();
            }
            PlayerData.Save(playerData);
            return playerData;
        }

        set
        {
            PlayerData.Save(value);
            playerData = value;
        }
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

    private void Awake()
    {
        if (instance != null & instance != this)
        {
            PlayerData = instance.PlayerData;
            Destroy(instance.gameObject);
        }
        else
        {
            PlayerData = PlayerData.GetPlayerData();
        }
        onPlayerDataLoad.Invoke();
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
}