using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float forwardSpeed;
    [SerializeField] private float sideSpeed;
    [SerializeField] private float shoesSkinScale;
    [SerializeField] private GameObject shoesSpawnPoint;
    [SerializeField] private Joystick shoesMovementjoystick;
    [SerializeField] private AudioSource stompAudioSource;
    [SerializeField] private List<AudioClip> stompClips;

    private Rigidbody playerRigidbody;

    public GameObject ShoesSpawnPoint { get => shoesSpawnPoint; set => shoesSpawnPoint = value; }

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        foreach (Transform child in ShoesSpawnPoint.transform)
        {
            Destroy(child.gameObject);
        }
        Instantiate(Player.instance.AllShoes.Shoes[Player.instance.PlayerData.SelectedShoesIndex].Prefab, ShoesSpawnPoint.transform.position, ShoesSpawnPoint.transform.rotation, parent: ShoesSpawnPoint.transform);
    }

    private void FixedUpdate()
    {
        Vector3 forward = transform.TransformDirection(new Vector3(sideSpeed * shoesMovementjoystick.Horizontal, 0, forwardSpeed));
        playerRigidbody.MovePosition(transform.position + forward * Time.fixedDeltaTime);
    }

    public void PlayStompAudio()
    {
        stompAudioSource.clip = stompClips[Random.Range(0, stompClips.Count)];
        stompAudioSource.Play();
    }

    public void StopMovement()
    {
        sideSpeed = 0;
        forwardSpeed = 0;
    }
}