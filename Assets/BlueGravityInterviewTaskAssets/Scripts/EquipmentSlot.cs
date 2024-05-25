using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EquipmentSlot : ItemSlot {
	public EquipmentSlot(Item item, EquipmentSlotType slotType) : base(item, slotType)
	{
		Item = item;
		SlotType = slotType;
	}
}
