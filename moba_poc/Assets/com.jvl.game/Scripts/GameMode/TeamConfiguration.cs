using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameClient.Scripts
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