using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VotingSystem.Entities
{
    public class AnswerField
    {
        [Key]
        public int Id { get; set; }

        public string AnswerText { get; set; }
        public int Count { get; set; }
    }
}
