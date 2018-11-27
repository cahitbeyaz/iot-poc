"# bvk-lock" 

todo: application description and application architecture

Requiremenets:
---
.net core 2.1

rabbitmq: 3.7.9+
	Rabbit installation problem fix for windows users
	https://stackoverflow.com/questions/38343656/epmd-error-for-host-myhost-address-cannot-connect-to-host-port-on-windows-10/38347187
	https://www.youtube.com/watch?v=v5e_PasMdXc
	
	also install rabbitmq-delayed-message-exchange
	https://github.com/rabbitmq/rabbitmq-delayed-message-exchange

mongo db 2.7+

to run
--
*make sure mongodb runs
*build project
*run init bat (make sure rabbitmq-server runs before other applications)

projects:
LockCommons:
	commons and utilities for other projects.
	
LockSimulator: 
	simulates a smart lock device
	
LockEventGateway: 
	Listens and receives binary event messages from LockSimulator then sends these messages to event_que using rabbit mq
	
LockEventProcesser:
	consumes events genereated via LockEventGateway
	also handles event based lock device activity check 

LockEventUI:
	backoffice and api for management and reporting



notes:

*Google proto-buf is used for lightweight messaging between lock(iot device) and event gateway
*binary message frame and partial message delivery handled
*logging across all application did not implemented for simplicity and time constraints
*error handling not developed or incomplete due to time
*lock device activity check done using message queuing which provides consisten event handling
*
*
.. todo
