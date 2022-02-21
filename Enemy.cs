using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4.0f;
    private Vector3 movementDirection = new Vector3(0,-1f,0);
    private Animator _animator;
    private UIManager _UIManager;

    [SerializeField]
    private GameObject _laserPrefab;
    private float _fireRate = 0.2f;
    private float _nextFire = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        _UIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _animator = gameObject.GetComponent<Animator>();

        StartCoroutine(laserShotRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
    }

    void CalculateMovement()
    {
        // Screen Wrapping
        if ((transform.position.y <= -4.9)) transform.position = new Vector3(Random.Range(-9.8f, 9.8f), 5.8f, transform.position.z);

        // Enemy movement 
        transform.Translate(movementDirection * _speed * Time.deltaTime);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Laser"))
        {
            StopAllCoroutines();
            Destroy(other.gameObject);
            Destroy(this.GetComponent<Collider2D>());
            _UIManager.addScore();
            triggerDestroyedAnim();
            _speed = 0;
            this.GetComponent<AudioSource>().Play();
            Destroy(this.gameObject,2.3f); 
        }

        else if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                StopAllCoroutines();
                player.damageTaken();
                triggerDestroyedAnim();
                _speed = 0;
                Destroy(this.GetComponent<Collider2D>());
                this.GetComponent<AudioSource>().Play();
                Destroy(this.gameObject,2.3f);
            }
        }

        else if (other.CompareTag("Player2"))
        {
            Player2 player = other.GetComponent<Player2>();

            if (player != null)
            {
                StopAllCoroutines();
                player.damageTaken();
                triggerDestroyedAnim();
                _speed = 0;
                Destroy(this.GetComponent<Collider2D>());
                this.GetComponent<AudioSource>().Play();
                Destroy(this.gameObject, 2.3f);
            }
        }
    }

    private IEnumerator laserShotRoutine()
    {
        yield return new WaitForSeconds(Random.Range(0, 8));
        Instantiate(_laserPrefab, transform.position + new Vector3(0, -1, 0), Quaternion.identity);

    }

    private void triggerDestroyedAnim()
    {
        _animator.SetTrigger("OnEnemyDeath");
    }
}
