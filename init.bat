start cmd /C  ""C:\Program Files\RabbitMQ Server\rabbitmq_server-3.7.9\sbin\rabbitmq-server.bat" "start"

timeout 30

start dotnet LockEventGateway\bin\Debug\netcoreapp2.1\LockEventGateway.dll
timeout 1
start dotnet LockEventProcesser\bin\Debug\netcoreapp2.1\LockEventProcesser.dll

start dotnet LockSimulator\bin\Debug\netcoreapp2.1\LockSimulator.dll

exit