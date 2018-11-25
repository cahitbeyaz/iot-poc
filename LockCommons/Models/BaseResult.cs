using LockCommons.LockApi;
using System;
using System.Collections.Generic;
using System.Text;

namespace LockCommons.Models
{
    public class BaseResult
    {
        public BaseResult()
        {

        }

        public BaseResult(int code,string description)
        {
            this.code = code;
            this.description = description;
        }

        public BaseResult(int code)
        {
            switch (code)
            {
                case ResultCodes.Success:
                    description = "success";
                    break;
                case ResultCodes.SystemError:
                    description = "system error";
                    break;
                default:
                    break;
            }
        }


        public int code { get; set; }
        public string description { get; set; }
    }
}
