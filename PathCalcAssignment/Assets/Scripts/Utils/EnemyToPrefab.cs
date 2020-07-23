using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyToPrefab : EditorWindow
{
    public GameObject _prefab = default;

    public GameObject _prefabContainer = default;

    public GameObject[] _toBeConvertedList;

    [MenuItem("CBC/ConvertPrefabs")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow<EnemyToPrefab>("Converter");
    }

    // Start is called before the first frame update
    void Convert()
    {
        foreach(var gameobjectToBeConverted in _toBeConvertedList)
        {
            // var newPrefab = Instantiate<GameObject>(_prefab, gameobjectToBeConverted.transform);
            var newPrefab = (GameObject)PrefabUtility.InstantiatePrefab(_prefab);
            newPrefab.name = gameobjectToBeConverted.name;
            newPrefab.transform.parent = _prefabContainer.transform;
            newPrefab.transform.localScale = gameobjectToBeConverted.transform.localScale;
            newPrefab.transform.localPosition = gameobjectToBeConverted.transform.localPosition;
            newPrefab.transform.localRotation = gameobjectToBeConverted.transform.localRotation;
        }

        for (int i = _toBeConvertedList.Length-1; i >= 0 ; i--)
        {
            DestroyImmediate(_toBeConvertedList[i]);
        }
    }

    private void OnGUI()
    {
        _prefab = EditorGUILayout.ObjectField("Prefab", _prefab, typeof(GameObject), allowSceneObjects: true) as GameObject;
        _prefabContainer = EditorGUILayout.ObjectField("Container", _prefabContainer, typeof(GameObject), allowSceneObjects: true) as GameObject;
        ScriptableObject scriptableObj = this;
        SerializedObject serialObj = new SerializedObject(scriptableObj);
        SerializedProperty serialProp = serialObj.FindProperty("_toBeConvertedList");

        EditorGUILayout.PropertyField(serialProp, new GUIContent("ToBeConverted"), true);
        serialObj.ApplyModifiedProperties();

        if (GUILayout.Button("Convert"))
        {
            Convert();
        }
    }
}
