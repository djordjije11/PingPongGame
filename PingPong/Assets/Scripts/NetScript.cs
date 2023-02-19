using UnityEngine;

public class NetScript : MonoBehaviour
{
    public GameLogic gameLogic;

    private void Start()
    {
        gameLogic = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameLogic>();
    }
}
