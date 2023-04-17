using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float forwardSpeed;
    [SerializeField] private float sideSpeed;
    [SerializeField] private float shoesSkinScale;

    private Vector2 lastClickPosition = Vector2.zero;
    private float sideMoveDirection = 0;
    
    public float SideSpeed { get => sideSpeed; set => sideSpeed = value; }

    private void Start()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        Instantiate(Player.instance.AllShoes.Shoes[Player.instance.PlayerData.SelectedShoesIndex].Prefab, transform.position, transform.rotation, parent: transform);
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
        Vector3 forward = transform.TransformDirection(new Vector3(sideSpeed * sideMoveDirection, 0, forwardSpeed));
        transform.position += forward * Time.deltaTime;
        lastClickPosition = Input.mousePosition;
    }
}