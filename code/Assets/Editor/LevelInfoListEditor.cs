using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(LevelInfoList))]

public class LevelInfoListEditor : Editor {

	enum displayFieldType {DisplayAsAutomaticFields, DisplayAsCustomizableGUIFields}
	displayFieldType DisplayFieldType;

	LevelInfoList l;
	SerializedObject GetTarget;
	SerializedProperty ThisList;
	int ListSize;

	void OnEnable() {
		l = (LevelInfoList)target;
		GetTarget = new SerializedObject (l);
		ThisList = GetTarget.FindProperty ("levelsInfo");
	}

	public override void OnInspectorGUI() {
		GetTarget.Update ();

		EditorGUILayout.Space ();
		EditorGUILayout.Space ();
		//DisplayFieldType = (displayFieldType)EditorGUILayout.EnumPopup ("", DisplayFieldType);

		ListSize = ThisList.arraySize;
		ListSize = EditorGUILayout.IntField ("List Size", ListSize);

		if (ListSize != ThisList.arraySize) {
			while(ListSize > ThisList.arraySize) {
				ThisList.InsertArrayElementAtIndex(ThisList.arraySize);
			}
			while(ListSize < ThisList.arraySize){
				ThisList.DeleteArrayElementAtIndex(ThisList.arraySize - 1);
			}
		}

		EditorGUILayout.Space ();
		EditorGUILayout.LabelField("Or");
		EditorGUILayout.Space ();

		//Or add a new item to the List<> with a button
		EditorGUILayout.LabelField("Add a new item with a button");

		if(GUILayout.Button("Add New")){
			l.levelsInfo.Add(new LevelInfoList.LevelInfo());
		}

		EditorGUILayout.Space ();
		EditorGUILayout.Space ();
		EditorGUILayout.BeginVertical();
		//Display our list to the inspector window
		for (int i = 0; i < ThisList.arraySize; i++) {
			SerializedProperty ListRef = ThisList.GetArrayElementAtIndex(i);
			SerializedProperty Creep = ListRef.FindPropertyRelative("creep");
			SerializedProperty Target = ListRef.FindPropertyRelative("target");
			SerializedProperty Amount = ListRef.FindPropertyRelative("amount");
			SerializedProperty Health = ListRef.FindPropertyRelative("health");
			SerializedProperty SpawnTime = ListRef.FindPropertyRelative("spawnTime");

			EditorGUILayout.LabelField("Waves");
			Creep.objectReferenceValue = EditorGUILayout.ObjectField("Unit Type", Creep.objectReferenceValue, typeof(GameObject), true);
			Target.objectReferenceValue = EditorGUILayout.ObjectField("Target", Target.objectReferenceValue, typeof(Transform), true);
			Amount.intValue = EditorGUILayout.IntField("Amount",Amount.intValue);
			Health.intValue = EditorGUILayout.IntField("Health",Health.intValue);
			SpawnTime.floatValue = EditorGUILayout.FloatField("Spawn time", SpawnTime.floatValue);

			//Remove this index from the List
			if(GUILayout.Button("Remove Wave (" + i.ToString() + ")")){
				ThisList.DeleteArrayElementAtIndex(i);
			}


			EditorGUILayout.Space ();
		}
		EditorGUILayout.EndVertical();

		//Apply the changes to our list
		GetTarget.ApplyModifiedProperties();
	}
}
