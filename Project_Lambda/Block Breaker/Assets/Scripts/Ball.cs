using UnityEngine;

public class Ball : MonoBehaviour
{
    //Config
    [SerializeField] Paddle paddle1;
    [SerializeField] float launchVelocityX = 2f;
    [SerializeField] float launchVelocityY = 15f;
    [SerializeField] AudioClip[] ballSounds;
    [SerializeField] float randomFactor = 2f;


    //State
    Vector2 paddleToBallVector;
    bool hasStarted = false;

    // Cached Component References
    AudioSource myAudioSource;
    Rigidbody2D myRidgid2D;


    // Start is called before the first frame update
    void Start()
    {
        paddleToBallVector = transform.position - paddle1.transform.position;
        myAudioSource = GetComponent<AudioSource>();
        myRidgid2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hasStarted == false)
        {
            LockBallToPaddle();
            LaunchOnMouseClick();
        }
    }

    private void LaunchOnMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            myRidgid2D.velocity = new Vector2(launchVelocityX, launchVelocityY);
            hasStarted = true;
        }
    }

    private void LockBallToPaddle()
    {
        Vector2 paddlePos = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y);
        transform.position = paddlePos + paddleToBallVector;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayBallSounds();
        BallRandomness();
    }

    private void BallRandomness()
    {
        Vector2 velocityTweak = new Vector2(Random.Range(0f, randomFactor), Random.Range(0f, randomFactor));
        myRidgid2D.velocity += velocityTweak;
    }

    private void PlayBallSounds()
    {
        if (hasStarted == true)
        {
            AudioClip clip = ballSounds[Random.Range(0, ballSounds.Length)];
            myAudioSource.PlayOneShot(clip);
        }
    }
}
