using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(ParaobjManager))]
public class TabEditor : Editor
{

    public override void OnInspectorGUI()
    {
        ParaobjManager objectGenerator = (ParaobjManager)target;
        
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        objectGenerator.type = (ParaobjManager.ObjectType)EditorGUILayout.EnumPopup("Object Type", objectGenerator.type);
        EditorGUILayout.Space();

        if (objectGenerator.type != ParaobjManager.ObjectType.none)
        {
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Ammount of Edges generated", EditorStyles.boldLabel);
            EditorGUILayout.Space();
            objectGenerator.XYEdgeSize = EditorGUILayout.IntField("X/Y Edge Ammount", objectGenerator.XYEdgeSize);
            objectGenerator.ZEdgeSize = EditorGUILayout.IntField("Z Edge Ammount", objectGenerator.ZEdgeSize);
        }
        if (objectGenerator.type != ParaobjManager.ObjectType.none && objectGenerator.type != ParaobjManager.ObjectType.torus)
        {
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Size of the Objekt", EditorStyles.boldLabel);
            EditorGUILayout.Space();
            objectGenerator.XRadius = EditorGUILayout.FloatField("X Radius", objectGenerator.XRadius);
            objectGenerator.YRadius = EditorGUILayout.FloatField("Y Radius", objectGenerator.YRadius);
            objectGenerator.ZRadius = EditorGUILayout.FloatField("Z Radius", objectGenerator.ZRadius);
        }
        if (objectGenerator.type == ParaobjManager.ObjectType.torus)
        {
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Size of the Objekt", EditorStyles.boldLabel);
            EditorGUILayout.Space();
            objectGenerator.TorusRadiusX = EditorGUILayout.FloatField("Torus X Radius", objectGenerator.TorusRadiusX);
            objectGenerator.TorusRadiusY = EditorGUILayout.FloatField("Torus Y Radius", objectGenerator.TorusRadiusY);
            EditorGUILayout.Space();
            objectGenerator.XRadius = EditorGUILayout.FloatField("Local X Radius", objectGenerator.XRadius);
            objectGenerator.YRadius = EditorGUILayout.FloatField("Local Y Radius", objectGenerator.YRadius);
            objectGenerator.ZRadius = EditorGUILayout.FloatField("Local Z Radius", objectGenerator.ZRadius);
        }
        if (objectGenerator.type == ParaobjManager.ObjectType.wheel)
        {
            EditorGUILayout.Space();
            objectGenerator.Stretch = EditorGUILayout.FloatField("Stretch", objectGenerator.Stretch);
        }
        if (objectGenerator.type == ParaobjManager.ObjectType.cone)
        {
            EditorGUILayout.Space();
            objectGenerator.Bow = EditorGUILayout.FloatField("Bow", objectGenerator.Bow);
        }
        if (objectGenerator.type == ParaobjManager.ObjectType.spiral)
        {
            EditorGUILayout.Space();
            objectGenerator.SpinAmmount = EditorGUILayout.FloatField("Spin Ammount", objectGenerator.SpinAmmount);
        }
        
        if (objectGenerator.type != ParaobjManager.ObjectType.none)
        {
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Gizmo", EditorStyles.boldLabel);
            objectGenerator.drawGizmo = EditorGUILayout.Toggle("Draw Gizmo", objectGenerator.drawGizmo);
            objectGenerator.gizmoSize = EditorGUILayout.FloatField("Gizmo Size", objectGenerator.gizmoSize);
        }
    }
}
