using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockFalling : MonoBehaviour
{

    public Button ButtonBlocks;

    public float BlockForce;
    private float BlockXValue;

    // Start is called before the first frame update
    void Start()
    {
        Button btn = ButtonBlocks.GetComponent<Button>();
        btn.onClick.AddListener(FallOver);

        BlockXValue = gameObject.transform.position.x;
    }

  
    void FallOver()
    {
        BlockXValue += BlockForce;
        gameObject.transform.position = new Vector2(BlockXValue, gameObject.transform.position.y);
    }
}
