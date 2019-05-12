using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridItems : MonoBehaviour {

	public int x
    {
        get;
       private set;
    }

    public int y
    {
        get;
        private set;
    }

    [HideInInspector]
    public int m_id;

   public void OnItemPositionChanged(int newx, int newy)
    {
        x = newx;
        y = newy;

        gameObject.name = string.Format("{0}{1}", x, y);
    }


    public delegate void OnMouseOveritem(GridItems item);
    public static event OnMouseOveritem OnMouseEventHandler;


    private void OnMouseDown()
    {
        //if (OnMouseEventHandler != null)
          //  OnMouseEventHandler;
    }
}
