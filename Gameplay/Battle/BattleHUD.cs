using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    [SerializeField] Text nameText;
    [SerializeField] Text levelText;
    [SerializeField] HPBar hpBar;

	Gubbe _gubbe;
	
	public void SetData(Gubbe gubbe) 
	{

		_gubbe = gubbe;
		nameText.text = gubbe.Base.Name;
		levelText.text = "Lvl " + _gubbe.Level;
		hpBar.SetHP((float) gubbe.HP/ gubbe.MaxHp);
	}

	public IEnumerator UpdateHP()
	{
		yield return hpBar.SetHPSmooth((float) _gubbe.HP/ _gubbe.MaxHp);

	}
}
