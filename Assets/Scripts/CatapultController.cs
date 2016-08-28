using UnityEngine;
using System.Collections;

public class CatapultController : MonoBehaviour {

    private Animation anim;
    private GameObject currentAmmo;
    public GameObject projectilePrefab;
    private GameObject ammoSlot;
    private Rigidbody ammoRigidBody;

    public float turnSpeed = 50f;
    public float lifetimeAfterFire = 10f;
    public float thrust = 8000;
    private Vector3 fireDirection;
    private bool fired = false;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animation>();
        ammoSlot = GameObject.FindGameObjectWithTag("AmmoSlot");

        currentAmmo = GameObject.Instantiate(projectilePrefab);
        currentAmmo.transform.position = ammoSlot.transform.position;
        ammoRigidBody = currentAmmo.GetComponent<Rigidbody>();
        ammoRigidBody.useGravity = false;

        fireDirection = Quaternion.AngleAxis(45, ammoSlot.transform.up) * transform.right;

        //Scale Animation Play Speed
        foreach (AnimationState state in anim)
        {
            state.speed = 0.5F;
        }

    }
	
	// Update is called once per frame
	void Update () {
        if(currentAmmo != null)
            Debug.DrawRay(currentAmmo.transform.position, fireDirection, Color.green, 1);

        if (Input.GetKeyUp(KeyCode.Space))
        {
            StartCoroutine(shoot());
        }

        if (Input.GetKeyUp(KeyCode.R))
        {
            StartCoroutine(reloadArm());
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.RotateAround(transform.position, transform.up, -1*Time.deltaTime * turnSpeed);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.RotateAround(transform.position, transform.up, Time.deltaTime * turnSpeed);
        }

        updateAmmoLoc();
    }

    IEnumerator shoot()
    {
        if (fired == false)
        {
            anim.Play("Arm|Shoot");
            //Debug.Log(anim["Arm|Shoot"].length * 1.4f);
            yield return new WaitForSeconds(anim["Arm|Shoot"].length *1.4f);

            ammoRigidBody.useGravity = true;
            ammoRigidBody.AddForce(fireDirection * thrust);
            currentAmmo.tag = "Rock";
            currentAmmo.AddComponent<SelfDestruct>();
            currentAmmo.GetComponent<SelfDestruct>().setDelay(lifetimeAfterFire);
            fired = true;
        }
    }

    IEnumerator reloadArm()
    {

        if (fired == true)
        {
            anim.Play("Arm|Reload");
            //Debug.Log(anim["Arm|Reload"].length * 1.4f);
            yield return new WaitForSeconds(anim["Arm|Reload"].length);

            currentAmmo = GameObject.Instantiate(projectilePrefab);
            currentAmmo.transform.position = ammoSlot.transform.position;
            ammoRigidBody = currentAmmo.GetComponent<Rigidbody>();
            ammoRigidBody.useGravity = false;
            fired = false;
        }
    }

    private void updateAmmoLoc()
    {
        if (currentAmmo != null) { 
            if (currentAmmo.transform.position != ammoSlot.transform.position && fired == false)
            {
                currentAmmo.transform.position = ammoSlot.transform.position;
            }
        }

        if (fireDirection != Quaternion.AngleAxis(45, ammoSlot.transform.up) * transform.right)
        {
            fireDirection = Quaternion.AngleAxis(45, ammoSlot.transform.up) * transform.right;
        }
    }

}
