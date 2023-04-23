using NaughtyAttributes;
using UnityEngine;


public class Mud : InteractiveObject
{
    [SerializeField] private int mudCount;
    [SerializeField] private Material footPrint;
    [SerializeField] private ParticleSystem mudBubblesEffect;
    [SerializeField, MinMaxSlider(0f, 360f)] private Vector2 rotateAngle;
    [SerializeField, MinMaxSlider(0.1f, 0.4f)] private Vector2 scaleX;
    [SerializeField, MinMaxSlider(0.1f, 0.4f)] private Vector2 scaleY;
    [SerializeField, MinMaxSlider(0.1f, 0.4f)] private Vector2 scaleZ;

    public override void Action()
    {
        GetComponent<Collider>().enabled = false;
        mudBubblesEffect.Play();
        if (Player.instance.MudCount + mudCount > Player.instance.MaxMud)
        {
            Player.instance.MudCount = Player.instance.MaxMud;
        }
        else
        {
            Player.instance.MudCount += mudCount;
        }
    }

    [Button]
    private void RandomizeRotatonAndScale()
    {
        transform.rotation = Quaternion.Euler(0f, Random.Range(rotateAngle.x, rotateAngle.y), 0f);
        transform.localScale = new Vector3(Random.Range(scaleX.x, scaleX.y), Random.Range(scaleY.x, scaleY.y), Random.Range(scaleZ.x, scaleZ.y));
    }
}
