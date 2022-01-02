using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody rb;

    public Rigidbody Rb { get => rb;  }
    public float Speed { get => speed; set => speed = value; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }



    private void OnTriggerEnter(Collider other)
    {
        IPhysicObject physicObject = other.GetComponent<IPhysicObject>();

        if(physicObject!=null)
        {
            physicObject.Collision();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        IPhysicObject physicObject = collision.gameObject.GetComponent<IPhysicObject>();

        if (physicObject != null)
        {
            physicObject.Collision();
        }
    }
}
