using System.Collections;
using System.Linq;
using UnityEngine;

public static class Utils
{
    public static float MaximumVectorSide(Vector3 vector)
    {
        return Mathf.Max(vector.x, Mathf.Max(vector.y, vector.z));
    }

    public static float MaxBoundSideLength(GameObject model)
    {
        return MaximumVectorSide(GetRealSize(model));
    }

    public static Vector3 GetRealSize(GameObject model)
    {
        Bounds allBounds = new Bounds(Vector3.zero, Vector3.zero);
        foreach (Renderer renderer in model.GetComponentsInChildren<Renderer>())
        {
            allBounds.Encapsulate(renderer.bounds);
        }
        return allBounds.size;
    }

    public static T RandomObject<T>(params T[] objects)
    {
        return objects[Random.Range(0, objects.Length)];
    }

    public static void RandomReplace(params GameObject[] gameObjects)
    {
        Vector3[] positions = gameObjects.Select(x => x.transform.position).ToArray();
        for (int i = 0; i < positions.Length; i++)
        {
            int randomIndex = Random.Range(0, gameObjects.Length);
            gameObjects[randomIndex].transform.position = positions[i];
            gameObjects = gameObjects.Where(x => x != gameObjects[randomIndex]).ToArray();
        }
    }

    public static IEnumerator DisactiveMediately(GameObject disactivirtGameObject, float time = 0)
    {
        yield return new WaitForSeconds(time);
        disactivirtGameObject.SetActive(false);
        yield break;
    }
}