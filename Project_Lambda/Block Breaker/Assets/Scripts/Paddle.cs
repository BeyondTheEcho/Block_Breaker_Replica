using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    // Config Parameters
    [SerializeField] float screenWidthInUnits = 16f;
    [SerializeField] float clampMin = 1f;
    [SerializeField] float clampMax = 15f;

    // Cached References
    GameStatus gamestatus;
    Ball ball;


    // Start is called before the first frame update
    void Start()
    {
        gamestatus = FindObjectOfType<GameStatus>();
        ball = FindObjectOfType<Ball>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 paddlePos = new Vector2(transform.position.x,transform.position.y);
        paddlePos.x = Mathf.Clamp(GetXPos(), clampMin, clampMax);
        transform.position = paddlePos;
    }

    private float GetXPos()
    {
        if (gamestatus.IsAutoPlayEnabled() == true)
        {
            return ball.transform.position.x;
        }
        else
        {
            return Input.mousePosition.x / Screen.width * screenWidthInUnits;
        }
    }
}
