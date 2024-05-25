using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlotController : MonoBehaviour, IDropHandler {


	public ItemSlot ItemSlot;

	private void Awake()
	{
		ItemSlot = new ItemSlot();
	}

	public virtual void OnDrop(PointerEventData eventData)
	{
		var itemController = eventData.pointerDrag.GetComponent<ItemController>();
		if (itemController.BeginDragParent == transform)
		{
			itemController.transform.SetParent(itemController.BeginDragParent);
			itemController.IsSplittingStack = false;
			return;
		}
		if (transform.childCount == 0)
		{
			if (itemController.IsSplittingStack && itemController.BeginDragParent != transform)
			{
				itemController.Item.StackSize = Mathf.FloorToInt((float)itemController.Item.StackSize / 2);
			}
			//	else if (itemController.IsSplittingStack)
			//	{
			//		itemController.IsSplittingStack = false;
			//	}

			eventData.pointerDrag.transform.SetParent(transform);
			eventData.pointerDrag.transform.localPosition = Vector3.zero;

			ItemSlot.Item = itemController.Item;
		}
		else if (itemController.Item.ItemSO == ItemSlot.Item.ItemSO && itemController.Item.ItemSO is ConsumableSO)
		{
			var consumable = itemController.Item.ItemSO as ConsumableSO;
			if (consumable.MaxStackSize >= itemController.Item.StackSize + ItemSlot.Item.StackSize)
			{
				ItemSlot.Item.StackSize += itemController.Item.StackSize;
				Destroy(itemController.gameObject);

			}

		}
	}
}
