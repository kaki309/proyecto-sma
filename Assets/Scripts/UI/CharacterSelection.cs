using System.Collections;
using UnityEngine;

public class CharacterSelection : MonoBehaviour
{
    [SerializeField] GameObject preventInputPanel;

    public void ChooseSara() => StartCoroutine(ConfirmSelection(Character.Sara));
    public void ChooseMateo() => StartCoroutine(ConfirmSelection(Character.Mateo));

    IEnumerator ConfirmSelection(Character character)
    {
        preventInputPanel.SetActive(true);
        yield return new WaitForSecondsRealtime(3.5f);
        GameController.Instace.SetCurrentCharacter(character);
    }
}