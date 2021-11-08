using Microsoft.VisualBasic.FileIO;
using Persistence.Entities;
using Persistence.Interfaces;
using System.Collections.Generic;
using System;

namespace Persistence.Mockups
{
    public class TodoItemDAO : ITodoItemDAO
    {   
        private List<TodoItem> MockData = new List<TodoItem>();
        private int IdCounter = 0;
        public TodoItemDAO(string Path)
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

                    MockData.Add(new TodoItem()
                    {
                        Id = Int32.Parse(fields[0]),
                        Name = fields[1],
                        Description = fields[2],
                        TodoListId = Int32.Parse(fields[3])
                    });
                }
            }
        }
        public bool DeleteTodoItem(int Id)
        {
            MockData.Remove(MockData.Find((item) => item.Id == Id));

            return true;
        }

        public List<TodoItem> GetMultipleTodoItems(TodoItem item)
        {
            return MockData.FindAll((elm) => 
                       (item.Id == 0 || item.Id == elm.Id) && 
                       (item.Name == "" || item.Name == null || item.Name == elm.Name) && 
                       (item.Description == "" || item.Description == null || item.Description == elm.Description) &&
                       (item.TodoListId == 0 || item.TodoListId == elm.TodoListId)
            );
        }

        public TodoItem GetTodoItem(TodoItem item)
        {
            return MockData.Find((elm) => 
                       (item.Id == 0 || item.Id == elm.Id) && 
                       (item.Name == "" || item.Name == null || item.Name == elm.Name) && 
                       (item.Description == "" || item.Description == null || item.Description == elm.Description) &&
                       (item.TodoListId == 0 || item.TodoListId == elm.TodoListId)
            );
        }

        public TodoItem InsertTodoItem(TodoItem item)
        {
            IdCounter++;
            item.Id = IdCounter;
            MockData.Add(item);
            return item;
        }

        public TodoItem UpdateTodoItem(TodoItem item)
        {
            TodoItem toUpdate = MockData.Find((elm) => elm.Id == item.Id);

            if (item.Name != default)
                toUpdate.Name = item.Name;

            return toUpdate;
        }
    }
}
