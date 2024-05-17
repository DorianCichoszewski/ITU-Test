using System;
using UnityEngine;

namespace StarterAssets
{
	// Modified script from Starter Assets package
	[RequireComponent(typeof(CharacterController))]
	public class ModelController : MonoBehaviour
	{
		[Header("Player")]
		[Tooltip("Move speed of the character in m/s")]
		public float MoveSpeed = 2.0f;

		[Tooltip("How fast the character turns to face movement direction")]
		[Range(0.0f, 0.3f)]
		public float RotationSmoothTime = 0.12f;

		[Tooltip("Acceleration and deceleration")]
		public float SpeedChangeRate = 10.0f;

		[SerializeField]
		private float tolerableDistance = 0.5f;

		// player
		private float _animationBlend;
		private float _targetRotation = 0.0f;
		private float _rotationVelocity;

		// animation IDs
		private int _animIDSpeed;
		private int _animIDGrounded;
		private int _animIDMotionSpeed;

		private Animator _animator;
		private CharacterController _controller;

		private bool _hasAnimator;

		private Vector3 targetPosition;
		private Action targetReachedAction;

		private void Start()
		{
			_hasAnimator = TryGetComponent(out _animator);
			_controller = GetComponent<CharacterController>();

			AssignAnimationIDs();

			_hasAnimator = TryGetComponent(out _animator);
			_animator.SetBool(_animIDGrounded, true);
		}

		private void AssignAnimationIDs()
		{
			_animIDSpeed = Animator.StringToHash("Speed");
			_animIDGrounded = Animator.StringToHash("Grounded");
			_animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
		}

		public void SetTargetPosition(Vector3 targetPosition, Action onTargetReached)
		{
			this.targetPosition = targetPosition;
			targetReachedAction = onTargetReached;
		}

		public void Update()
		{
			if (targetReachedAction == null)
				return;
			
			Vector3 distance = targetPosition - transform.position;
			distance.y = 0;
			Vector2 direction = new Vector2(distance.x, distance.z).normalized;
			
			if (distance.magnitude < tolerableDistance)
			{
				var tmpAction = targetReachedAction;
				targetReachedAction = null;
				tmpAction?.Invoke();
				return;
			}

			_animationBlend = Mathf.Lerp(_animationBlend, MoveSpeed, Time.deltaTime * SpeedChangeRate);
			if (_animationBlend < 0.01f) _animationBlend = 0f;

			// note: Vector2's != operator uses approximation so is not floating point error prone, and is cheaper than magnitude
			// if there is a move input rotate player when the player is moving
			if (direction != Vector2.zero)
			{
				_targetRotation = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
				float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity,
					RotationSmoothTime);
				
				transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
			}

			distance.y = 0;
			
			_controller.Move(distance.normalized * (MoveSpeed * Time.deltaTime));

			// update animator if using character
			if (_hasAnimator)
			{
				_animator.SetFloat(_animIDSpeed, _animationBlend);
				_animator.SetFloat(_animIDMotionSpeed, 1);
			}
		}
	}
}