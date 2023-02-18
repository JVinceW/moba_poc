namespace Com.JVL.Game.GameMode
{
	public interface IGameMode
	{
		BaseGameModeConfiguration GameModeConfig { get; }
		T GameModeConfigByType<T>() where T : BaseGameModeConfiguration;
		
		string GameModeName { get; }
	}
}