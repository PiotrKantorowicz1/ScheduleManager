namespace Manager.Struct.Exceptions
{
    public static class ErrorCodes
    {
        public static string NameInUse => "name_in_use";
        public static string InvalidName => "invalid_name";
        public static string TaskNotFound => "task_not_found";
        public static string ScheduleNotFound => "schedule_not_found";
        public static string UserNotFound => "user_not_found";
        public static string DetailsNotFound => "details_not_found";
    }
}