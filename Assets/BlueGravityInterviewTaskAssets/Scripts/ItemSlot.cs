using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot {

	public EquipmentSlotType SlotType;

	public Item Item { get; set; }
	

	public ItemSlot()
	{
		Item = null;
	}

	public ItemSlot(Item item, EquipmentSlotType slotType)
	{
		Item = item;
		SlotType = slotType;
	}

	public ItemSlot(Item item)
	{
		Item = item;
	}
}
