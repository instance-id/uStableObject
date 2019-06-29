﻿using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using uStableObject.Data.Localization;
using uStableObject.Utilities.Converters;

namespace                               uStableObject.Utilities.Editor
{
    public class                        ScriptableToolsWindow : EditorWindow
    {
        ScriptableObject                _parent;
        ScriptableObject                _child;
        ScriptableObject                _splitTarget;

        [MenuItem("Tools/Scriptables Tools")]
        static void                     Init()
        {
            // Get existing open window or if none, make a new one:
            ScriptableToolsWindow window = EditorWindow.GetWindow<ScriptableToolsWindow>("Scriptables Tools");
            window.Show();
        }

        void                            OnGUI()
        {
            GUILayout.Label("Merging", EditorStyles.boldLabel);
            this._parent = (ScriptableObject)EditorGUILayout.ObjectField("Parent", this._parent, typeof(ScriptableObject), false);
            this._child = (ScriptableObject)EditorGUILayout.ObjectField("Child", this._child, typeof(ScriptableObject), false);

            if (GUILayout.Button("Merge"))
            {
                //var childClone = Instantiate(this._child);
                //childClone.name = childClone.name.Replace("(Clone)", "");
                //AssetDatabase.AddObjectToAsset(childClone, this._parent);
                string sourcePath = AssetDatabase.GetAssetPath(this._child);
                AssetDatabase.RemoveObjectFromAsset(this._child);
                AssetDatabase.DeleteAsset(sourcePath);
                AssetDatabase.AddObjectToAsset(this._child, this._parent);
                EditorUtility.SetDirty(this._parent);
                AssetDatabase.SaveAssets();
                Selection.activeObject = this._child;
            }
            GUILayout.Space(30);
            GUILayout.Label("Splitting", EditorStyles.boldLabel);
            this._splitTarget = (ScriptableObject)EditorGUILayout.ObjectField("Target (child)", this._splitTarget, typeof(ScriptableObject), false);

            if (GUILayout.Button("Split"))
            {
                string sourcePath = System.IO.Path.GetDirectoryName(AssetDatabase.GetAssetPath(this._splitTarget));
                string filePath = System.IO.Path.Combine(sourcePath, this._splitTarget.name);
                string newPath = AssetDatabase.GenerateUniqueAssetPath(filePath + ".asset");
                AssetDatabase.RemoveObjectFromAsset(this._splitTarget);
                AssetDatabase.CreateAsset(this._splitTarget, newPath);
                EditorUtility.SetDirty(this._splitTarget);
                AssetDatabase.SaveAssets();
                Selection.activeObject = this._splitTarget;
            }
        }
    }
}