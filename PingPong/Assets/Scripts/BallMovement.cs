using System.Collections;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    public float initialMovementSpeed;
    public float extraSpeedPerHit;
    public float maxExtraSpeed;
    //private float BALL_DISTANCE_FROM_NET = 12.5f; - IF WE WANT TO BALL START BY ITSELF
    private readonly float BALL_DISTANCE_FROM_NET = 80f;
    private readonly float BALL_Y_POSITION = -4.1f;
    private bool player1First;
    private float movementSpeed;
    private Collider2D leftGoalCollider;
    private Collider2D rightGoalCollider;
    private Collider2D netCollider;
    private Collider2D ballCollider;
    private delegate void RestartPlayersPositionsEventHandler();
    private RestartPlayersPositionsEventHandler restartPlayersPositions;
    public int HitCounter { get; private set; } = 0;
    Rigidbody2D ballRigid;
    
    public BallMovement()
    {
        var randomNumber = new System.Random().Next(0, 1000);
        player1First = randomNumber < 500;
    }
    public BallMovement(bool player1First)
    {
        this.player1First = player1First;
    }

    public void SetIsPlayer1First(bool player1First)
    {
        this.player1First = player1First;
    }

    public IEnumerator RestartBall()
    {
        yield return new WaitForSeconds(1);
        restartPlayersPositions();
        if (gameObject.activeSelf == false)
        {
            gameObject.SetActive(true);
        }
        SetUpCollisions();
        StartCoroutine(StartBall(player1First));
    }

    // Start is called before the first frame update
    void Start()
    {
        ballRigid = gameObject.GetComponent<Rigidbody2D>();
        var goals = GameObject.FindGameObjectsWithTag("Goal");
        leftGoalCollider = goals[0].GetComponent<Collider2D>();
        rightGoalCollider = goals[1].GetComponent<Collider2D>();
        netCollider = GameObject.FindGameObjectWithTag("Net").GetComponent<Collider2D>();
        ballCollider = GetComponent<Collider2D>();
        SetUpCollisions();
        restartPlayersPositions += GameObject.Find("Player1").GetComponent<Player1Script>().RestartPlayerPosition;
        restartPlayersPositions += GameObject.Find("Player2").GetComponent<Player2Script>().RestartPlayerPosition;
        StartCoroutine(StartBall(player1First));
    }

    private void SetUpCollisions()
    {
        Physics2D.IgnoreCollision(ballCollider, leftGoalCollider, true);
        Physics2D.IgnoreCollision(ballCollider, rightGoalCollider, true);
        Physics2D.IgnoreCollision(ballCollider, netCollider, true);
    }

    private void SetBallPosition(bool isPlayer1Starting = true)
    {
        float x = 0;
        if (isPlayer1Starting)
        {
            x = -BALL_DISTANCE_FROM_NET;
        }
        else
        {
            x = BALL_DISTANCE_FROM_NET;
        }
        float y = BALL_Y_POSITION;
        transform.position = new Vector3(x, y, transform.position.z);
    }
    public IEnumerator StartBall(bool isPlayer1Starting = true)
    {
        SetBallPosition(isPlayer1Starting);
        HitCounter = 0;
        yield return new WaitForSeconds(0.05F);
        /*
         * IF WE DON'T WANT TO BALL START BY ITSELF
        if(isPlayer1Starting == true)
        {
            MoveBall(new Vector2(-1, 0));
        }
        else
        {
            MoveBall(new Vector2(1, 0));
        }
        */
    }

    public void IncreaseHitCounter(int number = 1)
    {
        HitCounter += number;
    }

    public void MoveBall(Vector2 direction)
    {
        direction = direction.normalized;
        var newSpeed = initialMovementSpeed + HitCounter * extraSpeedPerHit;
        if(newSpeed <= maxExtraSpeed)
        {
            movementSpeed = newSpeed;
        }

        ballRigid.velocity = direction * movementSpeed;
    }

    public void FreezeBall()
    {
        ballRigid.velocity = Vector2.zero;
    }
}
