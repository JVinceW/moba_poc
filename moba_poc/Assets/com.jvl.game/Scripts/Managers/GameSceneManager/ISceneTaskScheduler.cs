namespace Com.JVL.Game.Managers.GameSceneManager
{
	public interface ISceneTaskScheduler
	{
		BaseSceneContext SceneContext { get; set; }
		public ISceneEntryPoint SceneEntryPoint { get; set; }
	}
}