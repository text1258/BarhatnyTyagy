using UnityEngine;

public class MathUtils
{
    public static float MaximumVectorSide(Vector3 vector)
    {
        return Mathf.Max(vector.x, Mathf.Max(vector.y, vector.z));
    }

    public static float MaxBoundSideLength(GameObject model)
    {
        return MaximumVectorSide(BoundSize(model));
    }
    public static Vector3 BoundSize(GameObject model)
    {
        Bounds bounds = new Bounds(Vector3.zero, Vector3.zero);
        foreach (MeshFilter renderer in model.GetComponentsInChildren<MeshFilter>())
        {
            bounds.Encapsulate(renderer.sharedMesh.bounds);
        }
        return bounds.size;
    }
}