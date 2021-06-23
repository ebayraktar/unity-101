using System.Threading.Tasks;
using Models;

namespace REST
{
    public interface IRest
    {
        Task<ResponseModel> AgeGroups(string id = "");
        Task<ResponseModel> AnswerTypes(string id = "");
        Task<ResponseModel> Categories(string id = "");
        Task<ResponseModel> Answers(string id);
        Task<ResponseModel> PostAnswer(string json);
        Task<ResponseModel> QuestionCategories(string id);
        Task<ResponseModel> Questions(string id = "");
        Task<ResponseModel> QuestionTags(string id);
        Task<ResponseModel> QuestionTypes(string id = "");

        Task<ResponseModel> Tags(string id = "");
        //Task<ResponseModel> Users(string id = "");
    }
}