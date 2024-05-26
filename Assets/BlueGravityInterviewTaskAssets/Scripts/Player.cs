using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Player {
	public Action<List<Stat>> OnStatsChanged;
	public Action<int> OnGoldAmountChanged;


	public float Speed;
	public List<Stat> Stats
	{
		get
		{
			return _stats;
		}
		set
		{
			_stats = value;
			OnStatsChanged?.Invoke(value);
		}
	}
	private List<Stat> _stats = new List<Stat>();
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
	public Inventory Equipment { get; set; }

	public Player(List<Stat> stats, int goldAmount, float speed)
	{
		Stats = stats;
		GoldAmount = goldAmount;
		Speed = speed;
	}
}
