# Exchanges

## Introduction

RabbitMQ’s exchange is used as a routing mediator/agent defined by a virtual host within RabbitMQ, to receive messages from producers and push them to message queues according to rules provided by the RabbitMQ exchange type. Each RabbitMQ exchange type uses a separate set of parameters and bindings to route the message. Clients get the option to create their own exchanges or use the default exchanges. It uses the help of header, binding, and routing attribute / keys to route the messages to the necessary queues.

## Core concepts

There is a few concepts to understand before getting started with exchanges.

### Producers/Publishers
### Task
### Queue
### Binding
### Routing Key

## Basic RabbitMQ Message Cycle

## Types of exchanges

There are four basic RabbitMQ exchange types in RabbitMQ, each of which uses different parameters and bindings to route messages in various ways, These are:

- [Direct Exchange](https://github.com/clombo/cheatSheets/tree/main/RabbitMQ/Exchanges/Direct)
- [Topic Exchange](https://github.com/clombo/cheatSheets/tree/main/RabbitMQ/Exchanges/Topic)
- [Fanout Exchange](https://github.com/clombo/cheatSheets/tree/main/RabbitMQ/Exchanges/Fanout)
- [Headers Exchange](https://github.com/clombo/cheatSheets/tree/main/RabbitMQ/Exchanges/Header)

Additionally, there are 3 more types available to RabbitMQ:

- [Default/Alternate Exchange]()
- [Dead Letter Exchange]()
- [Consistent Hashing Exchange]()

The [`Consistent Hashing Exchange`]() is available through a plugin you install and it not available as a default.

For more information click on the exchange you are interested in.

## References
- [RabbitMQ Exchange Types: How messages are sent and received](https://hevodata.com/learn/rabbitmq-exchange-type/#:~:text=RabbitMQ's%20exchange%20is%20used%20as,bindings%20to%20route%20the%20message.)
- [Part 4: RabbitMQ Exchanges, routing keys and bindings](https://www.cloudamqp.com/blog/part4-rabbitmq-for-beginners-exchanges-routing-keys-bindings.html)