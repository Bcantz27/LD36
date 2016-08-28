using UnityEngine;
using System.Collections;

public class ShootRock : MonoBehaviour {

    public Rigidbody rb;
    public float thrust = 100;
    private Vector3 direction;
    private bool fired = false;
    private GameObject ammoSlot;
    public float x;
    public float y;
    public float z;


    // Use this for initialization
    void Start () {
        ammoSlot = GameObject.FindGameObjectWithTag("AmmoSlot");
        transform.position = ammoSlot.transform.position;
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        direction = Quaternion.Euler(x, y, z) * GameObject.FindGameObjectWithTag("Catapult").transform.right;

    }
	
	// Update is called once per frame
	void Update () {
        updateLoc();
        direction = Quaternion.Euler(x, y, z) * GameObject.FindGameObjectWithTag("Catapult").transform.right;
        Debug.DrawRay(transform.position, rb.velocity, Color.blue, 1);
        Debug.DrawRay(transform.position, direction, Color.green, 1);
    }

    public void shoot()
    {
        if (fired == false) {
            rb.useGravity = true;
            rb.AddForce(direction * thrust);
            fired = true;
        }
    }

    private void updateLoc()
    {
        if (transform.position != ammoSlot.transform.position && fired == false)
        {
            transform.position = ammoSlot.transform.position;
        }
    }
}
