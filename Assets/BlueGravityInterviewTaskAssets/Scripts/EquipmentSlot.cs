using System;

[Serializable]
public class EquipmentSlot : ItemSlot {

	public EquipmentSlotType SlotType;
	public EquipmentSlot(Item item, EquipmentSlotType slotType)
	{
		Item = item;
		SlotType = slotType;
	}
}
