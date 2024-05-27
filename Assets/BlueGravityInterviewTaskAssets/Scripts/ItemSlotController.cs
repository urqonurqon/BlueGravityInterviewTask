using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEditor.Progress;

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
				Shopkeeper shopkeeper = GameController.CurrentShopkeeper;

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
						if (itemController.Item.ItemSO is EquipmentSO && this is EquipmentSlotController)
						{
							ItemEquiped(itemController, false);
						}
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
			if (itemController.IsSplittingStack && itemController.BeginDragParent != transform)
			{
				if (ItemSlot.Item.StackSize + Mathf.FloorToInt((float)itemController.Item.StackSize / 2) <= ((ConsumableSO)ItemSlot.Item.ItemSO).MaxStackSize)
					itemController.Item.StackSize = Mathf.FloorToInt((float)itemController.Item.StackSize / 2);
			}
			if (itemController.BeginDragParent.GetComponent<ItemSlotController>().transform.parent.tag != transform.parent.tag)
			{
				Shopkeeper shopkeeper = GameController.CurrentShopkeeper;

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
				if (!itemController.IsSplittingStack)
				{
					Destroy(itemController.gameObject);

				}

			}

		}
	}


	protected void ChangeSprites(SpriteRenderer[] spriteRenderers, Sprite[] sprites)
	{
		for (int i = 0; i < spriteRenderers.Length; i++)
		{
			spriteRenderers[i].sprite = sprites[i];
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
		if (this is EquipmentSlotController)
		{
			EquipmentSlotController equipmentSlotController = this as EquipmentSlotController;
			ChangeSprites(equipmentSlotController._spriteRenderer, ((EquipmentSO)itemController.Item.ItemSO).WorldSprite);
		}
		else if (!isEquiped)
		{
			EquipmentSlotController equipmentSlotController=itemController.BeginDragParent.GetComponent<EquipmentSlotController>();
			ChangeSprites(equipmentSlotController._spriteRenderer, equipmentSlotController._startingSprite);

		}
	}
}
