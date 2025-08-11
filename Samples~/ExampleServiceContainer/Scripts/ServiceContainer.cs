using System;
using AbstractServiceLocator;
using nDependents;
using UnityEngine;

public sealed class ServiceContainer : IServiceContainer
{
	ServiceContainerAsset Settings;
	ServiceContainerUpdater Updater;

	public ServiceContainer(ServiceContainerAsset settings)
	{
		if(settings == null)
			throw new ArgumentNullException("settings");

		Settings = settings;

		Debug.Log(Settings.InitializeMessage);

		// Schedule Update
		GameObject gameObject = new GameObject("ServiceContainerUpdater");
		UnityEngine.Object.DontDestroyOnLoad(gameObject);
		gameObject.hideFlags = HideFlags.HideInHierarchy;
		Updater = gameObject.AddComponent<ServiceContainerUpdater>();
		Updater.Updated += Update;
	}

	private void Update()
	{
		Debug.Log(Settings.UpdateMessage);
	}

	public void ShutDown()
	{
		Debug.Log(Settings.ShutDownMessage);

		Updater.Updated -= Update;
		UnityEngine.Object.Destroy(Updater);
	}

	public T Get<T>() where T : class
	{
		Type type = typeof(T);

		// A MonoBehaviour can call Locator.Get<T>() to request an instance of any type.
		// This is where you can return a concrete service to an interface type request.
		// You don't need to return this whole IServiceLocator.
		// It's just to demonstrate type comparison and returning an object as a requested type.
		if(type == typeof(IServiceLocator))
			return this as T;

		return null;
	}
}
