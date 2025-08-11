#if UNITY_2018_3_OR_NEWER
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace nDependents.UnityEditor
{
	public sealed class NDependentsSettingsProvider : SettingsProvider
	{
		private CompositionRootAsset CompositionRootAsset;
		private SerializedObject SerializedCompositionRoot;

		private int FrameCount;
		private static int EditorFrameCount;

		private static NDependentsSettingsProvider Instance = new NDependentsSettingsProvider("Project/Dependency Injection", SettingsScope.Project, null);

		public NDependentsSettingsProvider(string path, SettingsScope scopes, IEnumerable<string> keywords) : base(path, scopes, keywords)
		{ }

		[SettingsProvider]
		public static SettingsProvider GetSettingsProvider()
		{
			return Instance;
		}

		static NDependentsSettingsProvider()
		{
			EditorApplication.update += EditorUpdate;
		}

		private static void EditorUpdate()
		{
			++EditorFrameCount;
		}

		private void LoadOncePerEditorFrame()
		{
			if(FrameCount != EditorFrameCount)
			{
				FrameCount = EditorFrameCount;
				LoadCompositionRootAsset();
			}
		}

		private void LoadCompositionRootAsset()
		{
			CompositionRootAsset compositionRoot = Resources.Load<CompositionRootAsset>(CompositionRootAsset.FileName);

			// Create New Composition Root Asset
			if(compositionRoot == null)
			{
				compositionRoot = ScriptableObject.CreateInstance<CompositionRootAsset>();

				if(!AssetDatabase.IsValidFolder("Assets/Dependency Injection"))
					AssetDatabase.CreateFolder("Assets", "Dependency Injection");

				if(!AssetDatabase.IsValidFolder("Assets/Dependency Injection/Assets"))
					AssetDatabase.CreateFolder("Assets/Dependency Injection", "Assets");

				if(!AssetDatabase.IsValidFolder("Assets/Dependency Injection/Assets/Resources"))
					AssetDatabase.CreateFolder("Assets/Dependency Injection/Assets", "Resources");

				AssetDatabase.CreateAsset(compositionRoot, "Assets/Dependency Injection/Assets/Resources/" + CompositionRootAsset.FileName + ".asset");
			}

			// Composition Root Asset Changed
			if(CompositionRootAsset != compositionRoot)
			{
				CompositionRootAsset = compositionRoot;
				SerializedCompositionRoot = new SerializedObject(CompositionRootAsset);
				keywords = GetSearchKeywordsFromSerializedObject(SerializedCompositionRoot);
			}
		}

		public override void OnGUI(string searchContext)
		{
			LoadOncePerEditorFrame();

			// Readonly Composition Root Asset
			using(new EditorGUI.DisabledGroupScope(true))
			{
				EditorGUILayout.ObjectField("Composition Root", CompositionRootAsset, typeof(CompositionRootAsset), false);
			}

			// Composition Root Asset Properties
			if(SerializedCompositionRoot != null)
			{
				SerializedProperty property = SerializedCompositionRoot.GetIterator();
				property.NextVisible(true);
				EditorGUI.BeginChangeCheck();
				while(property.NextVisible(false))
				{
					EditorGUILayout.PropertyField(property, new GUIContent(property.displayName), true);
				}
				if(EditorGUI.EndChangeCheck())
					SerializedCompositionRoot.ApplyModifiedProperties();
			}
		}

		public override void OnDeactivate()
		{
			CompositionRootAsset = null;
			SerializedCompositionRoot = null;
		}
	}
}
#endif
