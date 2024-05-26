using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour {


	public Inventory Inventory;

	[SerializeField] protected GameObject _itemPrefab;
	[SerializeField] protected List<ItemSO> _startingItems;

	//protected List<GameObject> _itemGOs = new List<GameObject>();

	protected ItemSlotController[] _slotControllers;

	public virtual void Init()
	{


		Inventory = new Inventory(transform.childCount);
		AssignItemSlots(transform);

		SetStartingItemsInInventory();
		DestroyItemObjects();
		InstantiateInventoryItems();
	}

	protected void AssignItemSlots(Transform transform)
	{
		_slotControllers = transform.GetComponentsInChildren<ItemSlotController>();
		//ItemSlot[] itemSlots = new ItemSlot[_slotControllers.Length];
		for (int i = 0; i < Inventory.ItemSlots.Length; i++)
		{
			var itemSlot = Inventory.ItemSlots[i];
			_slotControllers[i].ItemSlot = itemSlot;
			//itemSlots[i] = itemSlot;

		}
		//Inventory.ItemSlots = itemSlots;

	}

	protected void SetStartingItemsInInventory()
	{
		for (int i = 0; i < _startingItems.Count; i++)
		{
			var itemSO= _startingItems[i];
			var amount = 1;
			if (itemSO.ID == 0)
			{
				amount = 3;
			}
			var item = new Item(itemSO, amount);
			var itemSlot = Inventory.ItemSlots[i];
			itemSlot.Item = item;
			//_slotControllers[i].ItemSlot = itemSlot;
		}
	}

	protected void DestroyItemObjects()
	{
		for (int i = 0; i < _slotControllers.Length; i++)
		{
			if (_slotControllers[i].GetComponentInChildren<ItemController>() != null)
				Destroy(_slotControllers[i].GetComponentInChildren<ItemController>().gameObject);
		}
	}

	protected virtual void InstantiateInventoryItems()
	{
		
	
		if (_startingItems.Count > Inventory.Space) return;

		for (int i = 0; i < Inventory.ItemSlots.Length; i++)
		{
			var itemSlot= Inventory.ItemSlots[i];
			if (itemSlot.Item == null) continue;
			var itemGO = Instantiate(_itemPrefab, _slotControllers[i].transform);
			//_itemGOs.Add(itemGO);

			var itemController = itemGO.GetComponent<ItemController>();


			itemController.Initialize(itemSlot.Item);
			itemController.OnStackSplit += StackSplit;



		}
	}


	protected void StackSplit(Item item, ItemSlotController itemSlotController, int amount)
	{
		var itemGO = Instantiate(_itemPrefab, itemSlotController.transform);
		//_itemGOs.Add(itemGO);

		var itemController = itemGO.GetComponent<ItemController>();
		//itemController.Item = new Item(item.ItemSO);
		//itemController.Item.StackSize = amount;
		item = new Item(item.ItemSO, amount);
		itemController.Initialize(item);
		itemController.OnStackSplit += StackSplit;
		itemSlotController.ItemSlot.Item = item;

		//slotTransform.GetComponent<ItemSlot>().ItemController = itemController;


	}
}
