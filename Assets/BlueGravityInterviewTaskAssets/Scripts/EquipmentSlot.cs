using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EquipmentSlot : ItemSlot {

	public EquipmentSlotType SlotType;
	public EquipmentSlot(Item item, EquipmentSlotType slotType)
	{
		Item = item;
		SlotType = slotType;
	}
}
