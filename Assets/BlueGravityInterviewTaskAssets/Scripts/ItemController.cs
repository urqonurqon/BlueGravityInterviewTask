using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IEndDragHandler, IDragHandler {

	public Action<Item, ItemSlotController, int> OnStackSplit;

	public Item Item { get; set; }

	[HideInInspector] public Transform BeginDragParent;
	[HideInInspector] public bool IsSplittingStack;

	private Image _itemIcon;
	private Transform _canvasTransform;
	private CanvasGroup _cg;
	private TMP_Text _stackSize;
	private int _splittingStackSize;

	private void Awake()
	{
		_itemIcon = GetComponent<Image>();
		_cg = GetComponent<CanvasGroup>();
		_canvasTransform = GetComponentInParent<Canvas>().transform;
		_stackSize = GetComponentInChildren<TMP_Text>();
	}

	public void Initialize(Item item)
	{
		Item = item;
		if (item.ItemSO is ConsumableSO)
		{
			UpdateStackText(item.StackSize);
			item.OnStackSizeChanged += UpdateStackText;

		}
		else
		{
			_stackSize.gameObject.SetActive(false);
		}
		_itemIcon.sprite = item.ItemSO.Sprite;
	}

	private void UpdateStackText(int amount)
	{
		_stackSize.text = amount > 1 ? amount.ToString() : "";
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		if (Item.ItemSO is ConsumableSO)
		{
			if (Input.GetKey(KeyCode.LeftShift) && Item.StackSize > 1)
			{
				IsSplittingStack = true;
				_splittingStackSize = Item.StackSize;
			}
		}

		BeginDragParent = transform.parent;
		transform.SetParent(_canvasTransform);
		_cg.blocksRaycasts = false;


	}

	public void OnDrag(PointerEventData eventData)
	{
		transform.localPosition += (Vector3)eventData.delta;
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		_cg.blocksRaycasts = true;
		Item item;
		ItemSlotController itemSlotController = null;
		//if (eventData.pointerDrag.GetComponent<ItemController>() != null)
		item = eventData.pointerDrag.GetComponent<ItemController>().Item;
		if (eventData.pointerEnter != null && eventData.pointerEnter.GetComponent<ItemSlotController>() != null)
			itemSlotController = eventData.pointerEnter.GetComponent<ItemSlotController>();
		if (eventData.pointerEnter == null || itemSlotController == null || itemSlotController.ItemSlot.Item != item)
		{

			transform.SetParent(BeginDragParent);
			IsSplittingStack = false;
			return;
		}

		if (IsSplittingStack && transform.parent != BeginDragParent)
		{
			OnStackSplit?.Invoke(item, BeginDragParent.GetComponent<ItemSlotController>(), Mathf.CeilToInt((float)_splittingStackSize / 2));

			IsSplittingStack = false;
		}

		//if (eventData.pointerEnter != null && eventData.pointerEnter.GetComponent<ItemSlot>() != null && IsSplittingStack && BeginDragParent != transform.parent)
		//{
		//	IsSplittingStack = false;
		//}
		//else if (IsSplittingStack)
		//{
		//	IsSplittingStack = false;
		//}

		//if (eventData.pointerEnter == null || eventData.pointerEnter.GetComponent<ItemSlot>() == null || eventData.pointerEnter.GetComponent<ItemSlot>().ItemController != eventData.pointerDrag.GetComponent<ItemController>())


	}

	public void OnPointerEnter(PointerEventData eventData)
	{

	}

	public void OnPointerExit(PointerEventData eventData)
	{

	}
}
