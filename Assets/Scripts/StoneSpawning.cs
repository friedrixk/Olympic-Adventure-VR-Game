using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneSpawning : MonoBehaviour
{
    [SerializeField] private GameObject stonePrefab;
    [SerializeField] private GameObject stoneParent;

    [SerializeField] private Selector selector;

    [SerializeField] private Selector[] selectorsForStones = new Selector[] { };

    private int time = 0;

    private bool createdAndStillHeld = false;

    bool IsGesturePerformed()
    {
        // check if it's a combinedselector, then only allow hand, not controller
        // leave the other option in for cheat selector
        if (selector is CombinedSelector)
        {
            CombinedSelector combinedSelector = (CombinedSelector)selector;
            return combinedSelector.isMiddleFingerPinched();
        } 
        else
        {
            return selector.isSecondaryTriggered();
        }
    }
    
    void Update()
    {
        if (IsGesturePerformed() && !createdAndStillHeld)
        {
            Transform selectorPosition = selector.transform;
            createdAndStillHeld = true;
            GameObject stone = Instantiate(stonePrefab, selectorPosition.position, selectorPosition.rotation, selectorPosition.transform);

            Throwable throwable = stone.GetComponent<Throwable>();
            if (throwable != null)
            {
                //throwable.selectors = selectorsForStones;
                throwable.isSelected = getCreatingHandIndex();
            }
        }
        else if (!IsGesturePerformed())
        {
            createdAndStillHeld = false;
        }
    }

    public int getCreatingHandIndex()
    {
        int indexOfCreatingSelector = 0;
        for (; indexOfCreatingSelector < selectorsForStones.Length; indexOfCreatingSelector++)
        {
            if (selectorsForStones[indexOfCreatingSelector] == selector)
            {
                return indexOfCreatingSelector;
            }
        }
        return indexOfCreatingSelector;
    }

    public void getStoneSelectors()
    {
        bool creatingSelectorFound = false;
        for (int i = 0; i < selectorsForStones.Length; i++)
        {
            if (selectorsForStones[i] == selector)
            {
                creatingSelectorFound = true;
                break;
            }
        }
        if (!creatingSelectorFound)
        {
            Selector[] newSelectors = new Selector[selectorsForStones.Length + 1];
            for (int i = 0; i < selectorsForStones.Length; i++)
            {
                newSelectors[i] = selectorsForStones[i];
            }
            newSelectors[selectorsForStones.Length] = selector;
            selectorsForStones = newSelectors;
        }
    }
}
