using System;
using System.Collections.Generic;
using System.Linq;

public class Item  {
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

	public void Use()
	{
		if (ItemSO is ConsumableSO && _stackSize > 0)
		{
			StackSize--;
			var playerStats = new List<Stat>(PlayerController.Player.Stats);

			playerStats.First(s => s.Type == ((ConsumableSO)ItemSO).Stat.Type).Amount += ((ConsumableSO)ItemSO).Stat.Amount;
			PlayerController.Player.Stats = playerStats;
		}
	}
}
