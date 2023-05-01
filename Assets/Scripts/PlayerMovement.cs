using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float forwardSpeed;
    [SerializeField] private float sideSpeed;
    [SerializeField] private float shoesSkinScale;
    [SerializeField] private GameObject shoesSpawnPoint;
    [SerializeField] private Joystick shoesMovementjoystick;
    [SerializeField] private AudioSource stompAudioSource;
    [SerializeField] private List<AudioClip> stompClips;

    public float SideSpeed { get => sideSpeed; set => sideSpeed = value; }

    public float ForwardSpeed { get => forwardSpeed; set => forwardSpeed = value; }

    public GameObject ShoesSpawnPoint { get => shoesSpawnPoint; set => shoesSpawnPoint = value; }

    private void Start()
    {
        foreach (Transform child in ShoesSpawnPoint.transform)
        {
            Destroy(child.gameObject);
        }
        Instantiate(Player.instance.AllShoes.Shoes[Player.instance.PlayerData.SelectedShoesIndex].Prefab, ShoesSpawnPoint.transform.position, ShoesSpawnPoint.transform.rotation, parent: ShoesSpawnPoint.transform);
    }

    private void Update()
    {
        Vector3 forward = transform.TransformDirection(new Vector3(SideSpeed * shoesMovementjoystick.Horizontal, 0, ForwardSpeed));
        transform.position += forward * Time.deltaTime;
    }

    public void PlayStompAudio()
    {
        stompAudioSource.clip = stompClips[Random.Range(0, stompClips.Count)];
        stompAudioSource.Play();
    }
}