using UnityEngine;
public class BricksManager : MonoBehaviour
{

    public void Awake()
    {
        GameManager.instance.gameRestart.AddListener(gameRestart);
    }
    public void gameRestart()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);

            // If "question block" is under a "ChildObject" transform
            Transform questionBlock = child.Find("question block");
            if (questionBlock != null)
            {
                QuestionBlockHit questionBlockHit = questionBlock.GetComponent<QuestionBlockHit>();
                if (questionBlockHit != null)
                {
                    questionBlockHit.gameRestart();
                }
                else
                {
                    QuestionBoxPowerupController questionBoxPowerupController = questionBlock.GetComponent<QuestionBoxPowerupController>();
                    if (questionBoxPowerupController != null)
                    {
                        questionBoxPowerupController.gameRestart();
                    }
                }
            }
            else
            {

                Transform brickBlock = child.Find("BrickBlock");
                if (brickBlock != null)
                {
                    BrickHit brickHit = brickBlock.GetComponent<BrickHit>();
                    if (brickHit != null)
                    {
                        brickHit.gameRestart();
                    }
                    else
                    {
                        BrickPowerupController brickPowerupController = brickBlock.GetComponent<BrickPowerupController>();
                        if (brickPowerupController != null)
                        {
                            brickPowerupController.gameRestart();
                        }
                    }
                }
            }
        }
    }
}