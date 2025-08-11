using AbstractServiceLocator;

namespace nDependents
{
	public interface IServiceContainer : IServiceLocator
	{
		void ShutDown();
	}
}
