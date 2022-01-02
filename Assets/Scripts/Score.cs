using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Score : MonoBehaviour,IPhysicObject
{
    [SerializeField] private int updateScore;

    public void Collision()
    {
        Manager.Get.GameController.Score += updateScore;
        Destroy(gameObject);
    }

}
