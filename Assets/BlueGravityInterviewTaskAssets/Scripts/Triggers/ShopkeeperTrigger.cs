using UnityEngine;

public class ShopkeeperTrigger : MonoBehaviour {
	[SerializeField] private CanvasGroup _myInventoryCG;
	[SerializeField] private CanvasGroup _shopkeeperInventoryCG;
	[SerializeField] private CanvasGroup _interactTextCG;

	private bool _isInTrigger;
	private ShopkeeperController _shopController;
	private void Awake()
	{
		_shopController = GetComponentInParent<ShopkeeperController>();
		_interactTextCG.FadeCanvasGroup(false);
		_shopkeeperInventoryCG.FadeCanvasGroup(false);

	}
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.E) && _isInTrigger)
		{
			if (_shopkeeperInventoryCG.alpha == 0)
			{
				_interactTextCG.FadeCanvasGroup(false, .25f);
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
			_interactTextCG.FadeCanvasGroup(true, .25f);
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			_interactTextCG.FadeCanvasGroup(false, .25f);
			_isInTrigger = false;
			if (_shopkeeperInventoryCG.alpha > 0)
				_myInventoryCG.FadeCanvasGroup(false, .5f);
			_shopkeeperInventoryCG.FadeCanvasGroup(false, .5f);
		}
	}


}
