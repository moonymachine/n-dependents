using nDependents;
using ScrutableObjects;
using UnityEngine;

[CreateAssetMenu(menuName = "Dependency Injection/ServiceContainer", order = 1080, fileName = "ServiceContainer")]
public sealed class ServiceContainerAsset : ScriptableObject, IServiceContainerAsset
{
	public string InitializeMessage = "Good morning world!";
	public string UpdateMessage = "Hello world!";
	public string ShutDownMessage = "Good night world!";

	public IServiceContainer InitializeServiceContainer()
	{
		return new ServiceContainer(this);
	}
}
