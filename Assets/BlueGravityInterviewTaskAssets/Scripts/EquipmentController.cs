using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EquipmentController : InventoryController
{

	public override void Start()
	{
		_inventory = new Inventory(transform.childCount);
		_inventory.ItemSlotControllers = GetComponentsInChildren<ItemSlotController>();
	}
}
