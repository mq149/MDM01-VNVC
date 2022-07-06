using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using MongoDB.Driver;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
namespace MDM01_VNVC.Models
{
    public class FrequentQuestions
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string Title { get; set; }
        public string Question { get; set; }
        public string Asker { get; set; }
        public string Respondent { get; set; }
        public string Answer { get; set; }
        public FrequentQuestions(string Title, string Question, string Asker, string Respondent, string Answer)
        {
            this.Title = Title;
            this.Question = Question;
            this.Asker = Asker;
            this.Respondent = Respondent;
            this.Answer = Answer;
        }
    }
}
