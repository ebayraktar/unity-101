using REST;

public static class Constants
{
    private static RestManager _restManager;
    public static RestManager ServiceManager => _restManager ?? (_restManager = new RestManager(new RestService()));

    // private const string Host = "http://10.0.2.2:57571/";
    private const string Host = "http://localhost:5000/";
    private const string ApiAddress = "api/";
    private const string BaseAddress = Host + ApiAddress;

    public const string TagsURL = BaseAddress + "Tags/";
    public const string AgeGroupsURL = BaseAddress + "AgeGroups/";
    public const string AnswerTypesURL = BaseAddress + "AnswerTypes/";
    public const string CategoriesURL = BaseAddress + "Categories/";
    public const string QuestionsURL = BaseAddress + "Questions/";
    public const string QuestionTagsURL = BaseAddress + "QuestionTags/";
    public const string QuestionTypesURL = BaseAddress + "QuestionTypes/";
    public const string AnswersURL = BaseAddress + "Answers/";
    public const string QuestionCategoriesURL = BaseAddress + "QuestionCategories/";
    public static string Token { get; set; }
}