

public static class APIUrls
{
    public static readonly string baseAddress = "https://localhost:7145/";
    public static readonly string userLoginApi = baseAddress + "api/Users/login";
    public static readonly string userRegisterApi = baseAddress + "api/Users/register";
    public static readonly string getUsersApi = baseAddress + "api/Users";
    public static readonly string getQuestionsApi = baseAddress + "api/Questions";
    public static readonly string postQuestionApi = baseAddress + "api/Questions";
    public static readonly string getExaminationsApi = baseAddress + "api/Examinations?pageIndex=1&pageSize=5";
    public static readonly string postAttemptApi = baseAddress + "api/Attempts";
    public static readonly string postMBTIResultApi = baseAddress + "api/MBTIs";


}