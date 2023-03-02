using Cysharp.Threading.Tasks;

namespace Com.JVL.Game.GameMode
{
	public interface IGameMode
	{
		BaseGameModeConfiguration GameModeConfig { get; }
		T GameModeConfigByType<T>() where T : BaseGameModeConfiguration;

		string GameModeName { get; }

		UniTask StartGame(Fusion.GameMode gameMode);
	}
}