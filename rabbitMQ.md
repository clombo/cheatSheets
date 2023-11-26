# Rabbit MQ Cheat Sheet .NET and others

## Introduction

RabbitMQ is a message broker that supports multiple messaging protocols.

## Docker image example

### Create from command line:
- 

### Docker compose example:

```yaml

```

## Protocols

### Types of protocols:

- AMQP
- STOMP
- MQTT

#### AMQP (Advanced Message Queuing Protocol)

This is the most common used protocol used with applications.

## Exchanges

Exchanges has a binding and routing key that sends it to the necessary queues accordingly.
Routing keys are sent by the publisher and the queue has the binding key.

### Types

There are 4 common types exchanges for RabbitMQ

- Direct
- Fanout
- Topic
- Header

### CLI Tool

####  Examples

All the code examples that follow will be in .NET using the `MassTransit` package.

## Configure .NET application to use RabbitMQ with MassTransit

### Install

Install the following packages on your project where you will be configuring your message bus (RabbitMQ):

- `MassTransit.AspNetCore`
- `MassTransit.Extensions.DependencyInjection`
- `MassTransit.RabbitMQ`

### Configure

First we will need to add the necessary configuration values to your `appsettings.json` file. If you have multiple environments each using different instances add it to the correct `appsettings.{env}.json` file.

```json

"RabbitMQ": {
    "HostAddress": "host address like DNS name or IP of RabbitMQ instance",
    "Username": "Username to connect",
    "Password": "Password to connect",
    "ConnectionName": "Friendly name for connection"
}

```

Create a new .cs file (This example it is `Extensions.cs`) in your project. Depending on your project you might need to install some additional packages on the project.

We will be using .NET DI to configure RabbitMQ. we will be using a static `IServiceCollection` method:

```cs

public static class Extenstions
{
    public static IServiceCollection AddServiceBus(
        this IServiceCollection services,IConfiguration configuration
    )
    {
        services.AddMassTransit(x =>
            {   
                x.UsingRabbitMq((ctx, cfg) =>
                    {
                        cfg.Host(
                            configuration["RabbitMQ:HostAddress"], 
                            configuration["RabbitMQ:ConnectionName"], 
                            c =>
                            {
                            c.Username(configuration["RabbitMQ:Username"]);
                            c.Password(configuration["RabbitMQ:Password"]);
                            }
                        );                    
                    }
                );
            }
        );

        return services;
    }
}

```

#### Adding a consumer
#### Adding a Publisher
#### Adding a Queue



### Direct
#### Example Usage
### Fanout
#### Example Usage
### Topic
#### Example Usage
### Header
#### Example Usage

## Useful patterns

### Outbox Pattern
#### Example Usage
### Circuit Breaker Pattern
#### Example Usage
### Saga Pattern
#### Example Usage

## Deploy on production K8S 

For more inforamtion see [The K8S RabbitMQ docs](https://www.rabbitmq.com/kubernetes/operator/operator-overview.html)