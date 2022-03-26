using UnityEngine;

public class SceneInitializer : MonoBehaviour
{
    [SerializeField]
    private GameObject _stonePrefab;
    [SerializeField]
    private Vector2 _basePosition;
    [SerializeField]
    private int _rows;
    [SerializeField]
    private int _columns;

    private float _marginX = 1.8f;
    private float _columnMargin = -1.9f;
    private float _rowMargin = -0.2f;

    private void Start()
    {
        BombPool.Initialize();

        for (int k=0; k < _rows; k++)
        {
            Vector2 currentPosition = _basePosition;
            for (int l=0; l < _columns; l++)
            {
                Instantiate(_stonePrefab, currentPosition, Quaternion.identity, transform);
                currentPosition += new Vector2(_marginX, 0);
            }
            _basePosition += new Vector2(_rowMargin, _columnMargin);
        }
    }
}
