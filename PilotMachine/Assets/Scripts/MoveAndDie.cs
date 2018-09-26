using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAndDie : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameObject.Destroy(this, 1f);
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.position += Vector3.left;
	}

}
