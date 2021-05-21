using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Character/Create new Character bro")]
public class CharacterBase : ScriptableObject
{
    [SerializeField] string name;

    [TextArea]
    [SerializeField] string description;
    [SerializeField] Sprite frontSprite;
    [SerializeField] Sprite backSprite;

    [SerializeField] CharacterType type1;
    [SerializeField] CharacterType type2;

    [SerializeField] int maxHP;
    [SerializeField] int attack;
    [SerializeField] int defense;
    [SerializeField] int spAttack;
    [SerializeField] int spDefense;
    [SerializeField] int speed;

    [SerializeField] List<LearnableMove> learnableMoves;
    [SerializeField] List<LearnableMove> quizMoves;


    public CharacterType Type1 {
        get { return type1;}
    }
    public CharacterType Type2 {
        get { return type2;}
    }
    public Sprite FrontSprite {
        get { return frontSprite;}
    }
    public Sprite BackSprite {
        get { return backSprite;}
    }

    public string Name {
        get { return name;}
    }
    public int MaxHP {
        get { return maxHP;}
    }
    public int Attack {
        get { return attack;}
    }
    public int Defense {
        get { return defense;}
    }
    public int SPAttack {
        get { return spAttack;}
    }
    public int SPDefense {
        get { return spDefense;}
    }
    public int Speed {
        get { return speed;}
    }

    public List<LearnableMove> LearnableMoves {
        get { return learnableMoves; }
    }
    public List<LearnableMove> QuizMoves {
        get { return quizMoves; }
    }

}

[System.Serializable]
public class LearnableMove
{
    [SerializeField] MoveBase moveBase;
    [SerializeField] int level;

    public MoveBase Base {
        get { return moveBase;}
    }

    public int Level {
        get { return level; }
    }


}
public enum CharacterType
    {
        None,
        Hardware,
        Math,
        Programming
    }
