using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Com.JVL.Game.GameMode
{
	[Serializable]
	public class TeamConfiguration
	{
		[FormerlySerializedAs("_defaultTeamName")]
		[SerializeField]
		private string TeamName;

		public string DefaultTeamName {
			get => TeamName;
			set => TeamName = value;
		}
	}
}