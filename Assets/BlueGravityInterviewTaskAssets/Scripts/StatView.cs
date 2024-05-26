using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatView : MonoBehaviour
{
	[SerializeField] private TMP_Text _title;
	[SerializeField] private TMP_Text _amount;
	public StatType Stat;

	private void Start()
	{
		_title.text=Stat.ToString();
		PlayerController.Player.OnStatsChanged += UpdateStat;
		UpdateStat(PlayerController.Player.Stats);
	}

	public void UpdateStat(List<Stat> list)
	{
		for (int i = 0; i < list.Count; i++)
		{
			if (list[i].Type == Stat)
			{
				_amount.text = list[i].Amount.ToString();
			}
		}
	}

	
}
