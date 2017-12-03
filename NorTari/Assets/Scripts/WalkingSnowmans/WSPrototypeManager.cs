using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WSPrototypeManager : MonoBehaviour {

    public void InitPrototype () {
        SpriteRenderer spriteRenderer = transform.Find("SpriteRenderer").GetComponent<SpriteRenderer>();
        Transform prototypeTransform = transform.Find("PrototypeTransform");
        spriteRenderer.gameObject.AddComponent<PolygonCollider2D>();
        prototypeTransform.gameObject.AddComponent<PolygonCollider2D>();
        prototypeTransform.GetComponent<PolygonCollider2D>().isTrigger = true;
        float scale = (prototypeTransform.GetComponent<RectTransform>().rect.height * 100 - 100) / Mathf.Max(spriteRenderer.GetComponent<SpriteRenderer>().sprite.rect.size.x, spriteRenderer.GetComponent<SpriteRenderer>().sprite.rect.size.y);
        prototypeTransform.GetComponent<PolygonCollider2D>().points = ScalePoints(spriteRenderer.GetComponent<PolygonCollider2D>().points, scale);
    }

    private Vector2[] ScalePoints (Vector2[] points, float scale) {
        for (int i = 0 ; i < points.Length ; i++) {
            Vector2 point = points[i];
            point.x *= scale;
            point.y *= scale;
            points[i] = point;
        }
        return points;
    }

}
