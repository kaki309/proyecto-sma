using UnityEngine;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour
{
    public void ChooseSara()
    {
        GameController.Instace.SetCurrentCharacter(Character.Sara);
    }

    public void ChooseMateo()
    {
        GameController.Instace.SetCurrentCharacter(Character.Mateo);
    }

}