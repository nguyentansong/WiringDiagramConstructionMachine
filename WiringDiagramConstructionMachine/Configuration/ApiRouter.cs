using System;
namespace WiringDiagramConstructionMachine.Configuration
{
    public class ApiRouter
    {
        public const string LOGIN = "/api/auth/login";
        public const string REGISTER = "/api/auth/register";
        public const string GETSUBMENU = "api/menu/parent";
        public const string GET_LIST_PDF = "api/pdffile/listpdf";
        public const string DELETE_PDF = "api/pdffile";
        public const string GET_USERS = "api/user";
        public const string GET_USER = "api/user/oneuser";
        public const string UPDATE_USER = "api/user/updateuser";
    }
}
