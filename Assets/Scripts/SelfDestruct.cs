using UnityEngine;
using System.Collections;

public class SelfDestruct : MonoBehaviour {

    //Destory object after secs
    public float destroyAfter = 20f;

    public SelfDestruct(float delay)
    {
        destroyAfter = delay;
    }

	// Use this for initialization
	void Start () {
        StartCoroutine(WaitAndDestroy());
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator WaitAndDestroy()
    {
        yield return new WaitForSeconds(destroyAfter);
        //Debug.Log("Destroy");
        Destroy(this.gameObject);
    }

    public void setDelay(float delay)
    {
        destroyAfter = delay;
    }
}
