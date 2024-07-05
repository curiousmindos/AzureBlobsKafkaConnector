# Azure Blobs KafkaConnector Function App
If you're a .Net engineer and you're trying to create a custom Kafka connector (like to parse and publish file content to a Kafka topic), 
you might run into some challenges. 
There isn't a Kafka Connect-compatible API for C#.

But you can try using the next workaround to get Azure services involved.
So, the workaround is to use the next bunch of services: You can use the Azure Blob Storage service, Azure Blob Trigger Functions (also consider using Azure Event Hub), and the Confluent Kafka Publisher injected into Azure Functions classes.

So, the idea is that when the end customer uploads files through Azure Blob Storage (using SFTP, NFS, etc.), it triggers events in Azure Functions. These events are then used to use injected services like Kafka Confluent Producers and Custom File Parsers.

In practice, it'll be 'upload files' events -> Kafka Topics Messages.

![image](https://github.com/curiousmindos/AzureBlobsKafkaConnector/assets/7238801/0601779c-e733-4819-90ca-f5c3e43eaadd)

To customize this solution you might thing about 
1. to put some settins into local.settings.json and inject IOption into service classes
2. to modify topic name and Kafka Server settings in KafkaService class
3. to implement custom parser for uploaed file content and map to Kafka Publisher model

![image](https://github.com/curiousmindos/AzureBlobsKafkaConnector/assets/7238801/58e70be0-4d31-4975-b432-ed3940f49858)


