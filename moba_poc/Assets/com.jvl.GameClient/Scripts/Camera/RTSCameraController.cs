using Cinemachine;
using NaughtyAttributes;
using UnityEngine;

namespace GameClient.Scripts.Camera
{
	public class RTSCameraController : MonoBehaviour
	{
		public enum CameraLockType
		{
			/// <summary>
			/// Free to move the camera around battle field
			/// </summary>
			None,

			/// <summary>
			/// Camera will be lock to the target
			/// </summary>
			LockToTarget,
		}

		//The upper case in this naming have it's own meaning, it's not a mistake in region naming. I wanna separate things only use on editor in code logic

		#region EDITOR VARIABLE
		// The "E" Character before name mean this is the editor variable
		private const string EFoldoutLabelNameGeneral = "General";
		private const string EFoldoutLabelNameEdgeScrolling = "Edge Scrolling";
		#endregion EDITOR VARIABLE

		#region === States ===
		#region General Variable
		[Foldout(EFoldoutLabelNameGeneral)]
		[SerializeField]
		private CinemachineVirtualCamera _vtCamera;

		[Foldout(EFoldoutLabelNameGeneral)]
		[SerializeField]
		private CameraLockType _lockType;

		[Foldout(EFoldoutLabelNameGeneral)]
		[SerializeField]
		private Transform _followTarget;
		#endregion General Variable

		#region Edge Scrolling Variable
		[Foldout(EFoldoutLabelNameEdgeScrolling)]
		[SerializeField]
		private bool _canScroll;

		[Foldout(EFoldoutLabelNameEdgeScrolling)]
		[SerializeField]
		private float _scrollSpeed = 5f;

		[Foldout(EFoldoutLabelNameEdgeScrolling)]
		[SerializeField]
		private float _maxScrollSpeed;

		[Foldout(EFoldoutLabelNameEdgeScrolling)]
		[SerializeField]
		private Vector2 _baseZoomVerticalScrollingLimit = new Vector2(-18, -15);

		[Foldout(EFoldoutLabelNameEdgeScrolling)]
		[SerializeField]
		private Vector2 _baseZoomHorizontalScrollingLimit = new Vector2(-15, 15);
		[Foldout(EFoldoutLabelNameEdgeScrolling)]
		[SerializeField]
		private float _edgeBorderThickness = 10f;

		
		// ========= Acceleration variable ====================//
		[Foldout(EFoldoutLabelNameEdgeScrolling)]
		[Space]
		[SerializeField]
		private bool _useScrollAccelerator;

		[Foldout(EFoldoutLabelNameEdgeScrolling)]
		[ShowIf("_useScrollAccelerator")]
		[SerializeField]
		[Range(1f, 10f)]
		private float _acceleration = 1f;
		#endregion Edge Scrolling Variable

		private Vector3 _defaultCameraToTargetOffset;
		#endregion === States ===

		#region === Accessors ===
		public Transform FollowTarget {
			get => _followTarget;
			set {
				if (!value)
				{
					// When target is null, we should not lock camera to anything
					Debug.Log("The specific target is null, camera can not lock to null object", gameObject);
					_lockType = CameraLockType.None;
				}

				_followTarget = value;
			}
		}

		public CameraLockType LockType {
			get => _lockType;
			set {
				if (value == CameraLockType.LockToTarget && !_followTarget)
				{
					Debug.LogWarning(
						"LockType is set to lock to target, please set target to follow before lock camera to it, Lock type auto set to None now",
						gameObject);
					_lockType = CameraLockType.None;
					return;
				}

				_lockType = value;
			}
		}
		#endregion === Accessors ===

		#region === Event Funtions ===
		private void Update()
		{
			var pos = transform.position;
			var scrollingPosition = CalculateScrollCameraPosition(pos);
			_vtCamera.ForceCameraPosition(scrollingPosition, transform.rotation);
			// transform.position = scrollingPosition;
		}

		private void Reset()
		{
			_vtCamera = GetComponent<CinemachineVirtualCamera>();
		}
		#endregion === Event Funtion ===

		#region === Methods ===
		public void FocusCameraTo(Transform focusTarget)
		{
			FocusCameraTo(focusTarget, Vector3.zero);
		}

		// ReSharper disable once MemberCanBePrivate.Global
		public void FocusCameraTo(Transform focusTarget, Vector3 positionOffset)
		{
			if (!focusTarget)
			{
				Debug.LogWarning("[RTSCameraController.FocusCameraTo] target to focus to is null", gameObject);
				return;
			}

			
			_vtCamera.Follow = focusTarget;
			
			// Free camera 
			LockType = CameraLockType.None;
			var cameraPosition = focusTarget.position + _defaultCameraToTargetOffset + positionOffset;
			_vtCamera.ForceCameraPosition(cameraPosition, transform.rotation);
		}
		#endregion === Methods ===

		#region === Subroutiones ===
		private Vector3 CalculateScrollCameraPosition(Vector3 intPosition)
		{
			var outputPosition = intPosition;
			if (!_canScroll)
			{
				return outputPosition;
			}
			
			var movingOffset = Vector3.zero;
			var mousePosition = Input.mousePosition;
			if (Input.GetKey(KeyCode.W) || mousePosition.y > Screen.height - _edgeBorderThickness)
			{
				movingOffset.z += _scrollSpeed * Time.deltaTime;
			}

			if (Input.GetKey(KeyCode.S) || mousePosition.y <= _edgeBorderThickness )
			{
				movingOffset.z -= _scrollSpeed * Time.deltaTime;
			}

			if (Input.GetKey(KeyCode.A) || mousePosition.x <= _edgeBorderThickness)
			{
				movingOffset.x -= _scrollSpeed * Time.deltaTime;
			}

			if (Input.GetKey(KeyCode.D) || mousePosition.x >= Screen.width + _edgeBorderThickness)
			{
				movingOffset.x += _scrollSpeed * Time.deltaTime;
			}
			
			outputPosition += movingOffset;
			outputPosition.x = Mathf.Clamp(outputPosition.x, _baseZoomHorizontalScrollingLimit.x,
				_baseZoomHorizontalScrollingLimit.y);
			outputPosition.z = Mathf.Clamp(outputPosition.z, _baseZoomVerticalScrollingLimit.x,
				_baseZoomVerticalScrollingLimit.y);
			return outputPosition;
		}

		private bool IsEdgeScrollable()
		{
			var result = false;
			var mousePosition = Input.mousePosition;
			if (mousePosition.y > Screen.height - _edgeBorderThickness ||
			    mousePosition.y <= _edgeBorderThickness ||
			    mousePosition.x <= _edgeBorderThickness ||
			    mousePosition.x >= Screen.width + _edgeBorderThickness)
			{
				result = true;
			}

			return result;
		}
		#endregion === Subroutiones ===
	}
}