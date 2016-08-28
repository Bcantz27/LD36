using UnityEngine;
using System.Collections;

public class Destory : MonoBehaviour
{
	public float time;
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (time < 0)
		{
			Destroy(gameObject);
		}
		else
		{
			time -= Time.deltaTime;
		}
	}
}
