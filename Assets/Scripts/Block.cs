using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [Range(0,0.8f)]
    [SerializeField] private float popularity;

    [SerializeField] private GameObject[] nextBlocks;


    private PointEnd pointEnd;
    private PointStart pointStart;



    private PointEnd PointEnd
    {
        get
        {
            if (pointEnd == null)
            {
                pointEnd = GetComponentInChildren<PointEnd>();
                return pointEnd;
            }
            else
            {
                return pointEnd;
            }
        }
    }
    private PointStart PointStart
    {
        get
        {
            if (pointStart == null)
            {
                pointStart = GetComponentInChildren<PointStart>();
                return pointStart;
            }
            else
            {
                return pointStart;
            }
        }
    }

    public GameObject[] NextBlocks { get => nextBlocks;  }

    public Vector3 GetEndBlockPosition() => PointEnd.transform.position;
    public Vector3 GetStartBlockPosition()=> PointStart.transform.position;

    public bool IWantLive()
    {
        return Random.value <= popularity;
    }

    public void SetBlockPosition(Vector3 position)
    {
        transform.position = position + PointStart.transform.localPosition;     
    }

    public void AddCleaner()
    {
        GetComponentInChildren<MeshRenderer>().gameObject.AddComponent<Cleaner>();
    }






    [ContextMenu("GeneratePoints")]
    private void GeneratePoints()
    {
        GameObject pointStartObj = new GameObject();
        pointStartObj.name = "Point Start";
        pointStartObj.AddComponent<PointStart>();
        pointStartObj.transform.parent = transform;
        pointStartObj.transform.position = Vector3.zero;

        GameObject pointEndObj = new GameObject();
        pointEndObj.name = "Point End";
        pointEndObj.AddComponent<PointEnd>();
        pointEndObj.transform.parent = transform;
        pointEndObj.transform.position = Vector3.zero;
    }




}
