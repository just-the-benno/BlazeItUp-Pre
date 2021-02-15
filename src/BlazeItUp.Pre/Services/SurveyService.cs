using BlazeItUp.Pre.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Json;

namespace BlazeItUp.Pre.Services
{
    public class SurveyService
    {
        private readonly HttpClient _client;

        public SurveyService(HttpClient client)
        {
            this._client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<SurveyResults> LoadSurveyData()
        {
            var rawResult = await _client.GetFromJsonAsync<List<Dictionary<String, String>>>("/sample-data/survey-data.json");

            Dictionary<String, Question> results = new();

            foreach (var entry in rawResult)
            {
                String timeStamp = String.Empty;

                foreach (var item in entry)
                {
                    if(item.Key == "Timestamp")
                    {
                        timeStamp = item.Value;
                    }
                    else
                    {
                        if(results.ContainsKey(item.Key) == false)
                        {
                            results.Add(item.Key, new Question(item.Key));
                        }

                        results[item.Key].AddResponse(timeStamp, item.Value);
                    }
                }
            }

            foreach (var item in results.Values)
            {
                item.PrepareSummery();
            }

            return new SurveyResults
            {
                Questions = results.Values,
                ParticipantAmount = rawResult.Count,
                FirstVote = results.First().Value.Answers.First().Key,
                LastVote = results.First().Value.Answers.Last().Key,
            };
        }

    }
}
