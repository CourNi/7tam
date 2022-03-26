using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{
    [SerializeField]
    private float _positionTolerance;
    [SerializeField]
    private float _speed = 0.75f;
    [SerializeField]
    private float _raycastDistance = 2.0f;

    private Animator _animator;
    private List<int> _possibleDirection = new List<int>();
    private float _timeElapsed;
    private float _lerpDuration;
    private Vector2 _startPos;
    private Vector2 _targetPos;

    #region Grid
    private float _stepX = 1.8f;
    private float _stepY = 1.95f;
    private float _correction = 0.2f;
    #endregion

    public void Damage()
    {
        Destroy(gameObject);
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _startPos = transform.localPosition;
        _targetPos = transform.localPosition;
    }

    private void Update()
    {
        if ((transform.localPosition - (Vector3)_targetPos).magnitude < _positionTolerance)
        {
            _startPos = transform.localPosition;
            GetNewTarget();
            _lerpDuration = (_startPos - _targetPos).magnitude * _speed;
            _timeElapsed = 0.0f;
        }
        if (_timeElapsed < _lerpDuration)
        {
            transform.position = Vector2.Lerp(_startPos, _targetPos, _timeElapsed / _lerpDuration);
            _timeElapsed += Time.deltaTime;
        }
    }

    private void GetNewTarget()
    {
        _possibleDirection.Clear();
        for (int i = 0; i < 4; i++)
        {
            float angle = 90 * i;
            Vector2 direction = Extensions.RotateVector(Vector2.up, angle);
            RaycastHit2D hit = Physics2D.Raycast(transform.localPosition, direction, _raycastDistance);
            if (hit.collider != null)
            {
                if (hit.collider.GetComponent<Player>() != null)
                {
                    GetDirection(i);
                    return;
                }
            }
            else
            {
                _possibleDirection.Add(i);
            }
        }
        GetDirection(_possibleDirection[Random.Range(0, _possibleDirection.Count)]);
    }

    private void GetDirection(int index)
    {
        //need rework
        _animator.SetFloat("State", index);
        switch (index)
        {
            case 0:
                _targetPos.y += _stepY;
                _targetPos.x += _correction;
                Debug.Log("Up " + _targetPos);
                break;
            case 1:
                _targetPos.x -= _stepX;
                Debug.Log("Left " + _targetPos);
                break;
            case 2:
                _targetPos.y -= _stepY;
                _targetPos.x -= _correction;
                Debug.Log("Down " + _targetPos);
                break;
            case 3:
                _targetPos.x += _stepX;
                Debug.Log("Right" + _targetPos);
                break;
        }
    }
}
