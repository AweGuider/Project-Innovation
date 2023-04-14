using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BlockStack : MonoBehaviour
{
    [SerializeField]
    private GameObject fallingBlock;
    private Rigidbody fallingBlockRb;
    private BoxCollider fallingBlockCollider;
    private MeshRenderer fallingBlockMesh;
    [SerializeField]
    private List<GameObject> blocksToFall;
    private Dictionary<Vector3, Quaternion> blocksTransforms;
    [SerializeField]
    private bool fell;

    private void Start()
    {
        blocksToFall = new List<GameObject>();
        blocksTransforms = new Dictionary<Vector3, Quaternion>();
        if (fallingBlock == null) fallingBlock = transform.GetChild(0).gameObject;

        fallingBlockRb = fallingBlock.GetComponent<Rigidbody>();
        fallingBlockRb.useGravity = false;
        fallingBlockCollider = fallingBlock.GetComponent<BoxCollider>();
        fallingBlockMesh = fallingBlock.GetComponent<MeshRenderer>();


        blocksToFall.Clear();
        foreach (Transform block in transform.GetChild(1).transform)
        {
            block.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            blocksToFall.Add(block.gameObject);
            blocksTransforms.Add(new Vector3(block.position.x, block.position.y, block.position.z), new Quaternion(block.rotation.x, block.rotation.y, block.rotation.z, block.rotation.w));
            Debug.Log($"Starting position of a block: ({block.position.x}, {block.position.y}, {block.position.z})");
            Debug.Log($"Starting rotation of a block: ({block.rotation.x}, {block.rotation.y}, {block.rotation.z})");
        }
    }

    public void OnClickActivate()
    {
        if (fell)
        {
            SetState(true);
            for (int i = 0; i < blocksToFall.Count; i++)
            {
                blocksToFall[i].transform.position = blocksTransforms.ElementAt(i).Key;
                blocksToFall[i].transform.rotation = blocksTransforms.ElementAt(i).Value;
            }
        }
        else
        {
            SetState(false);
        }
    }

    private void SetState(bool s)
    {
        fallingBlockCollider.enabled = s;
        fallingBlockRb.isKinematic = s;
        fallingBlockMesh.enabled = s;
        foreach (GameObject block in blocksToFall)
        {
            block.gameObject.GetComponent<Rigidbody>().isKinematic = s;
        }
        fell = !s;
    }
}
