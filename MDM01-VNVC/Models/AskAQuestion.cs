using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MDM01_VNVC.Models
{
    public class AskAQuestion
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonElement("FullName")]
        public string FullName { get; set; }
        [BsonElement("Sex")]
        public string Sex { get; set; }
        [BsonElement("Age")]
        public int Age { get; set; }
        [BsonElement("Email")]
        public string Email { get; set; }
        [BsonElement("Phone")]
        public string Phone { get; set; }
        [BsonElement("Address")]
        public string Address { get; set; }
        [BsonElement("Title")]
        public string Title { get; set; }
        [BsonElement("Question")]
        public string Question { get; set; }
        [BsonElement("Answer")]
        public string Answer { get; set; }

        public AskAQuestion(string FullName, string Sex, int Age, string Email, string Phone, string Address, string Title, string Question, string Answer)
        {
            this.FullName = FullName;
            this.Sex = Sex;
            this.Age = Age;
            this.Email = Email;
            this.Phone = Phone;
            this.Address = Address;
            this.Title = Title;
            this.Question = Question;
            this.Answer = Answer;
        }
    }
}
