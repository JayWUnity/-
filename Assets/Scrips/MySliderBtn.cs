using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

namespace Unite
{

    public class MySliderBtn : MonoBehaviour , IDragHandler
    {
        RectTransform rectTran;
        MyDoubleSlider myslider;

        void Awake()
        {
            rectTran = transform.parent.GetComponent<RectTransform>();
            myslider = rectTran.GetComponent<MyDoubleSlider>();
        }


        /// <summary>
        /// Object被拖动的时候
        /// </summary>
        /// <param name="eventData"></param>
        public void OnDrag(PointerEventData eventData)
        {
            Vector2 pos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTran.GetComponent<RectTransform>(),
                Input.mousePosition, null, out pos);

            pos = myslider.Clamp(pos,transform);     
            transform.localPosition = pos;
            myslider.OnDrag();
        }
    }
}