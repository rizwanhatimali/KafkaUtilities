# ApacheKafka
## Setting up kafka locally

* Please make sure that Docker is already installed in the system, If not, install from https://www.docker.com/products/docker-desktop
* If on Windows O/S, open a Powershell window. If on Mac OS or Linux, open a Terminal window.
* Clone this repo locally and navigate to cloned directory. This directory would contain the docker-compose.yml file
* Execute the following command from this directory

       docker-compose up -d
* Check if the containers are up and running

       docker ps
* To shutdown and remove the setup, execute this command in the same directory

       docker-compose down
    
## Useful Shell Commands

* Logging into the Kafka Container

       docker exec -it kafka-broker /bin/bash
* Navigate to the Kafka Scripts directory

       cd /opt/bitnami/kafka/bin
* Creating new Topics

      
       ./kafka-topics.sh \
            --zookeeper zookeeper:2181 \
            --create \
            --topic kafka.learning.tweets \
            --partitions 1 \
            --replication-factor 1

        ./kafka-topics.sh \
            --zookeeper zookeeper:2181 \
            --create \
            --topic kafka.learning.alerts \
            --partitions 1 \
            --replication-factor 1
* Listing Topics

       ./kafka-topics.sh \
            --zookeeper zookeeper:2181 \
            --list
* Getting details about a Topic

       ./kafka-topics.sh \
            --zookeeper zookeeper:2181 \
            --describe
* Publishing Messages to Topics

       ./kafka-console-producer.sh \
            --bootstrap-server localhost:29092 \
            --topic kafka.learning.tweets
* Consuming Messages from Topics

       ./kafka-console-consumer.sh \
            --bootstrap-server localhost:29092 \
            --topic kafka.learning.tweets \
            --from-beginning
* Deleting Topics

       ./kafka-topics.sh \
            --zookeeper zookeeper:2181 \
            --delete \
            --topic kafka.learning.alerts
            
## Samples

This Repo contains couple of samples as mentioned below for hands-on applications to get started with kafka. Setup Kafka locally using Docker desktop as describe above and then use below application to see kafka in action:

* Kafka Basics

  Java Application to demonstrate basic producer and consumer functionality
       
* Kafka Use Cases

   Java Application to demonstratea use case of student enrolled in perticual course using consumer and producer functionality
       
* Use Cases - DotNet

   .Net Application using Kafka to hold credit card transaction in realtime for user approval if user is in city different from his native city
      
