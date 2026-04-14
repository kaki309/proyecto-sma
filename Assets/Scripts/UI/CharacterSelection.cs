using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelection : MonoBehaviour
{
    [SerializeField] List<GameObject> disableAfterConfirm;
    [SerializeField] GameObject cheersTextBox;

    public void ChooseSara() => StartCoroutine(ConfirmSelection(Character.Sara));
    public void ChooseMateo() => StartCoroutine(ConfirmSelection(Character.Mateo));

    IEnumerator ConfirmSelection(Character character)
    {
        foreach (GameObject obj in disableAfterConfirm)
        {
            obj.SetActive(false);
        }
        cheersTextBox.SetActive(true);
        yield return new WaitForSecondsRealtime(3.5f);
        GameController.Instace.SetCurrentCharacter(character);
    }
}