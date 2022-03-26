using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField]
    private DirtyBomb _bombPrefab;
    [SerializeField]
    private float _speed = 1.0f;
    [SerializeField]
    private float _angle = 0;

    private Control _inputActions;
    private Rigidbody2D _rigidbody;
    private Animator _animator;

    #region Setup
    private void Awake()
    {
        _inputActions = new Control();
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        _inputActions.Main.Action.performed += ctx => PlaceBomb();
    }

    private void OnDisable()
    {
        _inputActions.Main.Disable();
    }

    private void OnEnable()
    {
        _inputActions.Main.Enable();
    }
    #endregion

    private void FixedUpdate()
    {
        Vector2 input = _inputActions.Main.Movement.ReadValue<Vector2>();

        if (input != Vector2.zero)
        {
            _animator.SetFloat("PosX", input.x);
            _animator.SetFloat("PosY", input.y);
        }
        if (input.y != 0) input = Extensions.RotateVector(input, _angle);
        _rigidbody.velocity = input * _speed;
    }

    private void PlaceBomb()
    {
        if (BombPool.Current.GetCount() < 2)
        {
            DirtyBomb bomb = Instantiate(_bombPrefab, transform.localPosition, Quaternion.identity, transform.parent);
            BombPool.Current.Add(bomb);
        }
    }
}
