using System.Collections.Generic;
using Com.JVL.Game.GameMode;
using NaughtyAttributes;
using UnityEngine;

namespace GameClient.Scripts
{
	[CreateAssetMenu(fileName = "XXX_GameMode", menuName = "Project/Create Game Mode Config", order = 0)]

	public class GameModeConfiguration : BaseGameModeConfiguration
	{
		protected const string EGamePlayCategory = "GamePlay";
		
		[Foldout(EGamePlayCategory)]
		[SerializeField]
		private int _maxPlayers = 10;

		[Foldout(EGamePlayCategory)]
		[SerializeField]
		private int _playablePlayerCount = 10;

		[Foldout(EGamePlayCategory)]
		[SerializeField]
		private int _maxTeamCount = 2;

		[Foldout(EGamePlayCategory)]
		[SerializeField]
		private int _playerPerTeamCount = 5;
		
		[Foldout(EGamePlayCategory)]
		[SerializeField]
		private List<TeamConfiguration> _teamConfigurations = new List<TeamConfiguration>();

		public List<TeamConfiguration> TeamConfigurations => _teamConfigurations;
		
		public int MaxTeamCount => _maxTeamCount;

		public int PlayerPerTeamCount => _playerPerTeamCount;

		public int MaxPlayers => _maxPlayers;

		public int PlayablePlayerCount => _playablePlayerCount;
	}
}