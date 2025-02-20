using UnityEngine;


public class BricksManager : MonoBehaviour
{
    public void gameRestart()
    {
        for (int i = 0; i < transform.childCount; i++)
    {    
        Transform child = transform.GetChild(i);

        // If "question block" is under a "ChildObject" transform
        Transform questionBlock = child.Find("question block");
        if (questionBlock != null)
        {
            questionBlock.GetComponent<QuestionBlockHit>().gameRestart();
        }
        else
        {

        Transform brickBlock = child.Find("BrickBlock");
        if (brickBlock != null)
            {
            brickBlock.GetComponent<BrickHit>().gameRestart();
                
            }    
        }
    }
    }
}
