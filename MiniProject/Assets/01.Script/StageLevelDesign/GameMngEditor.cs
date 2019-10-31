using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameMng))]
public class GameMngEditor : Editor
{

	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		GameMng myScript = (GameMng)target;
		if (GUILayout.Button("Change Stage"))
		{
			GameMng.Ins.ChangeStage();
		}
	}

}
