using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SuperficialExplosionGenerator : MonoBehaviour
{
    //Declarations
    [SerializeField] private bool _isExploding = false;
    [SerializeField] private GameObject _minorExplosionPrefab;
    [SerializeField] private GameObject _majorExplosionPrefab;
    [SerializeField] private string _explosionContainerName = "Explosion Container";
    private Transform _explosionContainer;
    [SerializeField] private float _maxDuration = 3;
    [SerializeField] private float _currentDuration = 0;
    [SerializeField] private float _delayBetweenMinorExplosionsMin = .2f;
    [SerializeField] private float _delayBetweenMinorExplosionsMax = .5f;

    [Header("Minor Explosion Settings")]
    [SerializeField] private float _minorExplosionForceMag = .1f;
    [SerializeField] private float _minorExplosionRadius = .15f;
    [SerializeField] private int _minorExplosionParticles = 8;
    [SerializeField] private float _minorExplosionDuration = .15f;
    [SerializeField] private float _minPositionX;
    [SerializeField] private float _maxPositionX;
    [SerializeField] private float _minPositionY;
    [SerializeField] private float _maxPositionY;

    [Header("Maxor Explosion Settings")]
    [SerializeField] private float _majorExplosionForceMag = 1f;
    [SerializeField] private float _majorExplosionRadius = 1f;
    [SerializeField] private int _majorExplosionParticles = 20;
    [SerializeField] private float _majorExplosionDuration = .25f;
    [SerializeField] private int _majorExplosionDamage = 1;

    [Header("Events")]
    public UnityEvent OnExplosionsCompleted;


    //Monobehaviors
    private void Awake()
    {
        _explosionContainer = GameObject.Find(_explosionContainerName).transform;
    }

    private void Update()
    {
        CountDurationIfExploding();
    }



    //Utilities
    private void CreateMinorExplosion(Vector3 position)
    {
        GameObject explosion = Instantiate(_minorExplosionPrefab, transform);

        explosion.GetComponent<ExplodeBehavior>().SetDamage(0);
        explosion.GetComponent<ExplodeBehavior>().SetForceMagnitude(_minorExplosionForceMag);
        explosion.GetComponent<ExplodeBehavior>().SetRadius(_minorExplosionRadius);

        explosion.GetComponent<ExplosionVFXController>().SetVFXRadius(_minorExplosionRadius);
        explosion.GetComponent<ExplosionVFXController>().SetVFXParticleSpawnRate(_minorExplosionParticles);
        explosion.GetComponent<ExplosionVFXController>().SetVFXDuration(_minorExplosionDuration);

        explosion.GetComponent<ExplodeBehavior>().Explode();

    }

    private void CreateMinorExplosionAtRandomPosition()
    {
        CreateMinorExplosion(new Vector3(Random.Range(_minPositionX, _maxPositionX), Random.Range(_minPositionY, _maxPositionY), 0));
    }

    private void CreateMajorExplosion()
    {
        GameObject explosion = Instantiate(_majorExplosionPrefab, transform.position, Quaternion.identity, _explosionContainer);
        explosion.GetComponent<ExplodeBehavior>().SetDamage(_majorExplosionDamage);
        explosion.GetComponent<ExplodeBehavior>().SetForceMagnitude(_majorExplosionForceMag);
        explosion.GetComponent<ExplodeBehavior>().SetRadius(_majorExplosionRadius);

        explosion.GetComponent<ExplosionVFXController>().SetVFXRadius(_majorExplosionRadius);
        explosion.GetComponent<ExplosionVFXController>().SetVFXParticleSpawnRate(_majorExplosionParticles);
        explosion.GetComponent<ExplosionVFXController>().SetVFXDuration(_majorExplosionDuration);

        explosion.GetComponent<ExplodeBehavior>().Explode();
    }

    private IEnumerator ExplodeUntilDurationIsReached()
    {
        while (_currentDuration < _maxDuration)
        {
            CreateMinorExplosionAtRandomPosition();
            yield return new WaitForSeconds(Random.Range(_delayBetweenMinorExplosionsMin,_delayBetweenMinorExplosionsMax));
        }

        CreateMajorExplosion();
        yield return new WaitForSeconds(.1f);
        OnExplosionsCompleted?.Invoke();
    }

    private void CountDurationIfExploding()
    {
        if (_isExploding)
        {
            _currentDuration += Time.deltaTime;

            if (_currentDuration >= _maxDuration)
            {
                _isExploding = false;
                OnExplosionsCompleted?.Invoke();
            }
        }
    }


    //External Control Utils
    public void EnterExplosionSequence()
    {
        _isExploding = true;
        StartCoroutine(ExplodeUntilDurationIsReached());
    }

    public void EnterExplosionSequence(int damage)
    {
        EnterExplosionSequence();
    }


}
