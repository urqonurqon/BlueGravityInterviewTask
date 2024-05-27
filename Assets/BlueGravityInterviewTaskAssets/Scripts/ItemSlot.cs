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
