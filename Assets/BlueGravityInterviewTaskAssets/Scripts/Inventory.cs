public class Inventory
{
	public int Space { get; set; }
	public ItemSlot[] ItemSlots { get; set; }

	public Inventory(int space)
	{
		Space = space;

		ItemSlots = new ItemSlot[Space];
		for (int i = 0; i < Space; i++)
		{
			ItemSlots[i] = new ItemSlot();
		}
	}
}
