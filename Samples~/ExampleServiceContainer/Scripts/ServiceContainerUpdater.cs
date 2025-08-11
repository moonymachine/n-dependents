using System;
using UnityEngine;

public sealed class ServiceContainerUpdater : MonoBehaviour
{
	public event Action Updated;

	private void Update()
	{
		if(Updated != null)
		{
			try
			{
				Updated();
			}
			catch(Exception exception)
			{
				Debug.LogException(exception);
			}
		}
	}
	private void OnDestroy()
	{
		Updated = null;
	}
}
