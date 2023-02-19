using UnityEngine;

public class CollisionController : MonoBehaviour
{
    public BallMovement ballMovement;
    public GameLogic gameLogic;

    void BounceFromRacket(Collision2D collision)
    {
        var ballPosition = transform.position;
        var playerPosition = collision.gameObject.transform.position;
        var collisionHeight = collision.collider.bounds.size.y;

        float x = ballPosition.x > playerPosition.x ? 1 : -1;

        var y = (ballPosition.y - playerPosition.y) / collisionHeight;

        ballMovement.IncreaseHitCounter();
        ballMovement.MoveBall(new Vector2(x, y));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var collisionObject = collision.gameObject;
        if (collisionObject.tag == "Player")
        {
            BounceFromRacket(collision);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var collisionObject = collision.gameObject;
        if (collisionObject.name == "LeftWallTrigger")
        {
            gameLogic.Player2Scores();
        }
        else if (collisionObject.name == "RightWallTrigger")
        {
            gameLogic.Player1Scores();
        }
        else if (collisionObject.name == "Outline")
        {
            ballMovement.RestartBall();
        }
    }
}
