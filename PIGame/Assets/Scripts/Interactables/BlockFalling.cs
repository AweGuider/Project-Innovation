using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockFalling : MonoBehaviour
{

    public Button ButtonBlocks;

    public Rigidbody rb; 
    public float BlockForce;
    private float BlockXValue;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Button btn = ButtonBlocks.GetComponent<Button>();
        btn.onClick.AddListener(FallOver);
        GetComponent<MeshRenderer>().enabled = false;
    }

  
    public void FallOver()
    {
        //transform.Translate(new Vector3(2,0,0));
        GetComponent<BoxCollider>().enabled = false;
        rb.isKinematic = false;
        rb.useGravity = true;
        GetComponent<MeshRenderer>().enabled= false;
    }
}
