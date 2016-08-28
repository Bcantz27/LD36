using UnityEngine;
using System.Collections;

public class CatapultController : MonoBehaviour {

    private Animation anim;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animation>();

        foreach (AnimationState state in anim)
        {
            state.speed = 0.5F;
            Debug.Log(state.name);
        }

    }
	
	// Update is called once per frame
	void Update () {
        Debug.DrawRay(transform.position, transform.right *-1, Color.blue, 1);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(shoot());
        }
    }

    IEnumerator shoot()
    {
        anim.Play("Arm|Shoot");
        Debug.Log(anim["Arm|Shoot"].length * 1.4f);
        yield return new WaitForSeconds(anim["Arm|Shoot"].length *1.4f);
        this.gameObject.GetComponentInChildren<ShootRock>().shoot();
    }

}
