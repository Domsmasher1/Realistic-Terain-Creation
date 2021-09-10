using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(texture_creator))]
public class TextureCreatorInspector : Editor
{
	private texture_creator creator;

	private void OnEnable()
	{
		creator = target as texture_creator;
		Undo.undoRedoPerformed += RefreshCreator;
	}

	private void OnDisable()
	{
		Undo.undoRedoPerformed -= RefreshCreator;
	}

	private void RefreshCreator()
	{
		if (Application.isPlaying)
		{
			creator.FillTexture();
		}
	}

	public override void OnInspectorGUI()
	{
		EditorGUI.BeginChangeCheck();
		DrawDefaultInspector();
		if (EditorGUI.EndChangeCheck() && Application.isPlaying)
		{
			(target as texture_creator).FillTexture();
			{ RefreshCreator(); }
		}
	}
}