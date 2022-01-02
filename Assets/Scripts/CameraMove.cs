using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{

    private Transform target;
    private Vector3 offset;

    private Vector3 updatePosition;

    private float startY;

    public void Enable()
    {
        target = Manager.Get.Player.transform;
        offset = transform.position - target.position;
        startY = transform.position.y;

        Manager.Get.GameController.fixedUpdateEvent.AddListener(Move);
    }

    private void Move()
    {

        updatePosition = Vector3.Slerp(transform.position, target.position + offset, Time.deltaTime * 10);
        updatePosition.Set(updatePosition.x, startY, updatePosition.z);
        transform.position = updatePosition;


    }
}
