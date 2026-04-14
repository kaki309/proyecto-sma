using UnityEngine;

public class CharacterSelection : MonoBehaviour
{
    // Methods to call from buttons
    public void ChooseSara() => ChooseCharacter(Character.Sara);
    public void ChooseMateo() => ChooseCharacter(Character.Mateo);
    
    // Private method to communicate with GameController
    private void ChooseCharacter(Character character)
    {
        GameController.Instace.SetCurrentCharacter(character);
    }
}