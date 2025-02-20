using UnityEngine;

// public class BricksManager : MonoBehaviour
// {
//     public void gameRestart()
//     {
//         for (int i = 0; i < transform.childCount; i++)
//         {
//             Transform child = transform.GetChild(i);

//             // Find the "question block" inside this child
//             Transform questionBlock = child.Find("question block");


//             var brickHit = questionBlock.GetComponent<BrickHit>();
//             if (brickHit != null)
//             {
//                 brickHit.gameRestart();
//             }
//             else
//             {
//                 var questionBlockHit = questionBlock.GetComponent<QuestionBlockHit>();
//                 if (questionBlockHit != null)
//                 {
//                     questionBlockHit.gameRestart();
//                 }
//             }

//         }

//     }
// }


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