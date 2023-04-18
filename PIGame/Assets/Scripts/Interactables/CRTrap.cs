using UnityEngine;

public class CRTrap : Trap
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private Vector3 move;
    [SerializeField]
    private Rigidbody rb;
    private bool isOpen = false;

    [SerializeField]
    private bool left;
    [SerializeField]
    private bool right;
    [SerializeField]
    private bool up;
    [SerializeField]
    private bool down;

    [SerializeField]
    private GameObject _light;
    void Start()
    {
        rb = transform.GetChild(0).GetComponent<Rigidbody>();
        speed = 10f;
        move = new();
        if (left)
        {
            move.x = -1;
        }
        if (right)
        {
            move.x = 1;
        }
        if (down)
        {
            move.z = -1;
        }
        if (up)
        {
            move.z = 1;
        }
        move *= speed;

        if (_light == null) _light = transform.GetChild(2).gameObject;

    }
    public override void SelectTrap()
    {
        _light.SetActive(true);
    }
    public override void ActivateTrap()
    {
        _light.SetActive(false);

        if (isOpen == false)
        {
            AudioManager.instance.PlaySound(AudioManager.AudioType.Sound, 1);

            rb.velocity = move;
        }

        else if (isOpen == true)
        {
            AudioManager.instance.PlaySound(AudioManager.AudioType.Sound, 0);

            rb.velocity = move * -1;
        }
        isOpen = !isOpen;
    }
}
