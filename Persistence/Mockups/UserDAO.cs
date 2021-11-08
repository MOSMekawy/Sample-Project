using Microsoft.VisualBasic.FileIO;
using System.Collections.Generic;
using Persistence.Entities;
using Persistence.Interfaces;
using System;

namespace Persistence.Mockups
{
    public class UserDAO : IUserDAO
    {
        private List<User> MockData = new List<User>();
        private int IdCounter = 0;
        public UserDAO(string Path)
        {
            using (TextFieldParser csvParser = new TextFieldParser(Path))
            {
                csvParser.CommentTokens = new string[] { "#" };
                csvParser.SetDelimiters(new string[] { "," });
                csvParser.HasFieldsEnclosedInQuotes = true;

                // Skip the row with the column names
                //csvParser.ReadLine();

                while (!csvParser.EndOfData)
                {
                    // Read current line fields, pointer moves to the next line.
                    string[] fields = csvParser.ReadFields();

                    if (Int32.Parse(fields[0]) > IdCounter) IdCounter = Int32.Parse(fields[0]);

                    MockData.Add(new User()
                    {
                      Id = Int32.Parse(fields[0]),
                      Name = (string)fields[1]
                    });
                }
            }
        }
        public bool DeleteUser(int Id)
        {
            MockData.Remove(MockData.Find((user) => user.Id == Id));

            return true;
        }

        public User GetUser(User user)
        {
            return MockData.Find((item) => {
               return (user.Id == 0 || item.Id == user.Id) && (user.Name == "" || user.Name == null || item.Name == user.Name);
            });
        }

        public User InsertUser(User user)
        {
            IdCounter++;
            user.Id = IdCounter;
            MockData.Add(user);
            return user;
        }

        public User UpdateUser(User user)
        {
            User toUpdate = MockData.Find((item) => item.Id == user.Id);

            if (user.Name != default)
             toUpdate.Name = user.Name;

            return toUpdate;
        }
    }
}
