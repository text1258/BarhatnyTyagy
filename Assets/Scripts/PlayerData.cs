using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    [SerializeField] private int levelIndex = 1;
    [SerializeField] private float money = 1000;
    [SerializeField] private List<int> openedShoesIndexes = new List<int> { 0 };
    [SerializeField] private int selectedShoesIndex = 0;

    public int Level { get => levelIndex; set => levelIndex = value; }
    public float Money { get => money; set => money = value; }
    public List<int> OpenedShoesIndexes { get => openedShoesIndexes; set => openedShoesIndexes = value; }
    public int SelectedShoesIndex { get => selectedShoesIndex; set => selectedShoesIndex = value; }

    public static void Save(PlayerData savingData)
    {
#if UNITY_EDITOR
        File.WriteAllText("Assets/SavingData.json", JsonUtility.ToJson(savingData));
#elif UNITY_ANDROID
        PlayerPrefs.SetString("SavingData", JsonUtility.ToJson(savingData));
        PlayerPrefs.Save();
#endif
    }

    public static PlayerData GetPlayerData()
    {
        string dataJson = JsonUtility.ToJson(new PlayerData());
        try
        {
#if UNITY_EDITOR
            dataJson = File.ReadAllText("Assets/SavingData.json");
#elif UNITY_ANDROID
            if (PlayerPrefs.HasKey("SavingData"))
            {
                dataJson = PlayerPrefs.GetString("SavingData");
            }
#endif
        }
        catch { }
        return JsonUtility.FromJson<PlayerData>(dataJson);
    }
}
