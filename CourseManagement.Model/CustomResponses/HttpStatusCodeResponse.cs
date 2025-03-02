namespace CourseManagement.Model.CustomResponses
{
    internal static class HttpStatusCodeResponse
    {

        // Success codes
        public static int OK { get => 200; }
        public static int CREATED { get => 201; }
        public static int NO_CONTENT { get => 204; }


        // Fail codes
        public static int BAD_REQUEST { get => 400; }
        public static int UNAUTHORIZED { get => 401; }
        public static int FORBBIDEN { get => 403; }
        public static int NOT_FOUND { get => 404; }
        public static int CONFLICT { get => 409; }
        public static int INTERNAL_SERVER_ERROR { get => 500; }
    }
}
