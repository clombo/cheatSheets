## [Back to Exchange Page](https://github.com/clombo/cheatSheets/tree/main/RabbitMQ/Exchanges)

## Topic Exchange

### Introduction

It is similar to the direct exchange in that it will route messages where the routing key matches the binding key from the queue binding. However, with a topic exchange, you can also use wildcards in the binding key. When using a topic exchange the routing key of the message must be a list of words separated by dots, like “metrics.server.cpu”. The reason is that topic exchange allows you to match on parts of the routing key and uses dots as separators.

There are two types of wildcard you can use:
- `#`(hash) will match zero or more words, for example, “metrics.#” will match all routing keys that start with “metrics.”
- `*`(star) will match one word, for example, “metrics.*.cpu” will match all routing keys that start with “metrics.” and end in “.cpu”.

### Example Notes

The below example will be implemented using the MassTransit package for .NET. For more information see the [MassTransit Config page.](https://github.com/clombo/cheatSheets/blob/main/RabbitMQ/MassTransit.md)


The [MassTransit Documentation](https://masstransit.io/documentation) will be referenced through out this document as well.

Only the producers/consumers and messages are covered.

Example code can be found [HERE](https://github.com/clombo/cheatSheets/tree/main/RabbitMQ/Exchanges/Topic/Topic_Exchange)

> **_NOTE:_**  Make sure you have a running instance of RabbitMQ. If you are using different login details update the username and password in AppSettings. See the [MassTransit Config page](https://github.com/clombo/cheatSheets/blob/main/RabbitMQ/MassTransit.md) for more information.

### Characteristics

#### Topic Exchange Diagram:

![RabbitMQ Topic Exchange](https://github.com/clombo/cheatSheets/assets/11086072/6489237a-f746-4256-a8d4-dddb48f577e4)

### Example Implementation using MassTransit

#### Diagram of example implementation.
![Topic Exchange Example](https://github.com/clombo/cheatSheets/assets/11086072/8e66b9ed-926f-4426-a41f-b825befec699)

### References
- [MassTransit Documentation](https://masstransit.io/documentation/concepts)
- [RabbitMQ Exchanges,routing keys and bindings](https://www.cloudamqp.com/blog/part4-rabbitmq-for-beginners-exchanges-routing-keys-bindings.html?gad_source=1&gclid=Cj0KCQiAj_CrBhD-ARIsAIiMxT968bAT2q7IKHeMLJ-ttUXsa1pdhW39c7F7FqpXv_eNtLlzP5NbtSoaAofYEALw_wcB)
- [RabbitMQ Topic Excahnge Explained](https://www.cloudamqp.com/blog/rabbitmq-topic-exchange-explained.html)
- [Example Code](https://github.com/clombo/cheatSheets/tree/main/RabbitMQ/Exchanges/Topic/Topic_Exchange)

## [Back to Exchange Page](https://github.com/clombo/cheatSheets/tree/main/RabbitMQ/Exchanges)
