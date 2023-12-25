## [Back to Main](https://github.com/clombo/cheatSheets/blob/main/RabbitMQ/Main.md)

## Deadletter Exchange

### Introduction

This exchange is used to handle messages that were rejected or expired (no consumer).

basic_ack -> delivery_tag -> multiple

basic_reject -> delivery_tag -> requeue

basic_nack -> delivery_tag -> requeue / multiple