using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{

    public float flap = 600f;

    new Rigidbody2D rigidbody2D;
    GameObject gameController;
    GameObject scoreGUI;

    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        gameController = GameObject.Find("GameController");
        scoreGUI = GameObject.Find("ScoreGUI");
    }

    void Start()
    {
        rigidbody2D.isKinematic = true;
    }

    void FixedUpdate()
    {
        if (GameController.isPlaying == true)
        {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, rigidbody2D.velocity.y);
        }
    }

    void Update()
    {
        // スペースキーが押されたら
        if (Input.GetKeyDown("space"))
        {

            // プレイが始まる前だったら
            if (GameController.isPlaying == false)
            {
                // ゲームスタート
                gameController.SendMessage("GameStart");
                rigidbody2D.isKinematic = false;
            }
            // 落下速度をリセット
            rigidbody2D.velocity = Vector2.zero;
            // 上方向へ飛ばす
            rigidbody2D.AddForce(Vector2.up * flap, ForceMode2D.Impulse);
        }
    }

    void Move()
    {
        if (GameController.isPlaying == true)
        {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, rigidbody2D.velocity.y);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        // カウントゾーンに触れたら
        if (col.gameObject.tag == "CountZone")
        {
            // スコアを＋１
            scoreGUI.SendMessage("AddScore", 1);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        // デスに触れたら
        if (col.gameObject.tag == "Death")
        {
            // ゲームオーバー
            gameController.SendMessage("GameOver");
        }
    }
}