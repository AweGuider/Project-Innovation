using UnityEngine;

public class DoorTrap : Trap
{
    [SerializeField]
    private Animator _animator;

    private void Start()
    {
        if (_animator == null) _animator = GetComponent<Animator>();
    }


    public override void SelectTrap()
    {
        // TODO: Some selection action
    }

    public void ActivateFinal()
    {
        if (_animator.GetBool("Open") == false)
        {
            _animator.SetBool("Open", true);
        }
    }
    public override void ActivateTrap()
    {
        if (_animator.GetBool("Open") == false)
        {
            _animator.SetBool("Open", true);
        }
        else
        {
            _animator.SetBool("Open", false);
        }
    }
}
