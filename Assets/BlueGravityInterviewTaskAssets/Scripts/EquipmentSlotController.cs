using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquipmentSlotController : ItemSlotController {

	public EquipmentSlot _equipmentSlot;


	public override void OnDrop(PointerEventData eventData)
	{

		var item = eventData.pointerDrag.GetComponent<ItemController>().Item;
		if (item.ItemSO is not EquipmentSO || (item.ItemSO as EquipmentSO).SlotType != _equipmentSlot.SlotType)
			return;

		base.OnDrop(eventData);
	}
}
