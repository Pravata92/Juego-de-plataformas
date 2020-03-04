using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

    public static LevelGenerator sharedInstance;
    public List<LevelBlock> allTheLevelBlocks = new List<LevelBlock>();
    public Transform levelStartPoint;
    public List<LevelBlock> currentBlocks = new List<LevelBlock>();



    public void Awake()
    {
        sharedInstance = this;
    }

    private void Start()
    {
        GenerateInitialLevelBlock();
    }



    public void AddLevelBlock()
    {
        int randomIndex = Random.Range(0, allTheLevelBlocks.Count);
        LevelBlock currentBlock = (LevelBlock)Instantiate(allTheLevelBlocks[randomIndex]);
        currentBlock.transform.SetParent(this.transform, false);
        Vector3 spawnPosition = Vector3.zero;

        if(currentBlocks.Count == 0)
        {
            spawnPosition = levelStartPoint.position;
        }
        else
        {
            spawnPosition = currentBlocks[currentBlocks.Count - 1].exitPiont.position;
        }

        Vector3 correction = new Vector3(spawnPosition.x - currentBlock.startPoint.position.x, spawnPosition.y - currentBlock.startPoint.position.y, 0);


        currentBlock.transform.position = correction;
        currentBlocks.Add(currentBlock);

    }


    public void RemoveOldestLevelBlock()
    {
        LevelBlock oldestBlock = currentBlocks[0];
        currentBlocks.Remove(oldestBlock);
        Destroy(oldestBlock.gameObject);


    }



    public void RemoveAllLevelBlocks()
    {
        while (currentBlocks.Count > 0)
        {
            RemoveOldestLevelBlock();
        }
    }



    public void GenerateInitialLevelBlock()
    {
        for (int i = 0; i < 3; i++)
        {
            AddLevelBlock();
        }


    }

}
