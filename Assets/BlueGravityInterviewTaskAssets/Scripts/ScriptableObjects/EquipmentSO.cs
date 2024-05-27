using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Equipment", menuName = "BlueGravityInterviewTask/Inventory/Equipment")]
[Serializable]
public class EquipmentSO : ItemSO
{
	public List<Stat> Stats;
	public EquipmentSlotType SlotType;
	public Sprite[] WorldSprite;
}
