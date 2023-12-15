# RabbitMQ Cheat Sheet .NET and Others

## Introduction

RabbitMQ is a message broker that supports multiple messaging protocols. Focus will be put on the AMQP protocol through out this cheatsheet.

## RabbitMQ Docker image install

### Create from command line:
- `docker run -d --hostname my-rabbit --name some-rabbit -p 15672:15672 rabbitmq:management`

This will also install a managment plugin as well. You can access it on `http://host-ip:15672` or `http://localhost:15672`. Default username and password `guest`/`guest`

For more information see the [Docker Oficial Image Page](https://hub.docker.com/_/rabbitmq)

### Docker compose example:

```yaml
version: '3'

services:
  rabbitmq:
    image: rabbitmq:management
    container_name: rabbitmq
    environment:
      - RABBITMQ_DEFAULT_USER=guest
      - RABBITMQ_DEFAULT_PASS=guest
    ports:
      - "5672:5672"
      - "15672:15672"

networks:
  default:
    driver: bridge
```

## Protocols

### Types of protocols:

- AMQP
- STOMP
- MQTT

#### AMQP (Advanced Message Queuing Protocol)

This is the most common used protocol used with applications.

## Components

Some but not all components related to RabbitMQ. Some components are also not unique to rabbitMQ:

- Connection
- Channel
- Virtual Hosts
- Exchanges
- Queues
- Consumers
- Publishers

### Connection
### Channel
### Virtual Hosts
### Exchanges
### Queues

## Patterns

## Options