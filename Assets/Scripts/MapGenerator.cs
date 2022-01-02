using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private GameObject firstBuildingBlock;
    [SerializeField] private GameObject waterPrefab;
    [SerializeField] private int startBlockNumber;
    [SerializeField] private int distanceBuildBlock;
    [SerializeField] private int distanceBuildWater;
    private Block lastBlock;
    private Block lastWaterBlock;



    private void Start()
    {
        Initilaize();

    }


    private void Initilaize()
    {

        Block newBlock = Instantiate(firstBuildingBlock).GetComponent<Block>();
        newBlock.SetBlockPosition(Vector3.zero);
        newBlock.AddCleaner();
        lastBlock = newBlock;

        Block newWaterBlock = Instantiate(waterPrefab).GetComponent<Block>();
        newWaterBlock.AddCleaner();
        lastWaterBlock = newWaterBlock;


        for (int i = 0; i < startBlockNumber - 1; i++)
        {
            GenerateNextBlock();
        }


    }

    public void Enable()
    {
        Manager.Get.GameController.updateEvent.AddListener(Manager.Get.MapGenerator.CheckNeedBuildBlock);
        Manager.Get.GameController.updateEvent.AddListener(Manager.Get.MapGenerator.CheckNeedBuildWater);
    }

    //public void CheckNeedBuildBlock()
    //{
    //    if (Manager.Get.Player == null) return;

    //    float distance = lastBlock.GetStartBlockPosition().z - Manager.Get.Player.transform.position.z;
    //    distance = Mathf.Abs(distance);

    //    if (distance < distanceBuildBlock)
    //    {
    //        GenerateNextBlock();
    //    }
    //}

    public void CheckNeedBuildBlock() => CheckDistance(lastBlock.GetStartBlockPosition(), distanceBuildBlock, GenerateNextBlock);
    public void CheckNeedBuildWater() => CheckDistance(lastWaterBlock.GetEndBlockPosition(), distanceBuildWater, GenerateWaterBlock);

    private void CheckDistance(Vector3 point, float limit, UnityAction action)
    {
        if (Manager.Get.Player == null) return;

        float distance = point.z - Manager.Get.Player.transform.position.z;
        distance = Mathf.Abs(distance);

        if (distance < limit)
        {
            action.Invoke();
        }
    }

    private void GenerateWaterBlock()
    {
        Vector3 endBlockPosition = lastWaterBlock.GetEndBlockPosition();

        Block newBlock = Instantiate(waterPrefab).GetComponent<Block>();

        newBlock.SetBlockPosition(endBlockPosition);
        lastWaterBlock.AddCleaner();
        lastWaterBlock = newBlock;

    }


    private void GenerateNextBlock()
    {
        Vector3 endBlockPosition = lastBlock.GetEndBlockPosition();


        GameObject newBlockObject = SwitchBlock();
        Block newBlock = Instantiate(newBlockObject).GetComponent<Block>();

        newBlock.SetBlockPosition(endBlockPosition);
        lastBlock.AddCleaner();
        lastBlock = newBlock;


    }


    private GameObject SwitchBlock()
    {
        GameObject[] blocks = lastBlock.NextBlocks;

        foreach (var block in blocks)
        {
            if (block.GetComponent<Block>().IWantLive()) return block;
        }

        return blocks[0];

    }


}
