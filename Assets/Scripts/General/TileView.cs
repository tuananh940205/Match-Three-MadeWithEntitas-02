using DG.Tweening;
using Entitas;
using Entitas.Unity;
using UnityEngine;

public class TileView : MonoBehaviour
{
    private Vector2 firstPosition;
    private Vector2 lastPosition;
    private Tween tween;

    void OnMouseDown ()
    {
        Vector3[] wayPath = new Vector3[]
        {
            transform.position,
            new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z),
            transform.position
        };

        firstPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        tween = transform.DOPath(wayPath, .8f);
        EntityLink enLink = gameObject.GetEntityLink();

        if (enLink != null)
        {
            GameEntity e = enLink.entity as GameEntity;
            e.ReplaceFirstPosition(firstPosition.x, firstPosition.y);
        }
    }

    void OnMouseUp ()
    {
        tween.Complete();
        lastPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        EntityLink enLink = gameObject.GetEntityLink();

        if (enLink != null)
        {
            GameEntity e = enLink.entity as GameEntity;
            // Debug.LogFormat("ex = {0}, ey = {1}", e.arrayPosition.x, e.arrayPosition.y);
            e.ReplaceLastPosition(lastPosition.x, lastPosition.y);
        }
    }
}