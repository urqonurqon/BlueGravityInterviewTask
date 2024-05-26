using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEditor.Progress;

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
			itemController.BeginDragParent.GetComponent<ItemSlotController>().ItemSlot.Item = null;
			if (itemController.BeginDragParent.GetComponent<EquipmentSlotController>() != null)
			{
				ItemEquiped(itemController, false);
			}
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
	protected void ItemEquiped(ItemController itemController, bool isEquiped)
	{
		if (isEquiped)
		{
			if (ItemSlot.Item != null)
				return;
		}

		var equipment = itemController.Item.ItemSO as EquipmentSO;
		for (int i = 0; i < equipment.Stats.Count; i++)
		{
			var playerStats = new List<Stat>(PlayerController.Player.Stats);
			var playerStat = playerStats.First(s => s.Type == equipment.Stats[i].Type);
			playerStat.Amount += isEquiped ? equipment.Stats[i].Amount : -equipment.Stats[i].Amount;
			PlayerController.Player.Stats = playerStats;

		}
	}
}
