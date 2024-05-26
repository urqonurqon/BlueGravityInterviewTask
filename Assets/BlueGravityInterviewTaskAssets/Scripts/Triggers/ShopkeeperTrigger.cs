using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopkeeperTrigger : MonoBehaviour {
	[SerializeField] private CanvasGroup _myInventoryCG;
	[SerializeField] private CanvasGroup _shopkeeperInventoryCG;

	private bool _isInTrigger;
	private ShopkeeperController _shopController;
	private void Awake()
	{
		_shopController = GetComponentInParent<ShopkeeperController>();
	}
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.E) && _isInTrigger)
		{
			if (_shopkeeperInventoryCG.alpha == 0)
			{
				_shopController.Init();
				_myInventoryCG.FadeCanvasGroup(true, .5f);
				_shopkeeperInventoryCG.FadeCanvasGroup(true, .5f);
			}
			else
			{
				_myInventoryCG.FadeCanvasGroup(false, .5f);
				_shopkeeperInventoryCG.FadeCanvasGroup(false, .5f);
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			_isInTrigger = true;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			_isInTrigger = false;
			_myInventoryCG.FadeCanvasGroup(false, .5f);
			_shopkeeperInventoryCG.FadeCanvasGroup(false, .5f);
		}
	}


}
