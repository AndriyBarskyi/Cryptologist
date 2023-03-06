﻿using System;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;

namespace CryptoApp
{
    public class DictionaryChecker
    {
        private readonly string _apiUrl = ConfigurationManager.AppSettings["engDictApiUrl"];

        public async Task<bool> CheckIfWordExists(string word)
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(_apiUrl + word).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine(word);
                    return true;
                }
                return false;
            }
        }

        public bool CheckIfTextIsValid(string text)
        {
            var words = text.Split(' ');
            foreach (var word in words)
            {
                if (!CheckIfWordExists(word.ToLower()).Result)
                {
                    return false;
                }
            }
            return true;
        }
    }
}