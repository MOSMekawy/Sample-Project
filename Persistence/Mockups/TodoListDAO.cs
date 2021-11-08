using Microsoft.VisualBasic.FileIO;
using Persistence.Entities;
using Persistence.Interfaces;
using System.Collections.Generic;
using System;

namespace Persistence.Mockups
{
    public class TodoListDAO : ITodoListDAO
    {
        private List<TodoList> MockData = new List<TodoList>();
        private int IdCounter = 0;
        public TodoListDAO(string Path)
        {
            using (TextFieldParser csvParser = new TextFieldParser(Path))
            {
                csvParser.CommentTokens = new string[] { "#" };
                csvParser.SetDelimiters(new string[] { "," });
                csvParser.HasFieldsEnclosedInQuotes = true;

                // Skip the row with the column names
                // csvParser.ReadLine();

                while (!csvParser.EndOfData)
                {
                    // Read current line fields, pointer moves to the next line.
                    string[] fields = csvParser.ReadFields();

                    if (Int32.Parse(fields[0]) > IdCounter) IdCounter = Int32.Parse(fields[0]);

                    MockData.Add(new TodoList()
                    {
                        Id = Int32.Parse(fields[0]),
                        Name = fields[1],
                        Description = fields[2],
                        UserId = Int32.Parse(fields[3])
                    });
                }
            }
        }
        public bool DeleteTodoList(int Id)
        {
            MockData.Remove(MockData.Find((list) => list.Id == Id));

            return true;
        }

        public List<TodoList> GetMultipleTodoLists(TodoList list)
        {
            return MockData.FindAll((elm) =>
                       (list.Id == 0 || list.Id == elm.Id) &&
                       (list.Name == "" || list.Name == null || list.Name == elm.Name) &&
                       (list.Description == "" || list.Description == null || list.Description == elm.Description) &&
                       (list.UserId == 0 || list.UserId == elm.UserId)
            );
        }

        public TodoList GetTodoList(TodoList list)
        {   
            return MockData.Find((elm) =>
                       (list.Id == 0 || list.Id == elm.Id) &&
                       (list.Name == "" || list.Name == null || list.Name == elm.Name) &&
                       (list.Description == "" || list.Description == null || list.Description == elm.Description) &&
                       (list.UserId == 0 || list.UserId == elm.UserId)
            );
        }

        public TodoList InsertTodoList(TodoList item)
        {
            IdCounter++;
            item.Id = IdCounter;
            MockData.Add(item);
            return item;
        }

        public TodoList UpdateTodoList(TodoList item)
        {
            TodoList toUpdate = MockData.Find((elm) => elm.Id == item.Id);

            if (item.Name != default)
                toUpdate.Name = item.Name;

            return toUpdate;
        }
    }
}
