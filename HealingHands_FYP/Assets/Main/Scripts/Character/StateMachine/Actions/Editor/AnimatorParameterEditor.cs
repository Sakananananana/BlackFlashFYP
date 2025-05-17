using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CustomEditor(typeof(AnimatorParameterActionSO)), CanEditMultipleObjects]
public class AnimatorParameterEditor : CustomBaseEditor
{
    public override void OnInspectorGUI()
    {
        base.DrawNonEditableScriptReference<AnimatorParameterActionSO>();

        serializedObject.Update();

        EditorGUILayout.PropertyField(serializedObject.FindProperty("WhenToRun"));
        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Animator Parameter", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("ParameterName"), new GUIContent("Name"));

        // Draws the appropriate value depending on the type of parameter this SO is going to change on the Animator
        SerializedProperty animParamValue = serializedObject.FindProperty("parameterType");
        EditorGUILayout.PropertyField(animParamValue, new GUIContent("Type"));

        switch (animParamValue.intValue)
        {
            case (int)AnimatorParameterActionSO.ParameterType.Bool:
                EditorGUILayout.PropertyField(serializedObject.FindProperty("BoolValue"), new GUIContent("Desired Value"));
                break;
            case (int)AnimatorParameterActionSO.ParameterType.Int:
                EditorGUILayout.PropertyField(serializedObject.FindProperty("IntValue"), new GUIContent("Desired Value"));
                break;
            case (int)AnimatorParameterActionSO.ParameterType.Float:
                EditorGUILayout.PropertyField(serializedObject.FindProperty("FloatValue"), new GUIContent("Desired Value"));
                break;
        }

        serializedObject.ApplyModifiedProperties();
    }
}

public class CustomBaseEditor : Editor
{
    public void DrawNonEditableScriptReference<T>() where T : Object
    {
        GUI.enabled = false;

        if (typeof(ScriptableObject).IsAssignableFrom(typeof(T)))
            EditorGUILayout.ObjectField("Script", MonoScript.FromScriptableObject((ScriptableObject)target), typeof(T), false);
        else if (typeof(MonoBehaviour).IsAssignableFrom(typeof(T)))
            EditorGUILayout.ObjectField("Script", MonoScript.FromMonoBehaviour((MonoBehaviour)target), typeof(T), false);

        GUI.enabled = true;
    }
}


