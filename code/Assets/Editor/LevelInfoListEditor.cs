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
		DisplayFieldType = (displayFieldType)EditorGUILayout.EnumPopup ("", DisplayFieldType);

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
		EditorGUILayout.Space ();
		EditorGUILayout.LabelField("Or");
		EditorGUILayout.Space ();
		EditorGUILayout.Space ();

		//Or add a new item to the List<> with a button
		EditorGUILayout.LabelField("Add a new item with a button");

		if(GUILayout.Button("Add New")){
			l.levelsInfo.Add(new LevelInfoList.LevelInfo());
		}

		EditorGUILayout.Space ();
		EditorGUILayout.Space ();
		
		//Display our list to the inspector window
		for (int i = 0; i < ThisList.arraySize; i++) {
			SerializedProperty ListRef = ThisList.GetArrayElementAtIndex(i);
			SerializedProperty Creep = ListRef.FindPropertyRelative("creep");
			SerializedProperty Target = ListRef.FindPropertyRelative("target");
			SerializedProperty Amount = ListRef.FindPropertyRelative("amount");
			SerializedProperty Health = ListRef.FindPropertyRelative("health");

			// Display the property fields in two ways.
			if(DisplayFieldType == 0){// Choose to display automatic or custom field types. This is only for example to help display automatic and custom fields.
				//1. Automatic, No customization <-- Choose me I'm automatic and easy to setup
				EditorGUILayout.LabelField("Automatic Field By Property Type");
				EditorGUILayout.PropertyField(Creep);
				EditorGUILayout.PropertyField(Target);
				EditorGUILayout.PropertyField(Amount);
				EditorGUILayout.PropertyField(Health);
			}else{
				//Or
				
				//2 : Full custom GUI Layout <-- Choose me I can be fully customized with GUI options.
				EditorGUILayout.LabelField("Customizable Field With GUI");
				Creep.objectReferenceValue = EditorGUILayout.ObjectField("Unit Type", Creep.objectReferenceValue, typeof(GameObject), true);
				Target.objectReferenceValue = EditorGUILayout.ObjectField("Target", Target.objectReferenceValue, typeof(Transform), true);
				Amount.intValue = EditorGUILayout.IntField("Amount",Amount.intValue);
				Health.intValue = EditorGUILayout.IntField("Health",Health.intValue);
			}

			EditorGUILayout.Space ();

			//Remove this index from the List
			EditorGUILayout.LabelField("Remove an index from the List<> with a button");
			if(GUILayout.Button("Remove This Index (" + i.ToString() + ")")){
				ThisList.DeleteArrayElementAtIndex(i);
			}
			EditorGUILayout.Space ();
			EditorGUILayout.Space ();
			EditorGUILayout.Space ();
			EditorGUILayout.Space ();
		}

		//Apply the changes to our list
		GetTarget.ApplyModifiedProperties();
	}
}
