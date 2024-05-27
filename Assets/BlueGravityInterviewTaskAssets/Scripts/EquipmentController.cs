
public class EquipmentController : InventoryController
{

	public override void Init()
	{
		Inventory = new Inventory(transform.childCount);
		AssignItemSlots(transform);
	}
}
