using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item {
	public Action<int> OnStackSizeChanged;

	public ItemSO ItemSO;
	public int StackSize
	{
		get
		{
			return _stackSize;
		}
		set
		{
			_stackSize = value;
			OnStackSizeChanged?.Invoke(value);
		}
	}
	private int _stackSize;

	public Item(ItemSO itemSO, int stackSize)
	{
		ItemSO = itemSO;
		_stackSize = stackSize;
	}
}
