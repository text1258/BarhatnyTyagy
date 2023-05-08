using NaughtyAttributes;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Utils;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private Track walkway;
    [SerializeField] private Track startWalkway;
    [SerializeField] private Track endWalkway;
    [SerializeField] private Track moneyWalkway;
    [SerializeField] private Track mudWalkway;
    [SerializeField] private Track moneyAndMudWalkway;
    [SerializeField] private Track platform;
    [SerializeField] private List<GameObject> bonusBranches;
    [SerializeField] private BoxCollider levelRestarter;
    [SerializeField] private Finish finish;
    [SerializeField] private int iterations;
    [SerializeField] private bool useBonusBranches = true;

    private float pastPath = 0f;

    [Button]
    private void DestroyLevel()
    {
        pastPath = 0f;
        GameObject[] tempArray = new GameObject[transform.childCount];
        for (int i = 0; i < tempArray.Length; i++)
        {
            tempArray[i] = transform.GetChild(i).gameObject;
        }
        foreach (GameObject child in tempArray)
        {
            if (child.GetComponent<LevelRestarter>() != null | child.GetComponent<Finish>() != null)
            {
                continue;
            }
            DestroyImmediate(child);
        }
    }

    [Button]
    private void GenerateLevel()
    {
        DestroyLevel();
        CreateTrack(startWalkway);
        CreateTrack(platform).RandomBonusesSpawn();
        for (int i = 0; i < iterations; i++)
        {
            if (useBonusBranches == true)
            {
                GameObject currentBonusBranch = Instantiate(bonusBranches[UnityEngine.Random.Range(0, bonusBranches.Count)]);
                int side = RandomObject(1, -1);
                currentBonusBranch.transform.position = new Vector3(platform.GetComponent<Renderer>().bounds.size.x / 2 * -side, currentBonusBranch.transform.position.y, pastPath);
                currentBonusBranch.transform.localScale = new Vector3(currentBonusBranch.transform.localScale.x * side, currentBonusBranch.transform.localScale.y, currentBonusBranch.transform.localScale.z);
                currentBonusBranch.transform.SetParent(transform);
            }
            GenerateWalkwaysWithPlatform();
        }
        CreateTrack(endWalkway);
        finish.transform.position = new Vector3(0, finish.transform.position.y, pastPath);
        levelRestarter.size = new Vector3(levelRestarter.size.x, levelRestarter.size.y, pastPath * 3);
        pastPath = 0f;
    }

    private void GenerateWalkwaysWithPlatform()
    {
        RandomObject<Action>(GenerateTwoChooseWalkways, GenerateThreeChooseWalkways).Invoke();
        CreateTrack(platform).RandomBonusesSpawn();
    }

    private void GenerateTwoChooseWalkways()
    {
        RandomReplace(CreateTrack(moneyWalkway, (platform.GetComponent<Renderer>().bounds.size.x - walkway.GetComponent<Renderer>().bounds.size.x) / 4, false).gameObject, CreateTrack(mudWalkway, (platform.GetComponent<Renderer>().bounds.size.x - walkway.GetComponent<Renderer>().bounds.size.x) / -4).gameObject);
    }

    private void GenerateThreeChooseWalkways()
    {
        RandomReplace(CreateTrack(moneyWalkway, (platform.GetComponent<Renderer>().bounds.size.x - walkway.GetComponent<Renderer>().bounds.size.x) / 2, false).gameObject, CreateTrack(mudWalkway, (platform.GetComponent<Renderer>().bounds.size.x - walkway.GetComponent<Renderer>().bounds.size.x) / -2, false).gameObject, CreateTrack(moneyAndMudWalkway).gameObject);
    }

    private Track CreateTrack(Track track, float sideOffset = 0, bool addPastPath = true)
    {
        Track creatingTrak = Instantiate(track, parent: transform);
        creatingTrak.Level = transform;
        creatingTrak.transform.position = new Vector3(sideOffset, creatingTrak.transform.position.y, pastPath + track.GetComponent<Renderer>().bounds.size.z / 2);
        if (addPastPath == true)
        {
            pastPath += track.GetComponent<Renderer>().bounds.size.z;
        }
        for (int i = creatingTrak.transform.childCount; i > 0; --i)
        {
            creatingTrak.transform.GetChild(0).SetParent(transform);
        }
        return creatingTrak;
    }
}
