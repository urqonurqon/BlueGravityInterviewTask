using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
	public int Space { get; set; }
	public ItemSlotController[] ItemSlotControllers { get; set; }

	public Inventory(int space)
	{
		Space = space;

		ItemSlotControllers = new ItemSlotController[Space];
	}
}
