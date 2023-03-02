using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Com.JVL.Game.GameMode
{
	[Serializable]
	public class TeamConfiguration
	{
		[SerializeField]
		private string _teamName;

		[SerializeField]
		private int _teamId;

		[SerializeField]
		private int _maxTeamMemberCount;

		public string TeamName => _teamName;

		public int TeamId => _teamId;

		public int MaxTeamMemberCount => _maxTeamMemberCount;
	}
}