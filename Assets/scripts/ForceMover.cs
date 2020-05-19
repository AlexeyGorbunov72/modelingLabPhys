using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceMover : MonoBehaviour
{

    
    public float speed;
    public Rigidbody ball;
    public float mass;
    public string type;
    private CommunicationItem toSend;
    void Start()
    {
        ball = GetComponent<Rigidbody>();
        toSend.mass = mass;
        toSend.speed = speed;
        toSend.setType(type);

    }

    public void getParamsOfCollision(CommunicationItem item)
    {
        if (item.getType() == "r")
        {
            resilientCollision(item);
            return;
            
        }
        nonResilientCollision(item);
        
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision start!");
        
        other.gameObject.SendMessage("getParamsOfCollision", toSend);
    }

    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ball.velocity = new Vector3(speed, 0, 0);

        }
      
    }

    void resilientCollision(CommunicationItem item)
    {
        float newSpeed = (2 * item.getMass() * item.getSpeed() + (toSend.getMass() - item.getMass())*toSend.getSpeed())/(item.getMass() + toSend.getMass());
        ball.velocity = new Vector3(newSpeed, 0, 0);
        toSend.speed = newSpeed;
    }

    void nonResilientCollision(CommunicationItem item)
    {
        float newSpeed = (item.getMass() * item.getSpeed() + toSend.getMass() * toSend.getSpeed()) /
                         (toSend.getMass() + item.getMass());
        ball.velocity = new Vector3(newSpeed, 0,0);
        toSend.speed = newSpeed;
    }
}

public struct CommunicationItem
{
    public float mass;
    public float speed;
    private string typeOfCollision;
    public float getMass()
    {
        return mass;
    }

    public float getSpeed()
    {
        return speed;
    }

    public void setType(string type)
    {
        typeOfCollision = type;
    }

    public string getType()
    {
        return typeOfCollision;
    }
}
