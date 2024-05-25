using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {


	public Player Player;


	[SerializeField] private CanvasGroup _inventoryCG;

	private Vector3 _direction;
	private Animator _animator;

	private bool _inventoryActive;
	

	private void Awake()
	{
		_animator = GetComponent<Animator>();

		ShowInventory(false, true);
	}

	private void ShowInventory(bool show, bool instant = false)
	{
		_inventoryCG.FadeCanvasGroup(show, instant ? 0 : 0.5f);
	}

	private void Update()
	{
		_direction = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);

		_animator.SetFloat("Horizontal", _direction.x);
		_animator.SetFloat("Vertical", _direction.y);
		_animator.SetFloat("Speed", _direction.magnitude);

		if (Input.GetKeyDown(KeyCode.Tab))
		{
			_inventoryActive = !_inventoryActive;
			ShowInventory(_inventoryActive);
		}
	}

	private void FixedUpdate()
	{
		transform.position += _direction * Time.fixedDeltaTime * Player.Speed;
	}

}
