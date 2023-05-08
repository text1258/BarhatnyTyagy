using UnityEngine;
using NaughtyAttributes;
using static Utils;

public class Track : MonoBehaviour
{
    [SerializeField] private Transform level;
    [SerializeField] private BonusSpawnData coinSpawnData;
    [SerializeField] private BonusSpawnData mudSpawnData;
    [SerializeField, Min(0)] private float minimalDistance;
    [Header("SpawnZone")]
    [SerializeField] private Indent indent;

    private Vector3 SpawnZoneTopRightPoint
    {
        get
        {
            return new Vector3(transform.localScale.x / 2 - indent.right, 0, transform.localScale.z / 2 - indent.top);
        }
    }
    private Vector3 SpawnZoneLeftBottomPoint
    {
        get
        {
            return new Vector3(transform.localScale.x / 2 - indent.left, 0, transform.localScale.z / 2 - indent.bottom);
        }
    }
    private Vector3 SpawnZoneSize
    {
        get
        {
            return new Vector3(SpawnZoneTopRightPoint.x + SpawnZoneLeftBottomPoint.x, 1, SpawnZoneTopRightPoint.z + SpawnZoneLeftBottomPoint.z);
        }
    }

    public Transform Level { get => level; set => level = value; }

    public BonusSpawnData CoinSpawnData => coinSpawnData;

    public BonusSpawnData MudSpawnData => mudSpawnData;


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0f, 0f, 1f, 0.5f);
        Gizmos.DrawCube(transform.position + (SpawnZoneTopRightPoint - SpawnZoneLeftBottomPoint) * 0.5f, SpawnZoneSize);
    }

    [Button]
    public void RandomBonusesSpawn()
    {
        if (SpawnZoneSize.z / (CoinSpawnData.count + MudSpawnData.count - 1) < minimalDistance)
        {
            Debug.LogError("There are too many spawning bonuses! Please lower their number!");
            return;
        }
        if (CoinSpawnData.count + MudSpawnData.count == 0)
        {
            return;
        }
        uint remainingCoinPrefabCount = CoinSpawnData.count;
        uint remainingMudPrefabCount = MudSpawnData.count;
        for (float currentSpawnPosition = -SpawnZoneLeftBottomPoint.z; currentSpawnPosition <= SpawnZoneTopRightPoint.z; currentSpawnPosition += SpawnZoneSize.z / (CoinSpawnData.count + MudSpawnData.count - 1))
        {
            GameObject current = null;
            if (remainingCoinPrefabCount > 0 & remainingMudPrefabCount > 0)
            {
                current = Instantiate(RandomObject(CoinSpawnData, MudSpawnData).prefab);
                if (current.GetComponent<Coin>() != null)
                {
                    remainingCoinPrefabCount -= 1;
                }
                else if (current.GetComponent<Mud>() != null)
                {
                    remainingMudPrefabCount -= 1;
                }
            }
            else if (remainingCoinPrefabCount > 0 & remainingMudPrefabCount == 0)
            {
                current = Instantiate(CoinSpawnData.prefab);
                remainingCoinPrefabCount -= 1;
            }
            else if (remainingCoinPrefabCount == 0 & remainingMudPrefabCount > 0)
            {
                current = Instantiate(MudSpawnData.prefab);
                remainingMudPrefabCount -= 1;
            }
            current.transform.position = new Vector3(Random.Range(transform.position.x - SpawnZoneLeftBottomPoint.x, transform.position.x + SpawnZoneTopRightPoint.x), current.transform.position.y, transform.position.z + currentSpawnPosition);
            current.GetComponent<Mud>()?.RandomizeRotatonAndScale();
            current.transform.SetParent(Level);
        }
    }
}

[System.Serializable]
public class BonusSpawnData
{
    public GameObject prefab;
    public uint count;
}

[System.Serializable]
public class Indent
{
    public float left;
    public float right;
    public float top;
    public float bottom;
}