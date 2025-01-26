# HotelUp - Employee service
![Application tests](https://github.com/Wiaz24/HotelUp.Employee/actions/workflows/tests.yml/badge.svg)
![Github issues](https://img.shields.io/github/issues/Wiaz24/HotelUp.Employee)
[![Docker Image Size](https://badgen.net/docker/size/wiaz/hotelup.employee?icon=docker&label=image%20size)](https://hub.docker.com/r/wiaz/hotelup.employee/)


This service should expose endpoints on port `5002` starting with:
```http
/api/employee/
```

## Health checks
Health status of the service should be available at:
```http
/api/employee/_health
```
and should return 200 OK if the service is running, otherwise 503 Service Unavailable.

## Message broker
This service uses `MassTransit` library to communicate with the message broker. For the purpose of integration with
another MassTransit service, all published events are declared in the `HotelUp.Employee.Services.Events` namespace.

### AMQP Exchanges
This service creates the following exchanges:
- `HotelUp.Employee:EmployeeCreatedEvent` - to notify about new employees. Published messages have
    payload structure that contains the following section:
    ```json
    {
       "message":{
          "id":"24382428-8081-70f2-492c-d048610de311",
          "role":"Cook"
       }
    }
    ```
