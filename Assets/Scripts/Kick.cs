using UnityEngine;

public class Kick : MonoBehaviour
{
    [SerializeField] private int _force = 1000;
    private Rigidbody _rigidbody;
    private Vector3 _startPosition;
    private Quaternion _startRotation;
    private Vector3 _side = new Vector3();
    
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _startPosition = transform.position;
        _startRotation = transform.rotation;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            KickBall();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ResetParameters();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        
        if (other.gameObject.tag == "Leg")
        {
            KickBall();
        }
    }

    public void KickBall()
    {
        Vector3 result = Vector3.Lerp(transform.forward, transform.up, 0.2f);
        result = Vector3.Lerp(result, _side, 0.08f);
        _rigidbody.AddForce(result * _force);
    }

    public void SetSide(Side side)
    {
        switch (side)
        {
            case Side.Left: _side = -transform.right; break;
            case Side.Right: _side = transform.right; break;
        }
    }

    public void ResetParameters()
    {
        _rigidbody.Sleep();
        transform.position = _startPosition;
        transform.rotation = _startRotation;
    }
}

public enum Side
{
    Left, Right
}