using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class Pair<T, U> {

	public T X { get; set; }
	public U Y { get; set; }

	public Pair() 
	{}

	public Pair(T first, U second) 
	{
		this.X = first;
		this.Y = second;
	}

}