using VContainer;

namespace Com.JVL.Game.Common
{
	/// <summary>
	/// VContainer not support NetworkBehaviour. So this is a hacked way to inject dependencies
	/// How this should work?
	///		When we spawn the target networkBehaviour, we will call this function on the BeforeSpawn delegate
	/// </summary>
	public interface ICustomInjection
	{
		void SetDependencies(IObjectResolver objectResolver);
	}
}