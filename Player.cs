using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 9.0f;
    [SerializeField]
    private int _lives = 3;

    [SerializeField]
    private SpawnManager _spawnManager;
    
    [SerializeField]
    private bool _tripleShotActive = false;
    [SerializeField]
    private bool _shieldActive = false;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private float _fireRate = 0.2f;
    private float _nextFire = 0.0f;

    private UIManager _uiManager;
    [SerializeField]
    private int _score;
    [SerializeField]
    private GameObject _laserAudio;
    [SerializeField]
    private GameObject _powerUpAudio;

    [SerializeField]
    private Player2 _player2;


    // Start is called before the first frame update
    void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
       

        if (_spawnManager == null)
        {
            Debug.LogError("Spawn Manager not found");
        }
        // Take current position and assign new position (0,0,0)
        transform.position = new Vector3(5, 0, 0);

    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        if (Input.GetKeyDown(KeyCode.Space) && (Time.time > _nextFire))
        {
            shootLaser();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("TripleShotPower"))
        {
            _powerUpAudio.GetComponent<AudioSource>().Play();
            StopCoroutine(tripleShotDuration());
            _tripleShotActive = true;
            StartCoroutine(tripleShotDuration());
            Destroy(other.gameObject);
        }

        else if (other.CompareTag("SpeedBoostPower"))
        {
            _powerUpAudio.GetComponent<AudioSource>().Play();
            StopCoroutine(speedBoostDuration());
            _speed += 5;
            StartCoroutine(speedBoostDuration());
            Destroy(other.gameObject);
        }

        else if (other.CompareTag("ShieldPower"))
        {
            _powerUpAudio.GetComponent<AudioSource>().Play();
            _shieldActive = true;
            transform.Find("Player_Shield").gameObject.SetActive(true);
            Destroy(other.gameObject);
        }

        else if (other.CompareTag("EnemyLaser"))
        {
            Destroy(other.gameObject);
            damageTaken();
        }
    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 directionInput = new Vector3(horizontalInput, verticalInput, 0); ;

        // Screen Wrapping
        if ((transform.position.x <= -11.3)) transform.position = new Vector3(11.3f, transform.position.y, transform.position.z);
        else if ((transform.position.x >= 11.3)) transform.position = new Vector3(-11.3f, transform.position.y, transform.position.z);
        else if ((transform.position.y <= -4.9)) transform.position = new Vector3(transform.position.x, 5.8f, transform.position.z);
        else if ((transform.position.y >= 6.85)) transform.position = new Vector3(transform.position.x, -3.9f, transform.position.z);

        // Player movement 
        transform.Translate(directionInput * _speed * Time.deltaTime);


    }

    void shootLaser()
    {
            _laserAudio.GetComponent<AudioSource>().Play();
            _nextFire = Time.time + _fireRate;
            if (!_tripleShotActive) Instantiate(_laserPrefab, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
            else
            {
            Instantiate(_laserPrefab, transform.position + new Vector3(0,1,0), Quaternion.identity);
            Instantiate(_laserPrefab, transform.position + new Vector3(-0.63f, -0.2f, 0), Quaternion.identity);
            Instantiate(_laserPrefab, transform.position + new Vector3(0.63f, -0.2f, 0), Quaternion.identity);
            }
            
    }
    
    public void damageTaken()
    {
        if (_shieldActive)
        {
            transform.Find("Player_Shield").gameObject.SetActive(false);
            _shieldActive = false;
        }

        else
        {
            --_lives;
            _uiManager.updateLives();
        }

        if ( _lives < 1 && _player2.getLives() < 1 )
        {
            _spawnManager.onPlayerDeath();
            _uiManager.showGameOverText();
            Destroy(this.gameObject);
        }

        else if (_lives < 1) Destroy(this.gameObject);
    }

    public int getLives()
    {
        return this._lives;
    }

    private IEnumerator tripleShotDuration()
    {
        yield return new WaitForSeconds(5);
        _tripleShotActive = false;
    }

    private IEnumerator speedBoostDuration()
    {
        yield return new WaitForSeconds(10);
        _speed -= 5;
    }

    public void addScore()
    {
        _score += 10;
    }

    public int getScore()
    {
        return _score;
    }
}
