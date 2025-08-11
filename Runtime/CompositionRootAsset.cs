using ScrutableObjects;
using UnityEngine;

namespace nDependents
{
	// The custom project settings menu is only available after 2018.3.
	// Without it, you must create the CompositionRoot resource manually.
#if !UNITY_2018_3_OR_NEWER
	[CreateAssetMenu(menuName = "Dependency Injection/" + FileName, order = 1080, fileName = FileName)]
#endif
	public sealed class CompositionRootAsset : ScriptableObject
	{
		public const string FileName = "CompositionRoot";

		[Tooltip("The service container asset to initialize when the application starts. Must implement the IServiceContainerAsset interface.")]
		[ShowProperties(LockObjectAtRuntime = true)]
		public ScriptableObject ServiceContainer;
	}
}
