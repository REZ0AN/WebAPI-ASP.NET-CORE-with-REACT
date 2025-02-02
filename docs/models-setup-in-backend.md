# Setting Up Models in Backend

## Step-1 

Open-up your todo project in `VSCODE`. Then create a directory called `Models` under the `backend` directory. Now right click on  `Models` then from `New C#` select `Class`. Enter the name of the model `Todo`. This will create a class `Todo.cs` under a **namespace** like `backend.Models`. Create another in the same approach named `User.cs`.

## Step-2 

On the `User.cs` class import this `System.ComponentModel.DataAnnotations` at the begining

```c#
using System.ComponentModel.DataAnnotations;
```
Then in the class body add

```c#

    public Guid Id { get; set; } = Guid.NewGuid();

    public string Username { get; set; }
    
    public string Password { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    // Navigation property
    public List<Todo> Todos { get; set; } = new List<Todo>();
```

Here, The `User` class is a simple C# model that represents a `user` in the application.

- Id (Primary Key)
    - A unique identifier (Guid) for each user.
    - Automatically assigned a new GUID when a user instance is created.
- Username
    - Stores the username of the user.
    - Should be unique, though there's no explicit constraint in this class.
- Password
    - Stores the password (should ideally be hashed, not stored in plain text).
- CreatedAt
    - Stores the timestamp when the user was created.
    - Defaults to the current date and time.
- Navigation Property (Todos)
    - Represents a one-to-many relationship between User and Todo.
    - Each user can have multiple todo items (a Todo class must exists).

## Step-3 

On the `Todo.cs` class import this `System.ComponentModel.DataAnnotations` at the begining

```c#
using System.ComponentModel.DataAnnotations;
```
Then in the class body add

```c#

    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; }
    public bool IsCompleted { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    // Foreign key
    public Guid? UserId { get; set; }

    // Navigation property

    public User? User { get; set; }
```
Here, This class represents a `Todo` item in the application and has a relationship with the `User` class.

- Id (Primary Key)
    - Each Todo item gets a unique identifier (Guid).
    - Automatically assigned a new Guid when an instance is created.
- Title
    - Stores the title of the to-do item.
- IsCompleted
    - A boolean flag indicating whether the task is completed (true) or not (false).
- Description
    - Stores additional details about the to-do item.
- CreatedAt
    - Captures the timestamp when the task was created.
    - Defaults to the current date and time when the object is instantiated.
- Foreign Key and Relationship with User
    - UserId (Foreign Key)
        - Foreign Key referencing the User entity.
        - The ? (nullable) means the Todo might not be associated with a User (optional relationship).
    - User (Navigation Property)
        - Navigation Property that establishes a many-to-one relationship between Todo and User.

## Entity Relations

Here, A one-to-many relationship is formed

- One User → Can have multiple Todos.
- One Todo → Belongs to one User.
