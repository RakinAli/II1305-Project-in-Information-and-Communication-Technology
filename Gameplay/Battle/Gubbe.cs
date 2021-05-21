using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Gubbe
{
	[SerializeField] CharacterBase _base;
	[SerializeField] int level;

    public CharacterBase Base {
		get {
			return _base;
		}
	}
	public int Level {
		get {
			return level;
		}
	}

	public int HP { get; set; }

	public bool IsBeat { get; set; }
    public List<Move> Moves { get; set; }
	public List<Move> Answers { get; set; }

	public void Init()
	{
		HP = MaxHp;
        Moves = new List<Move>();
        foreach(var move in Base.LearnableMoves) {
            if(move.Level <= Level)
                Moves.Add(new Move(move.Base));

            // if(Moves.Count >= 5) //change if we want more than 4 moves
            //     break;
        }

		Answers = new List<Move>();
        foreach(var move in Base.QuizMoves) {
            if(move.Level <= Level)
                Answers.Add(new Move(move.Base));

           // if(Answers.Count >= 4) //change if we want more than 4 moves
             //   break;
        }

	}
	public CharacterType Type1 {
        get { return Base.Type1;}
    }
    public CharacterType Type2 {
        get { return Base.Type2;}
    }
	public int Attack {
		get { return Base.Attack; }
	}

	public int MaxHp {
		get {
			return Base.MaxHP;
		}
	}
	public string Name {
		get {
			return Base.Name;
		}
	}
	public int Defense {
		get {
			return Base.Defense;
		}
	}
	 public int SPAttack {
        get { return Base.SPAttack;}
    }
    public int SPDefense {
        get { return Base.SPDefense;}
    }
    public int Speed {
        get { return Base.Speed;}
	}


	// This method was pokemon formula, will change later
	public bool TakeDamage(Move move, Gubbe attacker)
	{
		float modifiers = Random.Range(0.85f, 1f);
		// float a = (2 * attacker.Level + 10) / 250f;
		float d = (100 / 250f) * move.Base.Power * ((float)attacker.Attack / Defense);
		int damage = Mathf.FloorToInt(d * modifiers);

		HP -= damage;
		if (HP <= 0)
		{
			HP = 0;
			return true;
		}

		return false;

	}


	// Will Change this later
	public Move GetNextQuestion(int i)
	{
		return Moves[i];
	}

}
