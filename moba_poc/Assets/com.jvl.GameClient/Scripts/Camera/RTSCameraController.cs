using System;
using Cinemachine;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Serialization;

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

		// ========= Acceleration variable ==================== //
		[Foldout(EFoldoutLabelNameEdgeScrolling)]
		[Space]
		[SerializeField]
		private bool _useScrollAccelerator;

		[Foldout(EFoldoutLabelNameEdgeScrolling)]
		[ShowIf("_useScrollAccelerator")]
		[SerializeField]
		[Range(1f, 10f)]
		private float _accelerationRate = 1f;
		[Foldout(EFoldoutLabelNameEdgeScrolling)]
		[ShowIf("_useScrollAccelerator")]
		[SerializeField]
		[Range(1f, 10f)]
		[Tooltip("Threshold time before start accelerate camera movement")]
		private float _accelerateThreshold = 1f;
		#endregion Edge Scrolling Variable

		// ======== Local variable ======== //
		private Vector3 _defaultCameraToTargetOffset;
		private float _currentSpeed;
		private float _acceleratorThresholdTimeCounter;
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
			AccelerateSpeed();
			var scrollingOffset = CalculateScrollingOffset();
			
			// Adjust camera limit before set position
			var pos = transform.position;
			pos += scrollingOffset;
			pos.x = Mathf.Clamp(pos.x, _baseZoomHorizontalScrollingLimit.x, _baseZoomHorizontalScrollingLimit.y);
			pos.z = Mathf.Clamp(pos.z, _baseZoomVerticalScrollingLimit.x, _baseZoomVerticalScrollingLimit.y);
			_vtCamera.ForceCameraPosition(pos, transform.rotation);
		}

		private void Reset()
		{
			_vtCamera = GetComponent<CinemachineVirtualCamera>();
		}
		#endregion === Event Funtion ===

		#region === Methods ===
		public void FocusCameraTo(Vector3 position, Quaternion rotation)
		{
			LockType = CameraLockType.None;
			var vCamPos = position + _defaultCameraToTargetOffset;
			_vtCamera.ForceCameraPosition(vCamPos, rotation);
		}

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
		private Vector3 CalculateScrollingOffset()
		{
			if (!_canScroll)
			{
				return Vector3.zero;
			}

			var movingOffset = Vector3.zero;
			var mousePosition = Input.mousePosition;
			if (mousePosition.y > Screen.height - _edgeBorderThickness)
			{
				movingOffset.z += _currentSpeed * Time.deltaTime;
			}

			if (mousePosition.y <= _edgeBorderThickness)
			{
				movingOffset.z -= _currentSpeed * Time.deltaTime;
			}

			if (mousePosition.x <= _edgeBorderThickness)
			{
				movingOffset.x -= _currentSpeed * Time.deltaTime;
			}

			if (mousePosition.x >= Screen.width + _edgeBorderThickness)
			{
				movingOffset.x += _currentSpeed * Time.deltaTime;
			}

			if (!(movingOffset.sqrMagnitude > 0))
			{
				// Reset accelerator if not scrolling anymore
				ResetScrollingAccelerator();
			}
			
			return movingOffset;
		}

		private void AccelerateSpeed()
		{
			if (_useScrollAccelerator)
			{
				_currentSpeed += _accelerationRate * Time.deltaTime;
				_currentSpeed = Mathf.Clamp(_currentSpeed, _scrollSpeed, _maxScrollSpeed);
				_acceleratorThresholdTimeCounter += Time.deltaTime;
				_acceleratorThresholdTimeCounter =
					Mathf.Clamp(_acceleratorThresholdTimeCounter, 0, _accelerateThreshold);
			} else
			{
				ResetScrollingAccelerator();
			}
		}

		private bool IsReachScrollingLimit()
		{
			var pos = transform.position;
			if (pos.x >= _baseZoomHorizontalScrollingLimit.y
			    || pos.x <= _baseZoomHorizontalScrollingLimit.x
			    || pos.z >= _baseZoomVerticalScrollingLimit.y
			    || pos.z <= _baseZoomVerticalScrollingLimit.x)
			{
				return true;
			} ;
			return false;
		}

		private void ResetScrollingAccelerator()
		{
			_currentSpeed = _scrollSpeed;
			_acceleratorThresholdTimeCounter = 0;
		}
		#endregion === Subroutiones ===
	}
}