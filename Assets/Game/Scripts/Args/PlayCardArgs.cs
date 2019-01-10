using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public  class PlayCardArgs
{
    public int Length;
    public int weight;
    public CharacterType CharacterType;
    public CardType cardType;
    public PlayCardArgs(int Length,int weight,CharacterType characterType, CardType cardType)
    {
        this.Length = Length;
        this.weight = weight;
        this.CharacterType = characterType;
        this.cardType = cardType;
    }

}
