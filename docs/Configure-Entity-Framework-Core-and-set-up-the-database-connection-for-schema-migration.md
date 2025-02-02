# Configure Entity Framework Core and set up the database connection for schema migration

## Step-1 

Open-up `VSCODE` and goto `backend` directory of the project. Create a directory called `Data`. In this directory I will create a file in which I will define the configurations for the `Models/Todo.cs` and `Models/User.cs`.

## Step-2 

Download the necessary packages and tools
```bash

dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet tool install --global dotnet-ef
```
## Step-3

Creating the file  `ApplicationDbContext.cs` inside `/backend/Data/` directory. And paste this contents

```c#
using Microsoft.EntityFrameworkCore;
using backend.Models; // because of namespacing we have to include this

namespace backend.Data
{
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Todo> Todos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasMany(u => u.Todos)
            .WithOne(t => t.User)
            .HasForeignKey(t => t.UserId);

        modelBuilder.Entity<User>()
            .ToTable("users"); // Explicitly set table name

        modelBuilder.Entity<Todo>()
            .ToTable("todos"); // Explicitly set table name
        base.OnModelCreating(modelBuilder);
    }
}
}
```

## Step-4 

Now startup the PostgreSQL database. I'm using docker for easily handle this database setup procedure.

### Step-4.1

Pull the `postgresql` docker image 
```bash
docker pull postgres:latest
```
### Step-4.2

Run a container outof the pulled `postgres` image

```bash
docker run --name driftodo-psql \
  -e POSTGRES_USER=username \
  -e POSTGRES_PASSWORD=hash1234 \
  -e POSTGRES_DB=driftodo \
  -p 5432:5432 \
  -d postgres:latest
```
Verify creation of the database `driftodo`

- `exec` into container
    ```bash
    docker exec -it driftodo-psql bash
    ```
- After entering into the container, enter to `psql` database `driftodo`
    ```bash
    psql -U username -d driftodo -W
    ```
    You will prompt up to enter the password, if this comes then simply give the password and you're good to go. Otherwise debug the issue. You can see the descriptions of those flags by `psql --help`.

## Step-5

In `appsettings.Development.json` add the connection string configurations for database connection like below

```json
{
    // others
    ...,
    "ConnectionStrings": {
        "DefaultConnection": "Host=localhost;Database=driftodo;Username=username;Password=hash1234"
  },
  ... // others
}
```
This is for development environement only. For this purpose the credentials are uploaded otherwise this things might kept secretly.

## Step-6

On the entry point file of this project `Program.cs` add the ApplicationDbContext to initiate the connection with database and seamlessly `migrate` the Data-Models to the database using Entity Framework Core.

Add this two in the begining of the `Program.cs` file

```c#
using Microsoft.EntityFrameworkCore;
using backend.Data;
```

Now add the DbContext as a serverice before calling `builder.build()`.

```c#
// Add DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
```

## Step-7

Now generate migration files using command below

```bash
dotnet ef migrations add init
```
This will generate migration files under the `Migrations` directory inside `backend` directory

And migration updates to reflect on the database use command below:

```bash
dotnet ef database update
```

To verify just `exec` into the **running container** of the database then log-into your **psql** database and list the schemas using `\d` in `psql shell`. 

