using UnityEngine;

public class Player : MonoBehaviour, IDamagable
{
    private Vector2 _initialPosition;

    private void Awake()
    {
        _initialPosition = transform.localPosition;
    }

    private void Respawn()
    {
        transform.position = _initialPosition;
        gameObject.SetActive(true);
    }

    public void Damage()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Enemy>() != null) Damage();
    }
}
