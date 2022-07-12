using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class recipesScript : MonoBehaviour
{
    public GameObject[] recipeSlots;
    public GameObject hammerRecipe;
    public GameObject bulletRecipe;
    public GameObject charcoalRecipe;
    public GameObject emptyBulletRecipe;
    public GameObject craftingRecipes;
    public GameObject bluePotionRecipe;
    public GameObject redPotionRecipe;
    public GameObject greenPotionRecipe;
    public GameObject stickBundleRecipe;
    public GameObject campfireRecipe;
    public bool hammerRec;
    public bool bulletRec;
    public bool charcoalRec;
    public bool emptyBulletRec;
    public bool bluePotionRec;
    public bool redPotionRec;
    public bool greenPotionRec;
    public bool stickBundleRec;
    public bool campfireRec;
    // Start is called before the first frame update
    void Start()
    {
        recipeSlots = GameObject.FindGameObjectsWithTag("blank");
        craftingRecipes.SetActive(false);
    }

    public void recipeUpdate()
    {
        GameObject recipeCraft = null;
        if(hammerRec)
        {
            hammerRec = false;
            recipeCraft = hammerRecipe;
            hammerRecipe = null;
        }
        else if(bulletRec)
        {
            bulletRec = false;
            recipeCraft = bulletRecipe;
            bulletRecipe = null;
        }
        else if (charcoalRec)
        {
            charcoalRec = false;
            recipeCraft = charcoalRecipe;
            charcoalRecipe = null;
        }
        else if(emptyBulletRec)
        {
            emptyBulletRec = false;
            recipeCraft = emptyBulletRecipe;
            emptyBulletRecipe = null;
        }
        else if (bluePotionRec)
        {
            bluePotionRec = false;
            recipeCraft = bluePotionRecipe;
            bluePotionRecipe = null;
        }
        else if (redPotionRec)
        {
            redPotionRec = false;
            recipeCraft = redPotionRecipe;
            redPotionRecipe = null;
        }
        else if (greenPotionRec)
        {
            greenPotionRec = false;
            recipeCraft = greenPotionRecipe;
            greenPotionRecipe = null;
        }
        else if (stickBundleRec)
        {
            stickBundleRec = false;
            recipeCraft = stickBundleRecipe;
            stickBundleRecipe = null;
        }
        else if (campfireRec)
        {
            campfireRec = false;
            recipeCraft = campfireRecipe;
            campfireRecipe = null;
        }
        if (recipeCraft != null)
        {
            for (int i = 0; i < recipeSlots.Length; i++)
            {
                if (recipeSlots[i].gameObject.tag == "blank")
                {
                    Transform slotPos = recipeSlots[i].transform;
                    Destroy(recipeSlots[i].gameObject.transform.GetChild(0).gameObject);
                    //Debug.Log(recipeCraft);
                    recipeSlots[i] = Instantiate(recipeCraft, slotPos, false);
                    slotPos = null;
                    break;
                }
            }
        }
    }

   
}
