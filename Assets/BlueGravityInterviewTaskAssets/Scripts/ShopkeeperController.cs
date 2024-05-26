using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class ShopkeeperController : InventoryController
{

	[SerializeField] private int _startingShopGold;
	[SerializeField] private Transform _canvasInventory;
	[SerializeField] private TMP_Text _goldAmountText;
	[SerializeField] private CanvasGroup _inventoryCG;

	public Shopkeeper Shopkeeper;

	private void Awake()
	{
		ShowInventory(false);
		Inventory = new Inventory(_canvasInventory.childCount);
		Shopkeeper = new Shopkeeper(Inventory, _startingShopGold);
		GameController.Shopkeepers.Add(Shopkeeper);
		Shopkeeper.OnGoldAmountChanged += GoldAmountChanged;
		SetStartingItemsInInventory();
	}

	private void GoldAmountChanged(int amount)
	{
		_goldAmountText.text = amount.ToString();

	}

	public override void Init()
	{
		GoldAmountChanged(Shopkeeper.GoldAmount);
		AssignItemSlots(_canvasInventory);

		DestroyItemObjects();
		InstantiateInventoryItems();
	}



	private void ShowInventory(bool show, bool instant = false)
	{
		_inventoryCG.FadeCanvasGroup(show, instant ? 0 : 0.5f);
	}

}
