using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class TempSet<T>
{
	private long currTime = 0;
	private long tmp;
	private Queue<Pair<long,T>> timeset = new Queue<Pair<long, T>>();

	public TempSet(float time)
	{
		tmp = (long)(time*10000000);
	}

	public void Add(T it)
	{
		currTime = DateTime.Now.Ticks;
		timeset.Enqueue(new Pair<long, T>(currTime,it));
	}

	public void UpdateSet()
	{
		currTime = DateTime.Now.Ticks;
		while(timeset.Count > 0 && timeset.Peek().X < currTime - tmp)
			timeset.Dequeue();
	}

	public List<T> GetSet()
	{
		UpdateSet();
		return timeset.ToList().ConvertAll(new System.Converter<Pair<long, T>, T>(a => a.Y));
	}


}
