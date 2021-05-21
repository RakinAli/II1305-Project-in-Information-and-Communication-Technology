using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUnit : MonoBehaviour
{
    
    [SerializeField] bool isPlayerUnit;

    public Gubbe gubbe {get; set;}

    public void setup(Gubbe gubbe) {
        this.gubbe = gubbe;
        if (isPlayerUnit){
            GetComponent<Image>().sprite = this.gubbe.Base.BackSprite;
        }
        else {
            GetComponent<Image>().sprite = this.gubbe.Base.FrontSprite;
        }
    }   
}
