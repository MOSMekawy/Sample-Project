# Application 
represents the application layer for our multilayered onion architecture sample project. this project uses [MediatR](#mediatr)

### File structure
**Note:** The MediatorEntryPoint.cs is an empty class that acts only as an entry point for MediatR package to load the project automatically without the need to register each command and query ourselves.
```
 Application
 |  MediatorEntryPoint.cs
 └─── Commands
 |       |
 |       └─── TodoItemCommands
 |       |           |  CreateTodoItem.cs
 |       |           |  DeleteTodoItem.cs
 |       |           |  UpdateTodoItem.cs
 |       |
 |       └─── TodoListCommands
 |       |           |  CreateTodoList.cs
 |       |           |  DeleteTodoList.cs
 |       |           |  UpdateTodoList.cs
 |       |
 |       └─── UserCommands
 |                   |  CreateUser.cs
 |                   |  DeleteUser.cs
 |                   |  UpdateUser.cs
 |        
 └─── Events
 |      |
 |      └─── TodoItemEvents
 |                 |  TodoItemCreated.cs
 |                  
 └─── Queries
        |
        └─── TodoItemQueries 
        |           |  GetTodoItem.cs
        |           |  GetListTodoItems.cs
        |           |
        └─── TodoListQueries
        |           |  GetTodoList.cs
        |           |  GetUserTodoLists.cs
        |       
        └─── UserQueries
                 |  GetUser.cs
                               
```

### MediatR 
[MediatR](https://github.com/jbogard/MediatR/wiki) is used in this project to provide mediation and pub/sub functionality in our project. 

