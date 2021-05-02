using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class highscore_script : MonoBehaviour
{
    public Text highScoreText;
    public int initialgamescore;
    // Start is called before the first frame update
    void Start()
    {
        highScoreText.text = PlayerPrefs.GetInt("High Score", 0).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.Score > initialgamescore)
        {
            initialgamescore = GameManager.Instance.Score;
            PlayerPrefs.SetInt("High Score", initialgamescore);
            highScoreText.text = initialgamescore.ToString();
        }
    }
}
