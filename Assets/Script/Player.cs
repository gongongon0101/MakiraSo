using UnityEngine;
using System.Collections;


public class Player : MonoBehaviour
{

    public float flap = 600f;
    // ジャンプ中はtrue
    public bool jumpFlg = true; 

    new Rigidbody2D rigidbody2D;
    GameObject gameController;
    GameObject scoreGUI;
    GameObject dashEffect;
    GameObject jumpEffect;

    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        gameController = GameObject.Find("GameController");
        scoreGUI = GameObject.Find("ScoreGUI");
        dashEffect = GameObject.Find("dash_effect");
        // ダッシュエフェクトを消す
        dashEffect.SetActive(false);
        jumpEffect = GameObject.Find("jump_effect");
        // ダッシュエフェクトを消す
        jumpEffect.SetActive(false);
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
            // ジャンプフラグを立てる
            jumpFlg = true;
            // ダッシュエフェクトを消す
            dashEffect.SetActive(false);
            // ジャンプエフェクトを表示
            DispJumpEffect();

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

        // 床に触れたら
        if (col.gameObject.tag == "Floor")
        {
            // ジャンプフラグを初期化
            jumpFlg = false;
            // ダッシュエフェクトを表示
            dashEffect.SetActive(true);
        }
    }

    // ジャンプエフェクトを表示
    void DispJumpEffect()
    {
        // ジャンプエフェクトを出す
        jumpEffect.SetActive(true);

        // 1秒後にエフェクトを消す
        StartCoroutine(DelayMethod(0.3f, () =>
        {
            // ジャンプエフェクトを消す
            jumpEffect.SetActive(false);
        }));
    }

    // 渡された処理を指定時間後に実行する
    // waitTime 遅延時間[ミリ秒]
    // action 実行したい処理
    private IEnumerator DelayMethod(float waitTime, System.Action action)
    {
        yield return new WaitForSeconds(waitTime);
        action();
    }
}