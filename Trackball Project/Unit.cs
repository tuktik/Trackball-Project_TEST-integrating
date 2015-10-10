using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trackball_Project
{
    class Unit
    {
        public Func mLeft = null;
        public Func mRight = null;
        public Func mUp = null;
        public Func mDown = null;
        public Func mDragUp = null;
        public Func mDragDown = null;

        public void setFuncAtLeft(Func func) {
            this.mLeft = func;
        }
        public void setFuncAtRight(Func func)
        {
            this.mRight = func;
        }
        public void setFuncAtUp(Func func)
        {
            this.mUp = func;
        }
        public void setFuncAtDown(Func func)
        {
            this.mDown = func;
        }
        public void setFuncAtDragUp(Func func)
        {
            this.mDragUp = func;
        }
        public void setFuncAtDragDown(Func func)
        {
            this.mDragDown = func;
        }
    }
}
