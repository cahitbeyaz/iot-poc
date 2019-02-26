# bvk-lock

bvk-lock is small PoC IoT project.

## Requirements:

- There are IoT lock devices across different locations. These lock devices are connected to a remote server and need to send events that occurs instantly. Events can be 'device locked', 'device unlocked' etc...

- There must be a monitoring application which shows these events.

- On this application it should be possible to activate or disable a device.

- Also if there is device unlocked event and if device does not send device locked event after a certain amount of time system should automatically set lock device to disabled mode.

## Solution

Build a console application to simulate IoT devices in .net core.

Build a scalable TCP Server to handle IoT device messages using an appropriate broker in .net core.

Build a web applications to manage and monitor IoT devices.

## System Architecture And Some Design Considerations

### 

> Simulated IOT device application will send binary messages which contains event to TCP server. TCP server should work asynchronously to handle multiple connections at the same time. So that any new connections should not affect other connections. Also system should favour asynchronous mechanisms to handle lots simultaneous connections.

- Solution: Async TCP Server

> To reduce network traffic and GSM costs underlying message protocol between IoT devices and TCP server must be optimized.

- Solution: google protocol buffer defines and implements offers back compatible optimized message serialization. So we can use protobuf

> To handle lots of devices, system should que events to process. Also queuing will provide non-blocked device messaging which means IOT devices will get response after message is queued. Process of queued message will be done later. Also there must be a retry mechanism in case of unsuccessful queued message handle.

- Solution: RabbitMQ has been used message broker. By default RabbitMQ does not have a delayed message delivery. delayed message exchange extension is used to handle automatically set device to disabled.

> IoT Device management and monitoring applications:

- A web application in angular and .net core restful in the backend.

## solution projects

- LockCommons:

commons and utilities for other projects.

- LockSimulator:

simulates a smart lock device

- LockEventGateway:

Listens and receives binary event messages from LockSimulator then sends these messages to event_que using rabbit mq

- LockEventProcesser

consumes events generated via LockEventGateway also handles event based lock device activity check

- LockEventUI

Backoffice and api for management and reporting

todo: application description and application architecture

## Dependencies:

- net core 2.1

- RabbitMQ: 3.7.9+

Rabbit installation problem fix for windows users

https://stackoverflow.com/questions/38343656/epmd-error-for-host-myhost-address-cannot-connect-to-host-port-on-windows-10/38347187

https://www.youtube.com/watch?v=v5e_PasMdXc

also install rabbitmq-delayed-message-exchange

https://github.com/rabbitmq/rabbitmq-delayed-message-exchange

- mongo db 2.7+

## how to run

- make sure mongodb runs

- build project

- run init bat (make sure **rabbitmq-server** runs before other applications)
