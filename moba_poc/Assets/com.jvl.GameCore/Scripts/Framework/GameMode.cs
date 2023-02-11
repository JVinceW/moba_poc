using System.Collections.Generic;

namespace GameCore.Scripts.Framework
{
	public class GameMode : BaseInfo
	{
		private string _optionString;
		protected GameState GameState;

		public string OptionString {
			get => _optionString;
			set => _optionString = value;
		}

		public GameState GetGameState()
		{
			return GameState;
		}

		public virtual void InitGame(string sceneName, Dictionary<string, string> initOpt, out string errorMess)
		{
			
			errorMess = string.Empty;
		}
		
		public virtual void InitGameState() { }
		
	}
}