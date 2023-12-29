# Exchanges

## Introduction

RabbitMQ’s exchange is used as a routing mediator/agent defined by a virtual host within RabbitMQ, to receive messages from producers and push them to message queues according to rules provided by the RabbitMQ exchange type. Each RabbitMQ exchange type uses a separate set of parameters and bindings to route the message. Clients get the option to create their own exchanges or use the default exchanges. It uses the help of header, binding, and routing attribute / keys to route the messages to the necessary queues.

## Core concepts

There is a few concepts to understand before getting started with exchanges.

### Producers/Publishers

a Publisher or Producer is a application instance that produces a message and pushes (publish) it to the necessary exchange.

> **_NOTE:_** a Producer or Publisher can also consume messages at the same time and thus can be a consumer as well.

For more see <a href="https://www.rabbitmq.com/publishers.html" target="_blank">the RabbitMQ Docs.</a>

### Consumers

a Consumer is a subscription for a message delivery on a queue that has to be registered before deliviries begin. This is normally in a form of a application instance and consumption can be cancelled by the consuming application.

For more see <a href="https://www.rabbitmq.com/consumers.html" target="_blank">the RabbitMQ Docs.</a>

### Queue

A queue in RabbitMQ is an ordered collection of messages. Messages are enqueued and dequeued (delivered to consumers) in a FIFO ("first in, first out") manner.

To define a queue in generic terms, it is a sequential data structure with two primary operations: 
- an item can be enqueued (added) at the tail. 
- an item can be dequeued (consumed) from the head.

Queues play a major role in the messaging technology space. Many messaging protocols and tools assume that publishers and consumers communicate using a queue-like storage mechanism.

For more see <a href="https://www.rabbitmq.com/queues.html" target="_blank">the RabbitMQ Docs.</a>


### Binding

a Binding can be seen as the "connection" between a queue and an exchange so that a message can be routed correctly for consumption.

### Routing Key

The routing key plays hand in hand with the binding. It is a message attribute taken into account by the exchange when deciding how to route a message to a queue.

## Basic RabbitMQ Message Cycle

Here’s what a basic RabbitMQ message cycle looks like:

1. An exchange message is sent out by the producer.
2. After the communication has been received, the exchange is responsible for sending it. It uses information from the RabbitMQ exchange type to direct the message to the relevant queues and exchanges.
3. The queue receives the message and stores it until the consumer receives it.
4. Finally, the consumer handles the message.

## Types of exchanges

There are four basic RabbitMQ exchange types in RabbitMQ, each of which uses different parameters and bindings to route messages in various ways, These are:

- [Direct Exchange](https://github.com/clombo/cheatSheets/tree/main/RabbitMQ/Exchanges/Direct)
- [Topic Exchange](https://github.com/clombo/cheatSheets/tree/main/RabbitMQ/Exchanges/Topic)
- [Fanout Exchange](https://github.com/clombo/cheatSheets/tree/main/RabbitMQ/Exchanges/Fanout)
- [Headers Exchange](https://github.com/clombo/cheatSheets/tree/main/RabbitMQ/Exchanges/Header)

Additionally, there is 1 more types available to RabbitMQ:
- The `Consistent Hashing Exchange` is available through a plugin you install and it not available as a default.

There is also some extensions to the Exchanges available on RabbitMQ namely:

- [Default/Alternate Exchange](https://github.com/clombo/cheatSheets/tree/main/RabbitMQ/Exchanges/Alternate)
- [Dead Letter Exchange](https://github.com/clombo/cheatSheets/tree/main/RabbitMQ/Exchanges/Deadletter)

These exchanges are not types on their own and instead uses one of the 4 default types of Exchanges available.


For more information click on the exchange you are interested in.

## References
- [RabbitMQ Exchange Types: How messages are sent and received](https://hevodata.com/learn/rabbitmq-exchange-type/#:~:text=RabbitMQ's%20exchange%20is%20used%20as,bindings%20to%20route%20the%20message.)
- [Part 4: RabbitMQ Exchanges, routing keys and bindings](https://www.cloudamqp.com/blog/part4-rabbitmq-for-beginners-exchanges-routing-keys-bindings.html)
- [RabbitMQ Consumer Documentation](https://www.rabbitmq.com/consumers.html)
- [RabbitMQ Publisher Documentation](https://www.rabbitmq.com/publishers.html)