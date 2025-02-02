# Data Models for ToDo Project

## Overview

In this project, I have two data models that represent the core entities of our ToDo application. These models are designed to handle the data structure and relationships between different entities.

## Models

### User

The `User` model represents a user in the system.

- **Properties:**
    - `Id` (UUID): The unique identifier for the user.
    - `Username` (string): The username of the user.
    - `Password` (string): The hashed password of the user.
    - `CreatedAt` (DateTime): The date and time when the user was created.

### Todo

The `Todo` model represents a todo in the ToDo list.

- **Properties:**
    - `Id` (UUID): The unique identifier for the task.
    - `Title` (string): The title of the task.
    - `Description` (string): A detailed description of the task.
    - `IsCompleted` (bool): A flag indicating whether the task is completed.
    - `CreatedAt` (DateTime): The date and time when the todo was created.
    - `UserId` (UUID): The identifier of the user who created the task.

## Relationships

- A `User` can have many `Todos`, but each `Todo` is linked to a single `User`.

These data models form the foundation of the ToDo application, allows to manage **users** and **todos** efficiently.