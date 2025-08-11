using System;
using AbstractServiceLocator;
using UnityEngine;

namespace nDependents
{
	// Loads the CompositionRootAsset, IServiceContainerAsset, initializes the IServiceContainer, and injects it into the Locator.
	// Shuts down the IServiceContainer and removes it from the Locator when quitting the application.
	public static class CompositionRoot
	{
		private static IServiceContainer ServiceContainer;
		private static IServiceLocator GetServiceLocator()
		{
			return ServiceContainer;
		}

		static CompositionRoot()
		{
			Application.quitting += Quitting;
		}

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void Initialize()
		{
			CompositionRootAsset compositionRootAsset = Resources.Load<CompositionRootAsset>(CompositionRootAsset.FileName);
			if(compositionRootAsset == null)
			{
				Debug.LogError("Failed to load the required composition root resource. Please create the required CompositionRoot asset in a Resources directory.");
			}
			else
			{
				IServiceContainerAsset serviceContainerAsset = compositionRootAsset.ServiceContainer as IServiceContainerAsset;
				if(serviceContainerAsset == null)
				{
					Debug.LogError("No service container asset has been assigned to the composition root.");
				}
				else
				{
					try
					{
						ServiceContainer = serviceContainerAsset.InitializeServiceContainer();
						Locator.Register(GetServiceLocator);
					}
					catch(Exception exception)
					{
						Debug.LogException(exception);
					}
				}
			}
		}

		private static void Quitting()
		{
			Locator.Remove(GetServiceLocator);

			if(ServiceContainer != null)
			{
				try
				{
					ServiceContainer.ShutDown();
				}
				catch(Exception exception)
				{
					Debug.LogException(exception);
				}
				finally
				{
					ServiceContainer = null;
				}
			}
		}
	}
}
