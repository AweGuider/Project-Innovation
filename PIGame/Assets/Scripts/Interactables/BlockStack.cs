using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockStack : MonoBehaviour
{
    [SerializeField] private GameObject fallingBlock;
    private Rigidbody fallingBlockRb;
    private BoxCollider fallingBlockCollider;
    private MeshRenderer fallingBlockMesh;
    [SerializeField] private List<GameObject> blocksToFall = new List<GameObject>();

    private void Start()
    {
        blocksToFall.Clear();
        fallingBlockRb = fallingBlock.GetComponent<Rigidbody>();
        fallingBlockCollider = fallingBlock.GetComponent<BoxCollider>();
        fallingBlockMesh= fallingBlock.GetComponent<MeshRenderer>();
        //blocksToFall.Add(transform.GetChild(1).gameObject);
        foreach (Transform block in transform.GetChild(1).transform)
        {
            blocksToFall.Add(block.gameObject);
        }
    }

    public void FallOver()
    {
        //transform.Translate(new Vector3(2,0,0));
        fallingBlockCollider.enabled = false;
        fallingBlockRb.isKinematic = false;
        fallingBlockRb.useGravity = false;
        fallingBlockMesh.enabled = false;

        //foreach (var block in blocksToFall)
        //{
        //    block.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        //}

        foreach (GameObject block in blocksToFall)
        {
            block.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        }
    }
}
