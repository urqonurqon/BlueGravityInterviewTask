using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	[SerializeField] private float _speed = 5f;

	private Vector3 _direction;
	private Animator _animator;


	private void Awake()
	{
		_animator = GetComponent<Animator>();
	}

	private void Update()
	{
		_direction = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);

		_animator.SetFloat("Horizontal", _direction.x);
		_animator.SetFloat("Vertical", _direction.y);
		_animator.SetFloat("Speed", _direction.magnitude);
	}

	private void FixedUpdate()
	{
		transform.position += _direction * Time.fixedDeltaTime * _speed;
	}

}
