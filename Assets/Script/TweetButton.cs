using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweetButton : MonoBehaviour {
    public string Url;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnClick() {
        GameObject scoreGUI = GameObject.Find("ScoreGUI");
        // スコアを取得
        int score = scoreGUI.GetComponent<Score>().getScore();

        string message = "スコアは" + score + "点でした。 #マキラ走 https://gongongon0101.github.io/game/MakiraSo/index.html";
        Application.OpenURL("http://twitter.com/intent/tweet?text=" + WWW.EscapeURL(message));
        //Application.ExternalEval(string.Format("window.open('{0}','_blank')", "http://twitter.com/intent/tweet?text=" + WWW.EscapeURL(message)));
    }
}
