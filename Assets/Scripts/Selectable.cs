using UnityEngine;
using System.Linq;

public class Selectable : MonoBehaviour
{
    public Color hoverColor = Color.yellow;
    public Color selectColor = Color.red;

    public Selector[] selectors = new Selector[] {};
    protected SelectorState[] selectorStates;

    protected bool[] isHovered;
    protected int isSelected = -1;

    void Start()
    {
        isHovered = new bool[selectors.Length];
        selectorStates = new SelectorState[selectors.Length];

        for (int i = 0; i < selectors.Length; i++)
        {
            selectorStates[i].selector = selectors[i];
            isHovered[i] = false;
        }
    }

    // This method needs to be called by the child class
    // Usually in the Update() call after refreshing the isHovered array
    protected void UpdateColor()
    {
        if (isSelected != -1)
        {
            GetComponent<Renderer>().material.color = selectColor;
        }
        else if (isHovered.Any(x => x))
        {
            GetComponent<Renderer>().material.color = hoverColor;
        }
        else
        {
            GetComponent<Renderer>().material.color = Color.white;
        }
    }
}
