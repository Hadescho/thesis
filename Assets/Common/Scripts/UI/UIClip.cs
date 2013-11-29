using System;
using System.Collections.Generic;
using UnityEngine;

public class UIClip : MonoBehaviour
{
    public float Left = 50.0f;
    public float Top = 50.0f;
    public float Right = 50.0f;
    public float Bottom = 50.0f;
    public List<exSprite> ClippedSprites = new List<exSprite>();

    private Rect _r;

    void Update()
    {
        float x = transform.position.x;
        float y = transform.position.y;

        float cx1 = x - Left;
        float cy1 = y - Bottom;
        float cx2 = x + Right;
        float cy2 = y + Top;

        for(int i = 0; i < ClippedSprites.Count; i++)
        {
            exSprite sprite = ClippedSprites[i];

            float sx = sprite.transform.position.x - sprite.width / 2.0f;
            float sy = sprite.transform.position.y - sprite.height / 2.0f;
            Rect sr = new Rect(sx, sy, sprite.width, sprite.height);

            _r = RectExt.Intersect(new Rect(cx1, cy1, cx2 - cx1, cy2 - cy1), sr);

            Mesh mesh = sprite.meshFilter.mesh;
            exAtlas.Element el = sprite.atlas.elements[sprite.index];

            float tw = el.coords.xMax - el.coords.x;
            float th = el.coords.yMax - el.coords.y;

            float tx1 = el.coords.x - (sx - _r.x) / sprite.width * tw;
            float ty1 = el.coords.y - (sy - _r.y) / sprite.height * th;
            float tx2 = el.coords.xMax - (sx + sprite.width - _r.xMax) / sprite.width * tw;
            float ty2 = el.coords.yMax - (sy + sprite.height - _r.yMax) / sprite.height * th;

            Vector2[] uv = new Vector2[]
            {
                new Vector2 { x = tx1, y = ty2 },
                new Vector2 { x = tx2, y = ty2 },
                new Vector2 { x = tx1, y = ty1 },
                new Vector2 { x = tx2, y = ty1 }
            };

            mesh.uv = uv;
        }
    }

    public void OnDrawGizmos()
    {
        Vector3 position = transform.position + new Vector3((Right - Left) / 2.0f, (Top - Bottom) / 2.0f, 0.0f);
        Vector3 size = new Vector3(Left + Right, Top + Bottom, 0.0f);

        Gizmos.color = new Color(0.0f, 1.0f, 0.0f);
        Gizmos.DrawWireCube(position, size);

        for(int i = 0; i < ClippedSprites.Count; i++)
        {
            exSprite sprite = ClippedSprites[i];

            Gizmos.color = new Color(0.0f, 0.0f, 1.0f);
            Gizmos.DrawWireCube(sprite.transform.position, new Vector3(sprite.width, sprite.height, 0.0f));

            Gizmos.color = new Color(0.0f, 1.0f, 1.0f);
            Gizmos.DrawWireCube(new Vector3(_r.center.x, _r.center.y), new Vector3(_r.width, _r.height, 0.0f));
        }
    }
}