# asbEvent
ASB Event Capitaland

## Getting Started
1. Clone the Repository
2. Run the following to start the application
   
```
dotnet run
```

3. Open tool to request API endpoints (Postman, Insomnia, etc)

## Dependencies
1. Mailkit/Mimekit for email generation
2. QRCoder for QR Code generation
3. ZXing for QR Code decoding
4. EFCore (MSSQLServer)

## Instructions to reproduce for future service migrations
1. Create a new repository
2. Run the following command at the root of the project directory to initialise a new .NET 8 MVC application

```
dotnet new mvc
```

3. Add required dependencies (e.g. EFCore)

```
dotnet add <dependency>
```


