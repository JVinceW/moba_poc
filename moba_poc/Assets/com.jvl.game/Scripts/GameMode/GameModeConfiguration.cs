using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

namespace Com.JVL.Game.GameMode
{
	[CreateAssetMenu(fileName = "XXX_GameMode", menuName = "Project/Create Game Mode Config", order = 0)]
	public class GameModeConfiguration : BaseGameModeConfiguration
	{
		// ReSharper disable once MemberCanBePrivate.Global
		protected const string EGamePlayCategory = "GamePlay";
		
		[Header("Basic Gameplay Config")]
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