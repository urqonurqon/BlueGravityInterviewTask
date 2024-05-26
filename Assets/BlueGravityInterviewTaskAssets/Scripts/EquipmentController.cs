using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EquipmentController : InventoryController
{

	public override void Init()
	{
		Inventory = new Inventory(transform.childCount);
		AssignItemSlots(transform);
	}
}
