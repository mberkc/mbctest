using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor (typeof (RandomFloatCollection))]
public class FloatCollectionInspector : Editor {

    public override void OnInspectorGUI () {
        DrawDefaultInspector ();
        RandomFloatCollection collection = (RandomFloatCollection) target;
        if (GUILayout.Button ("Generate")) {
            collection.Generate ();
            EditorUtility.SetDirty (collection);
        }
    }
}