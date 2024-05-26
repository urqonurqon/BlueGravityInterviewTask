using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlotController : MonoBehaviour, IDropHandler {


	public ItemSlot ItemSlot;



	public virtual void OnDrop(PointerEventData eventData)
	{
		var itemController = eventData.pointerDrag.GetComponent<ItemController>();
		if (itemController == null) return;


		if (itemController.BeginDragParent == transform)
		{
			itemController.transform.SetParent(itemController.BeginDragParent);
			itemController.IsSplittingStack = false;
			return;
		}

		int originalStackSize = itemController.Item.StackSize;
		if (transform.childCount == 0)
		{
			if (itemController.IsSplittingStack && itemController.BeginDragParent != transform)
			{
				itemController.Item.StackSize = Mathf.FloorToInt((float)itemController.Item.StackSize / 2);
			}


			if (itemController.BeginDragParent.GetComponent<ItemSlotController>().transform.parent.tag != transform.parent.tag)
			{
				Shopkeeper shopkeeper = null;
				var breakLoop = false;
				for (int i = 0; i < GameController.Shopkeepers.Count; i++)
				{
					shopkeeper = GameController.Shopkeepers[i];
					if (!breakLoop)
					{
						for (int j = 0; j < shopkeeper.Inventory.ItemSlots.Length; j++)
						{
							var shopkeeperItemSlot = shopkeeper.Inventory.ItemSlots[j];
							if (shopkeeperItemSlot.Item == itemController.Item)
							{
								breakLoop = true;
								break;
							}
						}
					}
				}
				int cost = itemController.Item.ItemSO.GoldValue * itemController.Item.StackSize;
				if (transform.parent.tag == "PlayerInventory")
				{
					if (PlayerController.Player.GoldAmount >= cost)
					{
						PlayerController.Player.GoldAmount -= cost;
						shopkeeper.GoldAmount += cost;
					}
					else
					{

						itemController.Item.StackSize = originalStackSize;

						return;
					}
				}
				else
				{
					if (shopkeeper.GoldAmount >= cost)
					{
						shopkeeper.GoldAmount -= cost / 2;
						PlayerController.Player.GoldAmount += cost / 2;
					}
					else
					{

						itemController.Item.StackSize = originalStackSize;

						return;
					}
				}
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
			if (itemController.BeginDragParent.GetComponent<ItemSlotController>().transform.parent.tag != transform.parent.tag)
			{
				Shopkeeper shopkeeper = null;
				var breakLoop = false;
				for (int i = 0; i < GameController.Shopkeepers.Count; i++)
				{
					shopkeeper = GameController.Shopkeepers[i];
					if (!breakLoop)
					{
						for (int j = 0; j < shopkeeper.Inventory.ItemSlots.Length; j++)
						{
							var shopkeeperItemSlot = shopkeeper.Inventory.ItemSlots[j];
							if (shopkeeperItemSlot.Item == itemController.Item)
							{
								breakLoop = true;
								break;
							}
						}
					}
				}
				int cost = itemController.Item.ItemSO.GoldValue * itemController.Item.StackSize;
				if (transform.parent.tag == "PlayerInventory")
				{
					if (PlayerController.Player.GoldAmount >= cost)
					{
						PlayerController.Player.GoldAmount -= cost;
						shopkeeper.GoldAmount += cost;
					}
					else
					{

						itemController.Item.StackSize = originalStackSize;

						return;
					}
				}
				else
				{
					if (shopkeeper.GoldAmount >= cost)
					{
						shopkeeper.GoldAmount -= cost / 2;
						PlayerController.Player.GoldAmount += cost / 2;
					}
					else
					{
						return;
					}
				}
			}
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
