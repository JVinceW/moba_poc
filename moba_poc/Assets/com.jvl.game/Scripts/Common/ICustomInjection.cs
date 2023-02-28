using VContainer.Unity;

namespace Com.JVL.Game.Common
{
	/// <summary>
	/// VContainer not support NetworkBehaviour. So this is a hacked way to set dependencies for the target class
	/// </summary>
	public interface ICustomInjection
	{
		void SetDependencies(LifetimeScope currentScope);
	}
}