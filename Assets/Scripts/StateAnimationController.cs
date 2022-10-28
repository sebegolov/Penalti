using UnityEngine;

public class StateAnimationController : MonoBehaviour
{
    [SerializeField] private int _state = 0;
    private Animator _animation;

    void Start()
    {
        _animation = GetComponent<Animator>();
    }

    void Update()
    {
        _animation.SetInteger("State", _state);
    }

    public void SetState(int state)
    {
        _state = state;
    }
}
