using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;
using UnityEngine;

namespace REST
{
    public class RestManager
    {
        private readonly IRest _service;

        public RestManager(IRest service)
        {
            _service = service;
        }

        public async Task<ResponseModel> AgeGroups(string id = "")
        {
            return await _service.AgeGroups(id);
        }

        public async Task<Questions> GetQuestions(string ageGroupID)
        {
            
            return null;
        }

        public async Task<Questions> RandomQuestion()
        {
            var questionCountRm = await _service.Questions();
            if (questionCountRm == null || questionCountRm.OpCode != 1) return null;
            var questionCountObj = questionCountRm.Result;
            if (!int.TryParse(questionCountObj.ToString(), out var questionCount)) return null;
            var randomQuestion = Random.Range(1, questionCount + 1);
            var questionRm = await _service.Questions(randomQuestion.ToString());
            if (questionRm == null || questionRm.OpCode != 1) return null;
            var question =
                Newtonsoft.Json.JsonConvert.DeserializeObject<List<Questions>>(questionRm.Result.ToString());
            return question.FirstOrDefault();
        }

        public async Task<List<Answers>> GetAnswers(string id)
        {
            var answerRm = await _service.Answers(id);
            if (answerRm == null || answerRm.OpCode != 1) return null;
            var answers =
                Newtonsoft.Json.JsonConvert.DeserializeObject<List<Answers>>(answerRm.Result.ToString());
            return answers;
        }

        public async Task<ResponseModel> PostAnswer(Answers answers)
        {
            var ansJson = Newtonsoft.Json.JsonConvert.SerializeObject(answers);
            return await _service.PostAnswer(ansJson);
        }
    }
}