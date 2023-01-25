using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (MovementController.GetIsMoving)
        {
            _animator.SetFloat("Speed" , 1);
        }
        else
        {
            _animator.SetFloat("Speed" , 0);
        }
    }
}
