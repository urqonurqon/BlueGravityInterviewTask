using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour {


	protected Inventory _inventory;

	[SerializeField] private GameObject _itemPrefab;
	[SerializeField] private List<ItemSO> _startingItems;

	private List<GameObject> _itemGOs = new List<GameObject>();



	public virtual void Awake()
	{


		_inventory = new Inventory(transform.childCount);
		_inventory.ItemSlotControllers = GetComponentsInChildren<ItemSlotController>();
		for (int i = 0; i < _inventory.ItemSlotControllers.Length; i++)
		{
			var itemSlot = _inventory.ItemSlotControllers[i].ItemSlot;
			//itemSlot=new ItemSlot()
		}
		InstantiateStartingItems();
	}




	private void InstantiateStartingItems()
	{
		if (_startingItems.Count > _inventory.Space) return;

		for (int i = 0; i < _startingItems.Count; i++)
		{
			var amount = 1;
			var itemSO = _startingItems[i];
			var itemGO = Instantiate(_itemPrefab, _inventory.ItemSlotControllers[i].transform);
			_itemGOs.Add(itemGO);

			var itemController = itemGO.GetComponent<ItemController>();
			if (itemSO.ID == 0)
			{
				amount = 3;
			}

			var item = new Item(itemSO, amount);

			itemController.Initialize(item);
			itemController.OnStackSplit += StackSplit;
			_inventory.ItemSlotControllers[i].ItemSlot.Item = item;



		}
	}


	private void StackSplit(Item item, ItemSlotController itemSlotController, int amount)
	{
		var itemGO = Instantiate(_itemPrefab, itemSlotController.transform);
		_itemGOs.Add(itemGO);

		var itemController = itemGO.GetComponent<ItemController>();
		//itemController.Item = new Item(item.ItemSO);
		//itemController.Item.StackSize = amount;
		item = new Item(item.ItemSO,amount);
		itemController.Initialize(item);
		itemController.OnStackSplit += StackSplit;
		itemSlotController.ItemSlot.Item = item;

		//slotTransform.GetComponent<ItemSlot>().ItemController = itemController;


	}
}
