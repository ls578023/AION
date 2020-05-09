using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragScript : MonoBehaviour, IBeginDragHandler, IDragHandler
{

        Vector3 curPos;
        Vector3 offset;


        public void OnBeginDrag(PointerEventData eventData)
        {
            curPos = transform.position;
            offset = curPos - Input.mousePosition;
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = Input.mousePosition + offset;
            transform.SetAsLastSibling();
        }
}
