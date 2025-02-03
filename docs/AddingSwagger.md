# Swagger and It's Configurations

## What and Why?

`Swagger` is a **framework** for `API documentation` and `testing` that simplifies the process of **creating**, **visualizing**, and **interacting** with `RESTful APIs`. It automatically generates interactive `API` docs, allows users to test endpoints directly, and helps generate client SDKs in various programming languages. Swagger is now part of the `OpenAPI` Specification **(OAS)**, which promotes standardization of API design, making APIs easier to understand and integrate with. It also supports automation for generating and updating docs as APIs evolve.

## How do we configure it in ASP.NET Core WebAPI

- Install package `Swashbuckle.AspNetCore`
    ```bash
    dotnet add package Swashbuckle.AspNetCore
    ```
- Add below line before `builder.build()` in the `Program.cs` file
    ```c#
    builder.Services.AddSwaggerGen();
    ```
- Now enable `swagger` only for `Development Environment`, add this lines to your `Program.cs`, If already have block like this just extend it.
    ```c#
    if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
    ```
Now `run` your project and hit this URL `http://localhost:5181/swagger/index.html`. You will be able to interact with Swagger Documentations.