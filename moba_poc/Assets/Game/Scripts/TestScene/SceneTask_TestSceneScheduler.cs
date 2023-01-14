using System;
using Com.JVL.Game.Managers.GameSceneManager;
using Cysharp.Threading.Tasks;
using GameCore.Scripts;
using UnityEngine;

namespace GameClient.Scripts.TestScene
{
	public class SceneTask_TestSceneScheduler : BaseSceneTaskScheduler
	{
		public SceneTask_TestSceneScheduler() { }

		public SceneTask_TestSceneScheduler(BaseSceneContext sceneContext)
		{
			SceneContext = sceneContext;
		}

		protected override void InitializeScheduler()
		{
			_taskList.Add(new SomeCallBeforeLoadSceneTaskObject());
		}
	}

	public class SomeCallBeforeLoadSceneTaskObject : BaseTaskObject
	{
		public override async UniTask ProcessTask()
		{
			await UniTask.Delay(TimeSpan.FromSeconds(5));
			Debug.Log("Finished Task when load test scene");
		}
	}

	public class SceneContext_TestScene : BaseSceneContext
	{
		public SceneContext_TestScene()
		{
			SceneName = "TestScene";
			SceneAssetAddress = "Assets/Game/Scenes/GameClient.unity";
		}
	}
}