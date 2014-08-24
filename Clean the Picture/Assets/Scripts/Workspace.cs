using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class Workspace
{
	public float sigma_add = 0.3f;
	public float sigma_jump = 1f;
	public int sample_number = 10000;
	public int burned_samples = 1000;

	private Queue<Pair<float,float>> samples = new Queue<Pair<float,float>>();
	public Queue<Pair<float,float>> Samples{get{return samples;}}
	private List<Pair<float,float>> data = new List<Pair<float,float>>();
	public List<Pair<float,float>> Data{get{return data;}}
	private System.Random rand = new System.Random();



	public Workspace()
	{}

	public Workspace(float s_add, float s_select, int sampl_nb, int burn)
	{
		sigma_add = s_add;
		sigma_jump = s_select;
		sample_number = sampl_nb;
		burned_samples = burn;
	}

	public Workspace(Workspace w)
	{
		sigma_add = w.sigma_add;
		sigma_jump = w.sigma_jump;
		sample_number = w.sample_number;
		burned_samples = w.burned_samples;
		samples = new Queue<Pair<float, float>>(w.Samples);
		data = new List<Pair<float, float>>(w.Data);
	}

	private float distance(Pair<float,float> p1, Pair<float,float> p2)
	{
		return Mathf.Sqrt(Mathf.Pow(p2.X - p1.X,2f) + Mathf.Pow(p2.Y - p1.Y,2f));
	}

	private float f(Pair<float,float> x)
	{
		float s = 0;
		foreach(var v in data)
			s += gauss_add(distance(x,v));
		return s;
	}

	private Pair<float,float> Next(Pair<float,float> x)
	{
		float rn = (float)rand.NextDouble();
		Pair<float,float> y = Q(x);
		if(rn < f(y)/f(x))
			return y;
		else
			return x;
	}

	public void DrawSamples()
	{
		try
		{
			Pair<float,float> x = new Pair<float,float>();
			x = data[rand.Next(data.Count)];

			for(int i = 0; i < sample_number + burned_samples; i++)
			{
				x = Next(x);
				samples.Enqueue(x);
			}

			for(int i = 0; i < burned_samples; i++)
				samples.Dequeue();
		}
		catch
		{}
	}

	public Vector2 Random()
	{
		try
		{
			var e = Next(samples.ToList()[rand.Next(samples.Count)]);
			return new Vector2(e.X,e.Y);
		}
		catch
		{
			return new Vector2();
		}
	}

	public Vector2 Random(int i)
	{
		if(i <= 1)
			return Random();
		else if(i%2 == 0)
			return (Random(i/2) + Random(i/2))/2f;
		else
			return (Random() + (i-1)*Random(i-1))/((float)i);
	}

	public void Add(Pair<float,float> it)
	{
		data.Add(it);
	}

	public void Add(float x, float y)
	{
		data.Add(new Pair<float,float>(x,y));
	}

	private Pair<float,float> Q(Pair<float,float> x)
	{
		// Box-Muller Method
		double u1 = rand.NextDouble();
		double u2 = rand.NextDouble();
		double randStdNormal1 = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2); //random normal(0,1)
		double randStdNormal2 = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Cos(2.0 * Math.PI * u2); //random normal(0,1)
		double r1 = sigma_jump * randStdNormal1; //random normal(0,sigma_select^2)
		double r2 = sigma_jump * randStdNormal2; //random normal(0,sigma_select^2)

		return new Pair<float,float>(x.X + (float)r1, x.Y + (float)r2);
        
    }
    
	private float gauss_add(float x)
	{
		return Mathf.Exp(-(x*x)/(2*sigma_add*sigma_add));
	}
	    
}
