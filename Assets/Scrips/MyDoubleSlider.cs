using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Unite
{
    public class MyDoubleSlider :   MaskableGraphic
    {
        [SerializeField]
        [Tooltip("被分成多少份，如果不是整位进模式，填-1")]
        int Count = 11;
        private Transform leftTran, rightTran;
       // private RectTransform rectTransform;

        float width;
        protected override void Awake()
        {
            base.Awake();
            leftTran = transform.Find("Left");
            rightTran = transform.Find("Right");
            //rectTransform = GetComponent<RectTransform>();
            width = rectTransform.rect.size.x;
        }


        /// <summary>
        /// 限制一下移动范围 以及移动固定单位时的判定
        /// </summary>
        /// <param name="v"></param>
        /// <param name="tran"></param>
        /// <returns></returns>
        public Vector2 Clamp(Vector2 v,Transform tran)
        {
            v.y = 0;
            if (tran.Equals(leftTran))
            {
                v.x = Mathf.Clamp(v.x,-width/2, rightTran.localPosition.x);
                if(Count>0)
                {
                    float c = width / Count;
                    float offset = v.x - leftTran.localPosition.x;
                    offset= Mathf.RoundToInt(offset/c)*c;
                    v.x = leftTran.localPosition.x + offset;
                }
            }
            else
            {
                v.x = Mathf.Clamp(v.x, leftTran.localPosition.x, width/2);
                if (Count > 0)
                {
                    float c = width / Count;
                    float offset = v.x - rightTran.localPosition.x;
                    offset = Mathf.RoundToInt(offset / c) * c;
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
            verts[0].position = new Vector3(leftTran.localPosition.x, -15);
            verts[0].color = color;
            verts[0].uv0 = Vector2.zero;
            
            verts[1].position = new Vector3(leftTran.localPosition.x, 15);
            verts[1].color = color;
            verts[1].uv0 = Vector2.zero;
            
            verts[2].position = new Vector3(rightTran.localPosition.x, 15);
            verts[2].color = color;
            verts[2].uv0 = Vector2.zero;
            
            verts[3].position = new Vector3(rightTran.localPosition.x, -15);
            verts[3].color = color;
            verts[3].uv0 = Vector2.zero;
            
            vh.AddUIVertexQuad(verts);
        }
    }
}