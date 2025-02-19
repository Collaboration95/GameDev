using UnityEngine;

public class BricksManager : MonoBehaviour
{
    public void gameRestart()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);

            // Find the "question block" inside this child
            Transform questionBlock = child.Find("question block");

            questionBlock.GetComponent<QuestionBlockHit>().gameRestart();
        }
    }
}
