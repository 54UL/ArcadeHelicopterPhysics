using UnityEngine;
using System.Collections;

public class lk : MonoBehaviour {

    public GameObject Target;

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        gameObject.GetComponent<Transform>().LookAt(Target.transform.position);
	}
}
