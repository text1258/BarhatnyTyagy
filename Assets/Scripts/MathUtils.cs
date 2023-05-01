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

    public static Vector3 RandomVector(Vector3 startVector, Vector3 endVector)
    {
        return new Vector3(Random.Range(startVector.x, endVector.x), Random.Range(startVector.y, endVector.y), Random.Range(startVector.z, endVector.z));
    }

    public static Vector2 RandomVector(Vector2 startVector, Vector2 endVector)
    {
        return new Vector2(Random.Range(startVector.x, endVector.x), Random.Range(startVector.y, endVector.y));
    }
}