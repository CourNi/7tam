using System.Collections;
using UnityEngine;

public class DirtyBomb : MonoBehaviour, IBomb
{
    [SerializeField]
    private Projectile _projectilePrefab;
    [SerializeField]
    private float _delay;
    private BoxCollider2D _collider;

    private int _layerMask = 1 << 6;
    private float _distance = 1.0f;

    private void Start()
    {
        _collider = GetComponent<BoxCollider2D>();
        StartCoroutine(ExplodeDelay(_delay));
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _collider.isTrigger = false;
    }

    private IEnumerator ExplodeDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Explode();
    }

    public void Explode()
    {
        for (int i = 0; i < 4; i++)
        {
            float angle = 90 * i;
            Vector2 direction = Extensions.RotateVector(Vector2.up, angle);
            RaycastHit2D hit = Physics2D.Raycast(transform.localPosition, direction, _distance, _layerMask, -Mathf.Infinity, Mathf.Infinity);
            if (hit.collider == null)
            {
                if (i % 2 == 0) direction = Extensions.RotateVector(direction, 353);
                Projectile projectile = Instantiate(_projectilePrefab, transform.localPosition, Quaternion.identity, transform.parent);
                projectile.Send(direction, angle);
            }
        }
        
        BombPool.Current.Remove(this);
        Destroy(gameObject);
    }
}
