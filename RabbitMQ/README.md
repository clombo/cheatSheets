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
- Exchanges
- Virtual Hosts
- Streams

### Connection

a Connection is as the name implies, a TCP connection to RabbitMQ.

### Channel

Some applications need multiple logical connections to the broker. However, it is undesirable to keep many TCP connections open at the same time because doing so consumes system resources and makes it more difficult to configure firewalls. AMQP 0-9-1 connections are multiplexed with `channels` that can be thought of as "lightweight connections that share a single TCP connection".

For more see <a href="https://www.rabbitmq.com/channels.html" target="_blank">the RabbitMQ Docs.</a>

### Exchanges

Exchanges are routing agents, defined by the virtual host within RabbitMQ. An exchange is resposible for routing the messages to different queues with the help of attributes, bindings, and routing keys.

#### Types

There are 4 common types exchanges for RabbitMQ but are not the only exchanges available. See the <a href="https://github.com/clombo/cheatSheets/tree/main/RabbitMQ/Exchanges" target="_blank">Exchange Section</a> for more.

- <a href="https://github.com/clombo/cheatSheets/tree/main/RabbitMQ/Exchanges/Direct" target="_blank">Direct</a>
- <a href="https://github.com/clombo/cheatSheets/tree/main/RabbitMQ/Exchanges/Fanout" target="_blank">Fanout</a>
- <a href="https://github.com/clombo/cheatSheets/tree/main/RabbitMQ/Exchanges/Topic" target="_blank">Topic</a>
- <a href="https://github.com/clombo/cheatSheets/tree/main/RabbitMQ/Exchanges/Header" target="_blank">Header</a>

Click on any of the above links for more details on each exchange with examples.

More information on Exchanges and components related to Exchanges like Consumers, Producers, Queues can be found 
<a href="https://github.com/clombo/cheatSheets/tree/main/RabbitMQ/Exchanges" target="_blank">HERE.</a>

> **_NOTE:_** Another exchange that is not covered as an example is the `Consistent Hashing Exchange`. This exchange is available through a plugin you install and is not available as a default.It is used to distribute messages across the queues possibly equily. This works similar to the worker queue but has some advantages where you can give one queue more messages than others instead of using a round robin approach.

>It still uses a routing/binding key but is a numerical value. This value determines how many messages are likely to be sent to a queue via a hash space under the hood.
This type is very complex and is hard adding additional queues with bindings.

### Virtual Hosts (V-Hosts)

- Virtual hosts provide logical grouping and seperation of resources such as connections,exchanges,queues,bindings,user permissions, policies etc.
- Virtual hosts can only act and operate on resources that are assigned to it.
- Virtual hosts can only be created via the HTTP API or using the rabbitmqctl

Examples of creating hosts:

- rabbitmqctl add_vhost qa1
curl -u userename:pa$sw0rD -X PUT `Host Name`

> **_NOTE:_**  `Host Name` Will be the your rabbitmq hosted URL for example `http://rabbitmq.local:15672/api/vhosts/vh1`

Virtual hosts have the following metadata that can be associated with them:
- description (--description argument)
- a Set of tags(--tags argument)
- Default queue type configured for the virtual host(--default-queue-type argument)

For more on Virtual Hosts see the following links:
- <a href="(https://www.rabbitmq.com/vhosts.html)" target=_blank>RabbitMQ Docs</a>


## Patterns

### Outbox Pattern
#### Example Usage
### Circuit Breaker Pattern
#### Example Usage
### Saga Pattern
#### Example Usage

## Streams

## Options

### CLI Tool

## Deploy on production K8S 

For more inforamtion see [The K8S RabbitMQ docs](https://www.rabbitmq.com/kubernetes/operator/operator-overview.html)