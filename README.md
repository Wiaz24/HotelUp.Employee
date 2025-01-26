# HotelUp - Employee service
![dockerhub_badge](https://github.com/Wiaz24/HotelUp.Employee/actions/workflows/dockerhub.yml/badge.svg)

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
