This assignment is twofold:
    1. First, you must complete the ChallengeContext class, found in ChallengeContext.cs. The relations between the classes
        are incomplete, especially the many-to-many relationship between Products and Orders. You may accomplish This
        using either the Fluid API or annotations, as long as it works.
    2. Secondly, you must complete the four functions in Queries.cs such that all tests pass. Take this opportunity to practice
        LINQ, as it will save your life in EFCore.

Database Model:
    Customer
        - Id
        - Name
    Product
        - Id
        - Name
        - Price
    OrderItem
        - OrderId
        - ProductId
        - Quantity
    Order
        - Id 
        - CustomerId

To build the challenge, run the following command:

    dotnet build Challenge

To run the challenge with your new code, run:

    dotnet run --project Challenge