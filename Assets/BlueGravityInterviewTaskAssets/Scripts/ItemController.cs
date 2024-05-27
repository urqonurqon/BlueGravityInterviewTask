using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemController : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerClickHandler {

	public Action<Item, ItemSlotController, int> OnStackSplit;

	public Item Item { get; set; }

	[HideInInspector] public Transform BeginDragParent;
	[HideInInspector] public bool IsSplittingStack;

	private Image _itemIcon;
	private Canvas _canvas;
	private CanvasGroup _cg;
	private TMP_Text _stackSize;
	private int _splittingStackSize;

	private RectTransform _rt;

	private void Awake()
	{
		_itemIcon = GetComponent<Image>();
		_cg = GetComponent<CanvasGroup>();
		_canvas = GetComponentInParent<Canvas>();
		_stackSize = GetComponentInChildren<TMP_Text>();
		_rt = GetComponent<RectTransform>();
	}

	
	public void OnPointerClick(PointerEventData eventData)
	{
		if (eventData.button==PointerEventData.InputButton.Right)
		{
			if (GetComponentInParent<InventoryController>() != null)
			{
				Item.Use();
				if (Item.StackSize == 0)
					Destroy(gameObject);
			}
		}
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
		transform.SetParent(_canvas.transform);
		_cg.blocksRaycasts = false;


	}

	public void OnDrag(PointerEventData eventData)
	{
		float scaleFactor = _canvas.scaleFactor;

		Vector2 adjustedDelta = eventData.delta / scaleFactor;

		_rt.anchoredPosition += adjustedDelta;
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		_cg.blocksRaycasts = true;
		Item item;
		ItemSlotController itemSlotController = null;
		item = eventData.pointerDrag.GetComponent<ItemController>().Item;
		if (eventData.pointerEnter != null && eventData.pointerEnter.GetComponent<ItemSlotController>() != null)
			itemSlotController = eventData.pointerEnter.GetComponent<ItemSlotController>();
		if (eventData.pointerEnter == null || itemSlotController == null){
			transform.SetParent(BeginDragParent);
		

			IsSplittingStack = false;
			return;
		}
		if  (itemSlotController.ItemSlot.Item != item)
		{

			transform.SetParent(BeginDragParent);
			if (IsSplittingStack && (Mathf.CeilToInt((float)_splittingStackSize / 2) + eventData.pointerEnter.GetComponent<ItemController>().Item.StackSize) <= ((ConsumableSO)Item.ItemSO).MaxStackSize)
			{
				Item.StackSize = Mathf.CeilToInt((float)_splittingStackSize / 2);
			}

			IsSplittingStack = false;
			return;
		}

		if (IsSplittingStack && transform.parent != BeginDragParent)
		{
			OnStackSplit?.Invoke(item, BeginDragParent.GetComponent<ItemSlotController>(), Mathf.CeilToInt((float)_splittingStackSize / 2));

			IsSplittingStack = false;
		}



	}


}
