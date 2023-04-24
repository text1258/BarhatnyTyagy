using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MudRandomizer : EditorWindow
{

    [SerializeField] private List<Mud> sceneMuds;

    [MenuItem("Tools/MudRandomizer")]
    public static void ShowWindow()
    {
        MudRandomizer window = GetWindow<MudRandomizer>("MudRandomizer");
        window.minSize = new Vector2(250, 300);
    }

    private void OnGUI()
    {
        SerializedObject serializedObject = new SerializedObject(this);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("sceneMuds"), true);
        serializedObject.ApplyModifiedProperties();
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("FindAllMuds", GUILayout.Height(60)))
        {
            foreach (Mud mud in FindObjectsByType<Mud>(FindObjectsSortMode.None))
            {
                if (sceneMuds.Contains(mud) == false)
                {
                    sceneMuds.Add(mud);
                }
            }
        }
        GUILayout.Space(30);
        if (GUILayout.Button("Randomize", GUILayout.Height(60)))
        {
            if (sceneMuds != null)
            {
                foreach (Mud mud in sceneMuds)
                {
                    if (mud != null)
                    {
                        mud.RandomizeRotatonAndScale();
                    }
                }

            }
        }
    }
}