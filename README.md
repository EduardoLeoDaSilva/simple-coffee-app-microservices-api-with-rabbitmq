# Coffee Ordering System with RabbitMQ

This is a simple project designed to demonstrate the functionality of RabbitMQ in inter-service communication within a coffee ordering system.

## Project Description

The goal of this project is to create a system where users can place coffee orders from different stores. Orders are processed through microservices that communicate using RabbitMQ, a messaging solution.

## Technologies Used

- **Programming Language:** C#
- **Frameworks (if applicable):** RabbitMQ, EFCore, .NET

## Main Components

### 1. Orders Service

This microservice allows users to place coffee orders. Orders include information such as coffee type and description.

### 2. Stores Service

This microservice maintains information about different coffee stores available, including name, address, and menu.

### 3. Payment Service

This microservice handles the processing of payments for coffee orders. It simulates a fake call to a payment gateway.

### 4. Message Queue (RabbitMQ)

RabbitMQ is used as the messaging solution to enable asynchronous communication between microservices. Whenever an order is placed, a message is placed in the payment queue, and after processing the payment, two more message are sent to order service, so it can update de order status, 
and the other message to stores service, so it can update the stock of product.

## How to Run the Project

## Running and Debugging via an Editor:

### MySQL and RabbitMQ Already Configured:

Before running the projects, make sure you have a MySQL database server available and a RabbitMQ server running.

You will need to define the connection strings for RabbitMQ and MySQL in the appsettings of the three projects. Once done, you can run and perform the operations.

In case you don't have RabbitMQ and MySQL configured, here are steps to set them up for running the application:

**Creating Containers to Run the Applications via Docker:**

**MySQL:**
```
docker run --name mysql -e MYSQL_ROOT_PASSWORD=123 -p 3306:3306 --network coffee-project-net -d mysql
```

**RabbitMQ:**
```
docker run -d --hostname my-rabbit --name some-rabbit -p 5672:5672 -p 15672:15672 -e RABBITMQ_DEFAULT_USER=username -e RABBITMQ_DEFAULT_PASS=password --network coffee-project-net rabbitmq:3-management
```

After this, configure the connection strings and connection information in the appsettings file, then run the applications.

## Running with Docker-compose.

If you just want to see all the microservices running and their communication, you can simply run the following command inside the folder 'CoffeeOnDemandSolution', to start the docker-compose process, after that, everything is set up:
```
docker-compose up
```

## Notes

This is a simple project created to demonstrate the functionality of RabbitMQ in inter-service communication. For a production environment, additional features such as authentication, more robust error handling, and scalability would need to be added.
