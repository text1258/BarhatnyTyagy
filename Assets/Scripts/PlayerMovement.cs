using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float forwardSpeed;
    [SerializeField] private float sideSpeed;
    [SerializeField] private float shoesSkinScale;
    [SerializeField] private GameObject shoesSpawnPoint;

    private Vector2 lastClickPosition = Vector2.zero;
    private float sideMoveDirection = 0;
    
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
        if (Input.GetMouseButton(0))
        {
            if (Input.mousePosition.x > lastClickPosition.x)
            {
                sideMoveDirection = 1;
            }
            else if (Input.mousePosition.x < lastClickPosition.x)
            {
                sideMoveDirection = -1;
            }
        }
        else
        {
            sideMoveDirection = 0;
        }
        Vector3 forward = transform.TransformDirection(new Vector3(SideSpeed * sideMoveDirection, 0, ForwardSpeed));
        transform.position += forward * Time.deltaTime;
        lastClickPosition = Input.mousePosition;
    }
}