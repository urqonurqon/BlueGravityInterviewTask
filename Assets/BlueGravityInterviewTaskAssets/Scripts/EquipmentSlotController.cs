using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquipmentSlotController : ItemSlotController {

	public EquipmentSlot _equipmentSlot;
	public SpriteRenderer[] _spriteRenderer;

	[HideInInspector] public Sprite[] _startingSprite;
	private void Awake()
	{
		_startingSprite = new Sprite[_spriteRenderer.Length];
		for (int i = 0; i < _spriteRenderer.Length; i++)
		{
			_startingSprite[i] = _spriteRenderer[i].sprite;
		}
	}

	public override void OnDrop(PointerEventData eventData)
	{

		var itemController = eventData.pointerDrag.GetComponent<ItemController>();
		var item = eventData.pointerDrag.GetComponent<ItemController>().Item;
		if (item.ItemSO is not EquipmentSO || (item.ItemSO as EquipmentSO).SlotType != _equipmentSlot.SlotType)
			return;
		ItemEquiped(itemController, true);

		base.OnDrop(eventData);
	}
}
