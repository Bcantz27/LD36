using UnityEngine;
using System.Collections;

public class Brick : MonoBehaviour
{
	public Brick master;
	public bool hit = false;
	public bool firstAttached = false;
	public float timeOutTime = 1.0f;
	private float deathTime;
	public bool groud;
	public bool doNothing = false;
	public float distancey;
	// Use this for initialization
	void Start ()
	{
		deathTime = timeOutTime;
		doNothing = false;
	}

	// Update is called once per frame
	void Update()
	{
		if (Floating())// && firstAttached)
		{
			gameObject.transform.DetachChildren();
			master = null;

			gameObject.GetComponent<Rigidbody>().isKinematic = false;
			gameObject.transform.parent = null;
		}
		if (transform.parent != null)
		{
			distancey = Vector3.Distance(transform.parent.transform.position, transform.position);//Mathf.Round(transform.parent.transform.position.y - transform.position.y);
		}
		groud = IsGrounded();
		if (IsGrounded())
		{
			if (!hit)
			{
				//doNothing = true;
				gameObject.GetComponent<Rigidbody>().isKinematic = true;
			}
		}
		else
		{
			
			if (master != null)
			{

				if (!master.GetComponent<Rigidbody>().isKinematic)
				{
					gameObject.transform.DetachChildren();
					master = null;

					gameObject.GetComponent<Rigidbody>().isKinematic = false;
					gameObject.transform.parent = null;
					//Debug.Log("Seven");
				}
				else if (firstAttached && Mathf.Abs(distancey) > 10)
				{
					gameObject.transform.DetachChildren();
					master = null;

					gameObject.GetComponent<Rigidbody>().isKinematic = false;
					gameObject.transform.parent = null;
					//Debug.Log("Sixth");
				}
				//Debug.Log("NotNUll");
			}
			else if (firstAttached && transform.parent == null)
			{
				gameObject.transform.parent = null;
				gameObject.GetComponent<Rigidbody>().isKinematic = false;
				gameObject.transform.DetachChildren();
				hit = true;
				//Debug.Log("Fifth"+ gameObject.transform.position);
			}
			if (gameObject.GetComponent<Rigidbody>().isKinematic)
			{
				if (GetComponent<Rigidbody>().velocity.magnitude > 0.01f && !hit && firstAttached)
				{
					gameObject.transform.DetachChildren();
					hit = true;

					master = null;
					gameObject.GetComponent<Rigidbody>().isKinematic = false;
					gameObject.transform.parent = null;
					//Debug.Log(GetComponent<Rigidbody>().velocity.magnitude);
					//Debug.Log("Fourth");
				}

				//if (firstAttached && master == null && gameObject.transform.childCount < 2 && !IsGrounded())
				//{
				//	deathTime -= Time.deltaTime;
				//	if (deathTime < 0)
				//	{
				//		gameObject.GetComponent<Rigidbody>().isKinematic = false;
				//		gameObject.transform.parent = null;
				//		Debug.Log("Third");
				//	}
				//}
			}
		}
	}

	void OnCollisionEnter(Collision collision)
	{
		if (!doNothing)
		{
			if (collision.gameObject.tag.Equals("Brick", System.StringComparison.OrdinalIgnoreCase))
			{
				GameObject otherBrick = collision.gameObject;
				if (gameObject.GetComponent<Rigidbody>().velocity.magnitude < 10f && !hit && !firstAttached)
				{
					if (otherBrick.GetComponent<Brick>().master == null  && otherBrick.transform.parent == null && otherBrick.transform.position.y > transform.position.y)
					{
						otherBrick.GetComponent<Brick>().master = this;
						otherBrick.GetComponent<Rigidbody>().isKinematic = true;
						otherBrick.transform.parent = gameObject.transform;
						otherBrick.GetComponent<Brick>().firstAttached = true;
						otherBrick.GetComponent<Brick>().deathTime = timeOutTime;
					}
					else if (otherBrick.transform.position.y < transform.position.y && otherBrick.transform.childCount < 5)
					{
						master = otherBrick.GetComponent<Brick>();
						gameObject.GetComponent<Rigidbody>().isKinematic = true;
						if (otherBrick.transform.parent != null)
						{
							if (Mathf.Abs(Vector3.Distance(otherBrick.transform.parent.transform.position,transform.position) )< 3)
							{
								gameObject.transform.parent = otherBrick.transform.parent.transform;
							}
							else
							{
								gameObject.transform.parent = otherBrick.transform;
							}
						}
						else
						{
							gameObject.transform.parent = otherBrick.transform;
						}
						deathTime = timeOutTime;
						firstAttached = true;
					}
				}
			}
		}
		if (collision.gameObject.tag.Equals("Rock", System.StringComparison.OrdinalIgnoreCase))
		{
			doNothing = false;
			gameObject.transform.DetachChildren();
			hit = true;

			master = null;
			gameObject.GetComponent<Rigidbody>().isKinematic = false;
			gameObject.transform.parent = null;
			//gameObject.GetComponent<Rigidbody>().AddForce(collision.gameObject.transform.forward * 10000);
			//Debug.Log("Second");
		}

		

	}

	void OnTriggerEnter(Collider trigger)
	{
		if (trigger.gameObject.tag.Equals("Buffer", System.StringComparison.OrdinalIgnoreCase))
		{
			gameObject.GetComponent<Rigidbody>().isKinematic = false;
			gameObject.transform.parent = null;
			//Debug.Log("Second");
		}
	}

	float GroundDistance;
	bool IsGrounded()
	{
		RaycastHit info = new RaycastHit();
		Physics.Raycast(transform.position,  -Vector3.up, out info, GroundDistance + 0.13f);
		if (info.transform != null)
		{
			if (info.transform.tag.Equals("Ground"))
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		return Physics.Raycast(transform.position, -Vector3.up, GroundDistance + 0.13f);
	}

	bool Floating()
	{
		RaycastHit info = new RaycastHit();
		Physics.Raycast(transform.position, -Vector3.up, out info, GroundDistance + 1f);
		if (info.transform != null)
		{
			if (info.transform.tag.Equals("Brick") && Vector3.Distance(info.transform.position, transform.position) > 1.0f)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		return Physics.Raycast(transform.position, -Vector3.up, GroundDistance + 1f);
	}
}
