# Multi-Language Console (MLC) Application

## Overview

The Multi-Language Console (MLC) application is designed to streamline the migration of multilingual content from XML files into a SQL database. It offers features like processing, restoring, and managing XML files with extensive logging and helper utilities. This project follows a modular and extensible structure to facilitate the efficient handling of multilingual data and user interactions.

---

## Folder Structure

### **_mlc**
Contains XML files categorized by functionality. Each folder has a backup sub-folder for historical files:
- `_forms`: Includes XML files for forms. Subfolder: `_backupForms`
- `_help`: Contains help-related XML files. Subfolder: `_backupHelp`
- `_messages`: Stores message-related XML files. Subfolder: `_backupMessages`
- `_various`: Holds various additional XML files. Subfolder: `_backupVarious`

### **Data**
Stores application logs and schemas:
- **Logs**
  - `exceptions.txt`: Captures exception details for debugging.
- **Schemas**
  - `XmlToSqlApp1DataSet.xsd`: XSD schema for XML-to-SQL conversion.

### **Helpers**
Utility classes to assist with file, logging, and data management:
- `FileHelper.cs`: Manages file operations.
- `LoggerHelper.cs`: Handles logging operations.
- `ProcessFilesHelper.cs`: Aids in processing XML files.
- `SqlRepositoryHelper.cs`: Manages SQL operations.
- `XmlProcessorHelper.cs`: Handles XML parsing and processing tasks.

### **Models**
Defines the data models used within the application:
- `XmlData.cs`: Represents XML data structure.
- `XmlPath.cs`: Represents XML file paths and metadata.

### **Service**
Core services handling main application logic:
- `ProcessFilesService.cs`: Manages file processing workflows.
- `RestoreFilesService.cs`: Handles restoration of backup XML files.
- `SqlRepositoryService.cs`: Interfaces with the SQL database.
- `XmlProcessorService.cs`: Orchestrates XML processing logic.

### **View**
User-facing components of the application:
- `LogoHandler.cs`: Manages logo display within the UI.
- `UserInterface.cs`: Handles user interactions and input/output management.

### **Program.cs**
Entry point of the application, orchestrating overall workflow and calling services as needed.

### **README.md**
This documentation file, providing an overview of the project.

---

## Features

- Migration of multilingual XML data to a SQL database.
- Comprehensive logging of operations and exceptions.
- Modular helper classes and services for reusability.
- Backup and restoration of XML files for version control.
- User-friendly interface for interaction and control.

---

## Setup and Installation

1. Clone the repository:
   ```bash
   git clone https://git.active-logistics.com/swe/XmlToSql.git


## TODO:
 
 1. Improvememts of the SqlRepositoryService.cs and XmlProcessorService.cs files and their helper classes
