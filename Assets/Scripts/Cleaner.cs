using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cleaner : MonoBehaviour
{
    private bool IsViewed;

    private void OnBecameInvisible()
    {
        if (IsViewed)
        {
            Destroy(transform.root.gameObject,2);
        }
    }

    private void OnBecameVisible()
    {
        IsViewed = true;
    }


}
