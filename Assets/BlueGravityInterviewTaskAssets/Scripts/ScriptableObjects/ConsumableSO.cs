using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Consumable", menuName = "BlueGravityInterviewTask/Inventory/Consumable")]
[Serializable]
public class ConsumableSO : ItemSO {

	public int MaxStackSize;

	public Stat Stat;
}
