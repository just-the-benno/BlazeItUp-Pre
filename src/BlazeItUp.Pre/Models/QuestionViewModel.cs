using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazeItUp.Pre.Models
{
    public class SummeryEntry
    {
        public String Option { get; init; }
        public Int32 VotesCasted { get; private set; }
        public Int32 TotalVotes { get; private set; }

        public SummeryEntry(String option)
        {
            Option = option;
            VotesCasted = 1;
            TotalVotes = 1;
        }

        public void SetTotalVotes(Int32 total) => TotalVotes = total;

        public void AddOneVote() => VotesCasted += 1;
    }

    public class SurveyResults
    {
        public IEnumerable<Question> Questions { get; set; }
        public Int32 ParticipantAmount { get; set; }
        public DateTime FirstVote { get; set; }
        public DateTime LastVote { get; set; }
    }

    public class Question
    {
        public String Name { get; init; }
        public Boolean IsMultipleChoice { get; private set; }

        public Dictionary<DateTime, IEnumerable<String>> Answers { get; init; } = new();

        public Dictionary<String, SummeryEntry> Summery { get; init; } = new();

        public Question(String name)
        {
            Name = name;
        }

        public void AddResponse(String timestamp, String rawResponses)
        {
            String[] responses = rawResponses.Split(';', StringSplitOptions.RemoveEmptyEntries);

            if (responses.Length > 1)
            {
                IsMultipleChoice = true;
            }

            Answers.Add(DateTime.ParseExact(timestamp, "yyyy/MM/dd h:mm:ss tt GMT+8", System.Globalization.CultureInfo.InvariantCulture), responses);

            foreach (var item in responses)
            {
                ProcessOption(Summery,item);
            }

        }

        private void ProcessOption(IDictionary<String,SummeryEntry> dict, string item)
        {
            if (dict.ContainsKey(item) == false)
            {
                dict.Add(item, new SummeryEntry(item));
            }
            else
            {
                dict[item].AddOneVote();
            }
        }

        internal void PrepareSummery()
        {
            foreach (var item in Summery.Values)
            {
                item.SetTotalVotes(Answers.Count);
            }
        }


        public  IEnumerable<SummeryEntry> GetSummeryUntil(DateTime dateTime)
        {
            Dictionary<String, SummeryEntry> result = new();
            Int32 totalEntries = 0;
            foreach (var item in Answers)
            {
                if(item.Key > dateTime) { continue; }

                foreach (var option in item.Value)
                {
                    ProcessOption(result, option);
                }

                totalEntries++;
            }

            foreach (var item in result.Values)
            {
                item.SetTotalVotes(totalEntries);
            }

            return result.Values;
        }
    }
}
