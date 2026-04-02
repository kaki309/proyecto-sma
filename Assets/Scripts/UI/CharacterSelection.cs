using UnityEngine;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour
{
    // This method will be called from the buttons
    public void ChooseCharacter(int characterNumber)
    {
        if (characterNumber == 1)
        {
            Debug.Log("you selected character 1");
            // Here you can add different logic for character 1
        }
        else if (characterNumber == 2)
        {
            Debug.Log("you selected character 2");
            // Here you can add different logic for character 2
        }
    }
}