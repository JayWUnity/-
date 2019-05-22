using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

namespace Unite
{
    public class MyDoubleSlider : MaskableGraphic
    {
        //开始时间
        public DateTime StartTime { get {  DateTime dateTime = DateTime.Now.Date; return dateTime.Add(new TimeSpan(0, GetHour(leftTran.localPosition.x), 0, 0));}}

        //结束时间
        public DateTime EndTime { get { DateTime dateTime = DateTime.Now.Date; return dateTime.Add(new TimeSpan(0, GetHour(rightTran.localPosition.x), 0, 0)); } }

        [SerializeField]
        [Tooltip("被分成多少份，如果不是整位进模式，填-1")]
        int Count = 12;
        private Transform leftTran, rightTran;

        float width, height,InitPosX;
        float uniteLength;
        protected override void Awake()
        {
            base.Awake();
            leftTran = transform.Find("Left");
            rightTran = transform.Find("Right");
            width = rectTransform.rect.size.x;
            height = rectTransform.rect.size.y;
            InitPosX = transform.localPosition.x;
            uniteLength = width / Count;
        }


        int GetHour(float xpos)
        {
            float offset = xpos- InitPosX;
            int count = Mathf.RoundToInt(xpos / uniteLength);

            return 12 + count;
        }


        /// <summary>
        /// 限制一下移动范围 以及移动固定单位时的判定
        /// </summary>
        /// <param name="v"></param>
        /// <param name="tran"></param>
        /// <returns></returns>
        public Vector2 Clamp(Vector2 v, Transform tran)
        {
            v.y = 0;
            if (tran.Equals(leftTran))
            {
                v.x = Mathf.Clamp(v.x, -width / 2+ InitPosX, rightTran.localPosition.x);
                if (Count > 0)
                {
                    float offset = v.x - leftTran.localPosition.x;
                    offset = Mathf.RoundToInt(offset / uniteLength) * uniteLength;
                    v.x = leftTran.localPosition.x + offset;
                }
            }
            else
            {
                v.x = Mathf.Clamp(v.x, leftTran.localPosition.x, width / 2+ InitPosX);
                if (Count > 0)
                {
                    float offset = v.x - rightTran.localPosition.x;
                    offset = Mathf.RoundToInt(offset / uniteLength) * uniteLength;
                    v.x = rightTran.localPosition.x + offset;
                }
            }
            return v;
        }


        /// <summary>
        /// 拖动两端的时候调用这个脚本  
        /// </summary>
        public void OnDrag()
        {
            SetVerticesDirty();//更改顶点  会调用OnPopulateMesh函数
        }

        protected override void OnPopulateMesh(VertexHelper vh)
        {
            vh.Clear();
            UIVertex[] verts = new UIVertex[4];
            verts[0].position = new Vector3(leftTran.localPosition.x, -height / 2);
            verts[0].color = color;
            verts[0].uv0 = Vector2.zero;

            verts[1].position = new Vector3(leftTran.localPosition.x, height / 2);
            verts[1].color = color;
            verts[1].uv0 = Vector2.zero;

            verts[2].position = new Vector3(rightTran.localPosition.x, height / 2);
            verts[2].color = color;
            verts[2].uv0 = Vector2.zero;

            verts[3].position = new Vector3(rightTran.localPosition.x, -height / 2);
            verts[3].color = color;
            verts[3].uv0 = Vector2.zero;

            vh.AddUIVertexQuad(verts);
        }
    }
}