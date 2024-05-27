using System;

public class Shopkeeper {
	public Action<int> OnGoldAmountChanged;

	public int GoldAmount
	{
		get
		{
			return _goldAmount;
		}
		set
		{
			_goldAmount = value;
			OnGoldAmountChanged?.Invoke(value);
		}
	}
	private int _goldAmount;

	public Inventory Inventory { get; set; }

	public Shopkeeper(Inventory inventory, int goldAmount)
	{
		Inventory = inventory;
		_goldAmount = goldAmount;
	}
}
