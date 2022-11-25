using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class TabNavigation : MonoBehaviour
{
    public bool findFirstSelectable = true;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (EventSystem.current != null)
            {
                GameObject selected = EventSystem.current.currentSelectedGameObject;

                if (selected == null && findFirstSelectable)
                {
                    Selectable found = (Selectable.allSelectableCount > 0) ? Selectable.allSelectablesArray[0] : null;

                    if (found != null)
                    {
                        selected = found.gameObject;
                    }
                }

                if (selected != null)
                {
                    Selectable current = (Selectable)selected.GetComponent("Selectable");

                    if (current != null)
                    {
                        Selectable nextDown = current.FindSelectableOnDown();
                        Selectable nextUp = current.FindSelectableOnUp();
                        Selectable nextRight = current.FindSelectableOnRight();
                        Selectable nextLeft = current.FindSelectableOnLeft();

                        if (nextDown != null)
                        {
                            nextDown.Select();
                        }
                        else if (nextRight != null)
                        {
                            nextRight.Select();
                        }
                        else if (nextUp != null)
                        {
                            nextUp.Select();
                        }
                        else if (nextLeft != null)
                        {
                            nextLeft.Select();
                        }
                    }
                }
            }
        }
    }
}
