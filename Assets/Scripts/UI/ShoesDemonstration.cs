using UnityEngine;
using UnityEngine.EventSystems;

public class ShoesDemonstration : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public static ShoesDemonstration instance;
    [SerializeField] private Transform demonstrationObject;
    [SerializeField] private Vector3 demonstrationObjectSpawnRotation;
    [SerializeField] private Vector3 demonstrationObjectSpawnPosition;
    [SerializeField] private float swipeFactor;

    private bool isPointOnDemonstrationZone = false;
    private Vector3 clickPositionBeforeRotate;
    private Quaternion rotationBeforeRotate;

    public Transform DemonstrationObject
    {
        get => demonstrationObject; 
        set
        {
            if (demonstrationObject != null)
            {
                Destroy(demonstrationObject.gameObject);
                demonstrationObject = null;
            }
            demonstrationObject = Instantiate(value.gameObject, parent: transform).transform;
            demonstrationObject.rotation = Quaternion.Euler(demonstrationObjectSpawnRotation);
            demonstrationObject.localPosition = demonstrationObjectSpawnPosition;
            demonstrationObject.localScale = GetComponent<RectTransform>().rect.width / Utils.MaxBoundSideLength(Player.instance.AllShoes.Shoes[Player.instance.PlayerData.SelectedShoesIndex].Model) * Player.instance.AllShoes.Shoes[Player.instance.PlayerData.SelectedShoesIndex].Model.transform.localScale;
        }
    }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        DemonstrationObject = Player.instance.AllShoes.Shoes[Player.instance.PlayerData.SelectedShoesIndex].Model.transform;
    }

    private void Update()
    {
        if (isPointOnDemonstrationZone)
        {
            if (Input.GetMouseButtonDown(0))
            {
                clickPositionBeforeRotate = Input.mousePosition;
                rotationBeforeRotate = DemonstrationObject.transform.rotation;
            }
            else if (Input.GetMouseButton(0))
            {
                Vector3 delta = Input.mousePosition - clickPositionBeforeRotate;
                float angleY = -delta.x * swipeFactor;
                Quaternion rotationY = Quaternion.Euler(0f, angleY, 0f);
                DemonstrationObject.transform.rotation = Quaternion.Euler(DemonstrationObject.rotation.eulerAngles.x, (rotationBeforeRotate * rotationY).eulerAngles.y, DemonstrationObject.rotation.eulerAngles.z);
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isPointOnDemonstrationZone = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPointOnDemonstrationZone = false;
    }
}
