using UnityEngine;
using System.Collections;

public class Castle : MonoBehaviour
{
	public Transform center;
	public GameObject brick;
	public int wallHeight;
	public int wallLength;
	// Use this for initialization
	void Start ()
	{
		
		float x = center.position.x;
		float y = -brick.transform.localScale.y;
		float z = center.position.z;

		if (wallHeight == 0)
		{
			wallHeight = Random.Range(10, 40);
		}
		if (wallLength == 0)
		{
			wallLength = Random.Range(10,20);
		}

		for(int i = 1; i <= wallHeight; i++)
		{
			GameObject newBlock;
			GameObject newBlock2;
			GameObject newBlock3;
			GameObject newBlock4;

			float height = y + (i*brick.transform.localScale.y);

			if (i % 2 == 0)
			{
				for (int k = 1; k <= wallLength; k++)
				{
					newBlock = Instantiate(brick, new Vector3(x - ((k*brick.transform.localScale.x)-brick.transform.localScale.x/2),
					height, z),
					new Quaternion()) as GameObject;

					newBlock2 = Instantiate(brick, new Vector3(x - brick.transform.localScale.z/2,
					height, (z - ((k * brick.transform.localScale.x) - brick.transform.localScale.x / 2)) + brick.transform.localScale.z / 2),
					 Quaternion.Euler(new Vector3(0, -90, 0))) as GameObject;

					newBlock3 = Instantiate(brick, new Vector3(x - ((k * brick.transform.localScale.x) - brick.transform.localScale.x / 2),
					height, z - (wallLength * (brick.transform.localScale.x))),
					new Quaternion()) as GameObject;

					newBlock4 = Instantiate(brick, new Vector3((x - brick.transform.localScale.z / 2) - (wallLength * (brick.transform.localScale.x)),
					height, (z - ((k * brick.transform.localScale.x) - brick.transform.localScale.x / 2)) + brick.transform.localScale.z / 2),
					 Quaternion.Euler(new Vector3(0, -90, 0))) as GameObject;
				}
				

				//newBlock2 = Instantiate(brick, new Vector3(x + (brick.transform.localScale.x/2),
				//height, z),
				//new Quaternion()) as GameObject;

			}
			else
			{
				for (int k = 1; k <= wallLength; k++)
				{
					newBlock = Instantiate(brick, new Vector3(x - (k*brick.transform.localScale.x),
					height, z),
					new Quaternion()) as GameObject;

					newBlock2 = Instantiate(brick, new Vector3(x - brick.transform.localScale.z/2,
					height, (z - (k * brick.transform.localScale.x)) + brick.transform.localScale.z/2),
					 Quaternion.Euler(new Vector3(0, -90, 0))) as GameObject;

					newBlock3 = Instantiate(brick, new Vector3(x - (k * brick.transform.localScale.x),
					height, z - (wallLength * (brick.transform.localScale.x))),
					new Quaternion()) as GameObject;

					newBlock4 = Instantiate(brick, new Vector3((x - brick.transform.localScale.z / 2)-(wallLength * (brick.transform.localScale.x)),
					height, (z - (k * brick.transform.localScale.x)) + brick.transform.localScale.z / 2),
						Quaternion.Euler(new Vector3(0, -90, 0))) as GameObject;


				}
				

				//newBlock2 = Instantiate(brick, new Vector3(x,
				//	height, z),
				//	new Quaternion()) as GameObject;
				
			}
		}

	}
	
	// Update is called once per frame
	void Update ()
	{
		//Start();
	}
}
