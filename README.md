# Dell Test - Customers

A web service providing a READ/ADD/UPDATE functionalities.

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes.

### Prerequisites

```
Visual Studio 2017 with the desktp and web developement features installed
```

### Installing

```
1. Clone repository
2. Open the 'DellTest.Customers.sln' solution file in Visual Studio
3. Build the solution
4. Run the 'DellTest.Customers.Service' project
5. (Optional) Run the 'DellTest.Customers.Web' project (while 'DellTest.Customers.Service' is also running) if you want to explore api's actions and data. See [this](https://msdn.microsoft.com/en-us/library/ms165413.aspx) for running multiple projects at the same time.
```

### Request Example
Type: POST

Url: /delltestapi/customer/add

Body (json):

```
{
    name: 'Customer Name',
    email: 'customer@email.net'
}
```

### Response Example
```
{
    "Id":1,
    "Data": {
        "Id":1,
        "State":1,
        "Name":"Customer Name",
        "Email":"customer@email.net",
        "DateCreated":"2018-06-10T08:10:59.786415Z",
        "DateUpdated":"2018-06-10T08:10:59.786415Z",
        "DateDeleted":null,
        "IsActive":true
    }
}
```

## Running the tests

Automated tests can be found in the 'DellTest.Customers.Service.Tests' project. To run them, use the 'Test Explorer' window, available in Visual Studio.
