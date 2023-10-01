using System;
using System.Collections.Generic;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class ApplicantsData
{
    [BsonId]
    public ObjectId Id { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
    public DateTime DateOfBirth { get; set; }
    public decimal Salary { get; set; }
    public bool IsActive { get; set; }
    public Address CurrentAddress { get; set; }
    public List<string> Languages { get; set; }
    public Dictionary<string, string> Contacts { get; set; }
    public EducationInfo Education { get; set; }
    // Add more properties as needed

    public class Address
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
    }

    public class EducationInfo
    {
        public string Degree { get; set; }
        public string University { get; set; }
        public int GraduationYear { get; set; }
    }
}
class Program
{
    private static ApplicantRepository? applicantRepository;
    static async Task Main(string[] args)
    {
        // MongoDB connection settings
        
        var connectionString = ""; // MongoDB server address
        var databaseName = "bdjobs"; // Database name
        var collectionName = "applicants"; // Collection name

        // Establish a connection to MongoDB
        var client = new MongoClient(connectionString);
        var database = client.GetDatabase(databaseName);

        // Get a reference to the collection
        var collection = database.GetCollection<ApplicantsData>(collectionName);
        applicantRepository = new ApplicantRepository();

        // Create a new complex data document

        // Insert the document into the collection
        //collection.InsertOne(complexData);
        //for (int i = 0; i < 300000; i++)
        //{
        //    List<ApplicantsData> applicantsData = GenerateRandomApplicants(1000,i);
        //    collection.InsertMany(applicantsData);
        //    Console.WriteLine("Round "+i.ToString());
        //}


        for (int i = 0; i < 1000; i++)
        {
            int numberOfQueries = 500;
            DateTime startDateTime = DateTime.Now;
            List<List<ApplicantsData>> results = await ExecuteQueriesInParallel(numberOfQueries, 1000);
            DateTime endDateTime = DateTime.Now;

            Console.WriteLine("Time Taken - " + (endDateTime - startDateTime).TotalMilliseconds);
        }
    }
    static async Task<List<List<ApplicantsData>>> ExecuteQueriesInParallel(int numberOfQueries, int limit)
    {
        // Create a list of tasks to run the query function in parallel
        var tasks = Enumerable.Range(0, numberOfQueries)
            .Select(i => applicantRepository.GetApplicantsOlderThanAsync(new Random().Next(50),limit))
            .ToList();

        // Wait for all tasks to complete
        List<ApplicantsData>[] results =await Task.WhenAll(tasks);

        return results.ToList();
    }



    public class ApplicantRepository
    {
        private string connectionString = ""; // MongoDB server address
        private string databaseName = "bdjobs"; // Database name
        private string collectionName = "applicants"; // Collection name
        private MongoClient client;
        private IMongoDatabase database;
        private IMongoCollection<ApplicantsData> applicantCollection;
        public ApplicantRepository()
        {
            client = new MongoClient(connectionString);
            database = client.GetDatabase(databaseName);
            applicantCollection = database.GetCollection<ApplicantsData>("applicants");
        }
        public List<ApplicantsData> GenerateRandomApplicants(int count, int round)
        {
            var random = new Random();
            var applicants = new List<ApplicantsData>();
            int i = round * count;
            int totalcount = (round + 1) * count;
            for (; i < totalcount; i++)
            {
                var applicant = new ApplicantsData
                {
                    FirstName = "First" + i,
                    LastName = "Last" + i,
                    Age = random.Next(18, 65),
                    DateOfBirth = DateTime.Now.AddYears(-random.Next(18, 65)),
                    Salary = (decimal)(random.NextDouble() * 50000 + 25000),
                    IsActive = random.Next(2) == 0,
                    CurrentAddress = new ApplicantsData.Address
                    {
                        Street = "Street" + i,
                        City = "City" + i,
                        State = "State" + i,
                        ZipCode = "Zip" + i
                    },
                    Languages = new List<string> { "English", "Spanish", "French" },
                    Contacts = new Dictionary<string, string>
                {
                    { "Email", "email" + i + "@example.com" },
                    { "Phone", "555-123-45" + (i % 10) }
                },
                    Education = new ApplicantsData.EducationInfo
                    {
                        Degree = "Degree" + i,
                        University = "University" + i,
                        GraduationYear = random.Next(2000, 2022)
                    }
                    // Add values for other properties as needed
                };

                applicants.Add(applicant);
            }

            return applicants;
        }
        public async Task<List<ApplicantsData>> GetApplicantsOlderThanAsync(int ageThreshold, int limit)
        {
            
            var filter = Builders<ApplicantsData>.Filter.Gt("Age", ageThreshold);

            List<ApplicantsData> result = await applicantCollection.Find(filter).Limit(limit).ToListAsync();

            return result;
        }

    }
}