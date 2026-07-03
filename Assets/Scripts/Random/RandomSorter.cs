using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSorter : Comparer<Object>
	{
		public override int Compare(Object x, Object y)
		{
			return Random.Range(-1, 2);
		}
	}
