using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using XamarinForms_Demo.Models;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;

namespace XamarinForms_Demo.Services
{
    public class PersonServices
    {
        private static string URL = "YOUR URL TO YOUR WEBSERVICE HERE"; //Add your URL here. Should look something like this www.yoursite.com/api/person/{0}
        HttpClient client;

        //Constructor
        public PersonServices()
        {
            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
        }


        //Get all persons
        public async Task<List<PersonModel>> GetAllPersonsAsync()
        {
            List<PersonModel> personList = new List<PersonModel>();

            var uri = new Uri(string.Format(URL, string.Empty));

            try
            {
                var response = await client.GetAsync(uri);
                if(response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    personList = JsonConvert.DeserializeObject<List<PersonModel>>(content);
                }
                else
                {
                    personList = null;
                    Debug.WriteLine(@"				Something went wrong.");
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }
            return personList;
        }

        public async Task<PersonModel> GetOnePerson(int idPerson)
        {
            string id = idPerson.ToString();
            PersonModel person = new PersonModel();

            var uri = new Uri(string.Format(URL, id));

            try
            {
                HttpResponseMessage response = null;
                response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    person = JsonConvert.DeserializeObject<PersonModel>(content);
                }
                else
                {
                    person = null;
                    Debug.WriteLine(@"				No person found..");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }
            return person;
        }

        //Create a new person
        public async Task<bool> CreateNewPersonAsync(PersonModel person)
        {
            bool ok = false;
            var uri = new Uri(string.Format(URL, string.Empty));

            try
            {
                var json = JsonConvert.SerializeObject(person);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = null;
                response = await client.PostAsync(uri, content);

                if(response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"                        Sucess");
                    ok = true;
                }
                else
                {
                    Debug.WriteLine(@"                        Person not created.");
                }
                return ok;
            }
            catch(Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
                return ok;
            }
        }

        //Update a person
        public async Task<bool> UpdatePersonAsync(PersonModel person)
        {
            bool ok = false;
            string id = person.IDPerson.ToString();

            var uri = new Uri(string.Format(URL, id));

            try
            {
                var json = JsonConvert.SerializeObject(person);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = null;
                response = await client.PutAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"				Person successfully updated.");
                    ok = true;
                }
                else
                {
                    Debug.WriteLine(@"				Person NOT updated.");
                }
                return ok;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
                return ok;
            }
        }

        //Delete a person.
        public async Task<bool> DeletePersonAsync(int idPerson)
        {
            bool ok = false;
            string id = idPerson.ToString();
            var uri = new Uri(string.Format(URL, id));

            try
            {
                var response = await client.DeleteAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"				Person successfully deleted.");
                    ok = true;
                }
                else
                {
                    Debug.WriteLine(@"				Person NOT deleted.");
                }
                return ok;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
                return ok;
            }
        }
    }
}
