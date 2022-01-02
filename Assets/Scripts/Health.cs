using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Health : MonoBehaviour,IPhysicObject
{
    [SerializeField] private int updateHealth;


    public void Collision()
    {
        Manager.Get.GameController.Health += updateHealth;      
    }
}
