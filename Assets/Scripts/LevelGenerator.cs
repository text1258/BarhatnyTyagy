using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private Track walkway;
    [SerializeField] private List<Track> bonusWalkways;
    [SerializeField] private List <GameObject> bonusBranches;
    [SerializeField] private Finish finish;

    [Button]
    private void fdkfl()
    {
        Debug.Log(Utils.GetRealSize(gameObject));
    }
}
