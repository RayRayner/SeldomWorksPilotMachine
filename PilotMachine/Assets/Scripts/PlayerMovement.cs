using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    private Rigidbody rb;

    public int speed;

    public Transform[] interactableObjects;

    public bool Left, Right, Drive, PlayerMove;

    public Object pew;

    public float[] dist;

    public GameObject structure;

    public Vector3 leftAngle = new Vector3(0, 0, 6);
    public Vector3 rightAngle = new Vector3(0,0,-6);


    // Use this for initialization
    void Awake () {
        rb = GetComponent<Rigidbody>();
        PlayerMove = true;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (PlayerMove)
        {
                float moveHorizontal = Input.GetAxis("Horizontal");
                float moveVertical = Input.GetAxis("Vertical");
                if (Input.GetAxisRaw("Horizontal") > 0 || Input.GetAxisRaw("Horizontal") < 0 || Input.GetAxisRaw("Vertical") > 0 || Input.GetAxisRaw("Vertical") < 0)
                {
                    rb.isKinematic = false;
                    Left = false;
                    Right = false;
                }

                Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

                rb.AddForce(movement * speed);
        }
        
    }

    private void Update()
    {
        //Working out what you're next to
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Drive)
            {
                Drive = false;
                PlayerMove = true;
            }
            for (int i = 0; i < interactableObjects.Length; i++)
            {
                dist[i] = Vector3.Distance(this.transform.position, interactableObjects[i].transform.position);
                if (dist[i] <= 5f)
                {
                    rb.isKinematic = true;
                    if (i == 0)
                    {
                        Left = true;
                    }
                    if (i == 1)
                    {
                        Right = true;
                    }
                    if (i == 2)
                    {
                        Debug.Log("Driving");
                        Drive = true;
                    }
                }
            }          

        }
        //Rotating Weapons
        if (Input.GetKey(KeyCode.UpArrow) && Left)
        {
            interactableObjects[0].Rotate(new Vector3(0.0f, 0.0f, 1));
        }
        if (Input.GetKey(KeyCode.DownArrow) && Left)
        {
            interactableObjects[0].Rotate(new Vector3(0.0f, 0.0f, -1));
        }
        if (Input.GetKey(KeyCode.UpArrow) && Right)
        {
            interactableObjects[1].Rotate(new Vector3(0.0f, 0.0f, 1));
        }
        if (Input.GetKey(KeyCode.DownArrow) && Right)
        {
            interactableObjects[1].Rotate(new Vector3(0.0f, 0.0f, -1));
        }
        //Shooting
        if (Input.GetKeyDown(KeyCode.Space) && Left)
        {
            Instantiate(pew, new Vector3(interactableObjects[0].position.x + 2, 0.0f, interactableObjects[0].position.x + 2), Quaternion.identity);
        }
        if (Input.GetKeyDown(KeyCode.Space) && Right)
        {
            Instantiate(pew, new Vector3(interactableObjects[1].position.x + 2, 0.0f, interactableObjects[1].position.x + 2), Quaternion.identity);
        }
        //Driving the ship
        if (Drive)
        {
            this.transform.parent = structure.transform;
            PlayerMove = false;
            if (Input.GetKey(KeyCode.W))
            {
                structure.transform.Translate(0, 0.5f, 0);
            }
            if (Input.GetKey(KeyCode.S))
            {
                structure.transform.Translate(0, -0.5f, 0);
            }
            if (Input.GetKey(KeyCode.D))
            {
                structure.transform.Translate(0.5f, 0, 0);
                //if (structure.transform.eulerAngles.z >= -7)
                //{
                structure.transform.eulerAngles = new Vector3(0, 0, Mathf.LerpAngle(structure.transform.rotation.z, -6, 1f));
                //structure.transform.Rotate = new Vector3.Lerp(0, 0, -6);
                //}

            }
            if (Input.GetKey(KeyCode.A))
            {
                structure.transform.Translate(-0.5f, 0, 0);
                //if (structure.transform.eulerAngles.z <= 7)
                //{
                structure.transform.eulerAngles = (new Vector3(0, 0, 6));
                //}

            }

            if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
            {
                structure.transform.eulerAngles = new Vector3(0, 0, 0);
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                Drive = false;
                rb.isKinematic = false;
                this.transform.parent = null;
                PlayerMove = true;
            }
        }
    }
}
