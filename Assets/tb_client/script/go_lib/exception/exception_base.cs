// gowinder@hotmail.com
// Assembly-CSharp
// exception_base.cs
// 2016-05-10-17:45

#region

using System;

#endregion

namespace Assets.tb_client.script.go_lib.exception
{
    internal class exception_base : Exception
    {
        public const int RETURN_NOT_VALID_DATA = -1; //!<	for client to use 
        public const int RETURN_OK = 0;
        public const int RETURN_NO_DATA = 1; //!<	
        public const int RETURN_NEED_ASYN_LOAD = 2; //!<	
        public const int RETURN_WAIT_TO_OTHER_SERVICE_TO_PROCESS = 3; //!<	
        public const int RETURN_NULL_REF = 100; //!<	
        public const int RETURN_CHECK_FAILED = 101; //!<	check
        public const int RETURN_CREATE_NEW = 102; //!<	
        public const int RETURN_INPUT_INVALID_PARAMETER = 103; //!<	
        public const int RETURN_CRASH = 104;
        public const int RETURN_NULL_TYPE_REF = 105; //!<	
        public const int RETURN_CACULATE_FAILED = 106; //!<	
        public const int RETURN_DB_SAVE_FAILED = 107; //!< 	
        public const int RETURN_DB_LOAD_FAILED = 108; //!< 	
        public const int RETURN_EXECUTE_STORED_PROCEDURE_FAILED = 109; //!<	
        public const int RETURN_NORMAL_CHECK_FAILED = 201; //!<	


        public exception_base(string id)
        {
            exception_id = id;
        }

        public string exception_id { get; set; }
    }
}