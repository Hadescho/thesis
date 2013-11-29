using UnityEngine;
using System.Collections.Generic;

public class UIFlow : MonoBehaviour
{
    public int Columns = 4;
    public int Rows = 4;
    public float VerticalDistance = 100.0f;
    public float HorizontalDistance = 100.0f;
    public UI.Alignment Alignment = UI.Alignment.MiddleCenter;
    public UI.Direction Direction = UI.Direction.Horizontal;
	
    private Vector3 _offset = new Vector3(0.0f, 0.0f, 0.0f);
	private UIPagination _pagination;
	private Vector3 _pageOffset;

    void Awake()
    {
        Commit();
    }

    public void Commit()
    {
        _ComputeOffset();

        int x = 0;
        int y = 0;
		int page = 0;

		_pagination = GetComponent<UIPagination>();

		if(_pagination == null)
		{
			_pageOffset = Vector3.zero;

			if(transform.childCount > Columns * Rows)
			{
				Debug.LogError("Too many children.");
				return;
			}
		}
		else
		{
			_pageOffset = new Vector3(_pagination.PageOffset, 0.0f, 0.0f);
		}

		int width;
		int height;

		if(Direction == UI.Direction.Horizontal)
		{
			width = Columns;
			height = Rows;
		}
		else
		{
			width = Rows;
			height = Columns;
		}

        for(int i = 0; i < transform.childCount; i++)
        {
            GameObject go = transform.GetChild(i).gameObject;

            go.transform.localPosition = _GetPosition(x, y, page);

            x++;

            if(x >= width)
            {
                x = 0;
                y++;

				if(y >= height)
				{
					y = 0;
					page++;
				}
            }
        }

		if(_pagination != null)
		{
			if(x != 0 || y != 0)
			{
				page++;
			}

			_pagination.SetPages(page);
		}
    }

    private void _ComputeOffset()
    {
        float x = 0.0f;
        float y = 0.0f;
        float w = (Columns - 1) * HorizontalDistance;
        float h = (Rows - 1) * VerticalDistance;

        if(Alignment == UI.Alignment.TopCenter)
        {
            x = -w / 2.0f;
            y = 0.0f;
        }
        else if(Alignment == UI.Alignment.TopLeft)
        {
            x = 0.0f;
            y = 0.0f;
        }
        else if(Alignment == UI.Alignment.TopRight)
        {
            x = -w;
            y = 0.0f;
        }
        else if(Alignment == UI.Alignment.MiddleCenter)
        {
            x = -w / 2.0f;
            y = -h / 2.0f;
        }
        else if(Alignment == UI.Alignment.MiddleLeft)
        {
            x = 0.0f;
            y = -h / 2.0f;
        }
        else if(Alignment == UI.Alignment.MiddleRight)
        {
            x = -w;
            y = -h / 2.0f;
        }
        else if(Alignment == UI.Alignment.BottomCenter)
        {
            x = -w / 2.0f;
            y = -h;
        }
        else if(Alignment == UI.Alignment.BottomLeft)
        {
            x = 0.0f;
            y = -h;
        }
        else if(Alignment == UI.Alignment.BottomRight)
        {
            x = -w;
            y = -h;
        }

        _offset = new Vector3(x, y, 0.0f);
    }

    private Vector3 _GetPosition(int x, int y, int page)
    {
        if(Direction == UI.Direction.Vertical)
        {
            int temp = x;
            x = y;
            y = temp;
        }

		return(new Vector3(x * HorizontalDistance, y * VerticalDistance, -1.0f) + _offset + page * _pageOffset);
    }
}