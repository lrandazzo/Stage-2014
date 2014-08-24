using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Splitter : MonoBehaviour 
{
	public float coeff;
	public float Coeff
	{
		get
		{
			return coeff;
		}
		set
		{
			if(Activate)
			{
				foreach(var c in collided)
				{
					CircleCollider2D cc = c.gameObject.GetComponent<CircleCollider2D>();
					if(cc != null)
					{
						cc.radius /= coeff*value;
					}
					BoxCollider2D bc = c.gameObject.GetComponent<BoxCollider2D>();
					if(bc != null)
					{
						bc.size /= coeff*value;
					}
				}
			}

			coeff = value;
		}
	}
	public bool activate;
	private List<Collider2D> collided = new List<Collider2D>();
	public bool Activate
	{
		get
		{
			return activate;
		}
		set
		{
			if(activate != value)
			{
				if(activate)
				{
					foreach(var c in collided)
					{
						CircleCollider2D cc = c.gameObject.GetComponent<CircleCollider2D>();
						if(cc != null)
						{
							cc.radius /= coeff;
						}
						BoxCollider2D bc = c.gameObject.GetComponent<BoxCollider2D>();
						if(bc != null)
						{
							bc.size /= coeff;
						}
					}
				}
				else
				{
					foreach(var c in collided)
					{
						CircleCollider2D cc = c.gameObject.GetComponent<CircleCollider2D>();
						if(cc != null)
						{
							cc.radius *= coeff;
						}
						BoxCollider2D bc = c.gameObject.GetComponent<BoxCollider2D>();
						if(bc != null)
						{
							bc.size *= coeff;
						}
					}

				}
			}
			activate = value;
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.GetComponent<Splitable>() != null)
		{
			if(Activate)
			{
				CircleCollider2D cc = other.gameObject.GetComponent<CircleCollider2D>();
				if(cc != null)
				{
					cc.radius *= coeff;
				}
				BoxCollider2D bc = other.gameObject.GetComponent<BoxCollider2D>();
				if(bc != null)
				{
					bc.size *= coeff;
				}
			}
			if(!collided.Contains(other))
			{
				collided.Add(other);
			}
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if(collided.Contains(other))
		{
			if(Activate)
			{
				CircleCollider2D cc = other.gameObject.GetComponent<CircleCollider2D>();
				if(cc != null)
				{
					cc.radius /= Coeff;
				}
				BoxCollider2D bc = other.gameObject.GetComponent<BoxCollider2D>();
				if(bc != null)
				{
					bc.size /= Coeff;
				}
			}
			collided.Remove(other);
		}
	}

}
