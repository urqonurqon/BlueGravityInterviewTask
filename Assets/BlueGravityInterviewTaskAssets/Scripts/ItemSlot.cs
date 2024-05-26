using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot {


	public Item Item { get; set; }
	

	public ItemSlot()
	{
		Item = null;
	}

	public ItemSlot(Item item)
	{
		Item = item;
	}
}
