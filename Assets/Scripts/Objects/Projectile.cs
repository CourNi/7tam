using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    private Rigidbody2D _rigidbody;
    private Vector2 _direction;

    private readonly float _timeCoef = 2.5f;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Send(Vector2 direction, float rotZ)
    {
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, rotZ));
        _direction = direction;
        StartCoroutine(DestroyDelay());
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = _direction * _speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<IDamagable>() != null) collision.gameObject.GetComponent<IDamagable>().Damage();
        if (collision.gameObject.GetComponent<Projectile>() == null) Destroy(gameObject);
    }

    private IEnumerator DestroyDelay()
    {
        yield return new WaitForSeconds(_timeCoef / _speed);
        Destroy(gameObject);
    }
}
