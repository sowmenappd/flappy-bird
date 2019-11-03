using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeScorePoint : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col){
        if(col.tag == "Player"){
            ScoreController.Instance.UpdateScore();
            AudioManager.Instance.Play("point");
        }
    }
}
