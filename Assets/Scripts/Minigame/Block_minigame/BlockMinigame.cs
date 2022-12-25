using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMinigame : MonoBehaviour
{
    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    [SerializeField] private float minY;
    [SerializeField] private float maxY;
    [SerializeField] private float minZ;
    [SerializeField] private float maxZ;


    private Rigidbody rb;
    private Transform player;
    private bool canCheck = true;


    //private bool isMoving = false; 
    //private Vector3 position;
    //[SerializeField] float moveSpeed;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        //position = transform.position;
    }

    void Update()
    {
        if (rb.velocity != Vector3.zero && canCheck)
        {
            moveBlockPosition();
        }

        /*
        if (isMoving)
        {
            moveBlock();
        }
        */

    }

    void moveBlockPosition()
    {
        //isMoving = true;
        canCheck = false;
        float x = Random.Range(minX, maxX);
        float y = Random.Range(minY, maxY);
        float z = Random.Range(minZ, maxZ);

        Vector3 position = new Vector3(x, y, z);
        transform.position = position;
        rb.velocity = Vector3.zero;
        canCheck = true;
    }

    void moveBlock()
    {
        //if (transform.position == position)
        //{
        //    isMoving = false;
        //}

        // transform.position = Vector3.MoveTowards(transform.position, position, moveSpeed * Time.deltaTime);

    }
}
