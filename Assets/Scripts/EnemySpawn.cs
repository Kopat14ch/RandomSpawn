using System.Collections;
using UnityEngine;
using Random = System.Random;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoints;
    [SerializeField] private Enemy _enemy;
    [SerializeField] private float _delay;
    [SerializeField] private int _countEnemy;
    
    private Transform[] _points;

    private void OnValidate()
    {
        int minEnemy = 4;
        int minDelay = 1;
        
        if (_countEnemy < minEnemy)
        {
            _countEnemy = minEnemy;
        }

        if (_delay < minDelay)
        {
            _delay = minDelay;
        }
    }

    private void Start()
    {
        _points = new Transform[_spawnPoints.childCount];

        for (int i = 0; i < _spawnPoints.childCount; i++)
        {
            _points[i] = _spawnPoints.GetChild(i);
        }

        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        Random random = new Random();

        for (int i = 0; i < _countEnemy; i++)
        {
            GameObject newObject = Instantiate(_enemy.gameObject, Vector3.zero, Quaternion.identity);
            Transform newObjectTransform = newObject.GetComponent<Transform>();

            newObjectTransform.position = _points[random.Next(_spawnPoints.childCount)].position;
            
            yield return new WaitForSeconds(_delay);
        }
    }
}
