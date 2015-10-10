using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trackball_Project
{
    public class Data
    {
        private Boolean isClicked = false;
        private String FunctionName = "None";

        public Data(Boolean isClicked, String functionName)
        {
            this.isClicked = isClicked;
            this.FunctionName = functionName;
        }

        public void setIsClicked(Boolean flag)
        {
            this.isClicked = flag;
        }
        public void setFunctionName(String name)
        {
            this.FunctionName = name;
        }
        public Boolean getIsClicked() {
            return this.isClicked;
        }
        public String getFunctionName()
        {
            return this.FunctionName;
        }
    }
}
