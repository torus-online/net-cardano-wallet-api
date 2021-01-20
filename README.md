# .NET Cardano Wallet API

### .NET client for the Cardano Wallet API

The Cardano node exposes a [REST like API](https://github.com/input-output-hk/cardano-wallet) 
allowing clients to perform a variety of tasks including 
 - creating or restoring a wallet
 - submitting a transaction with or without [metadata](https://github.com/input-output-hk/cardano-wallet/wiki/TxMetadata) 
 - checking on the status of the node
 - listing transactions
 - listing wallets

The full list of capabilities can be found [here](https://input-output-hk.github.io/cardano-wallet/api/edge/). 
     
This artefact wraps calls to that API to make them easily accessible to .NET developers.

It also provides an executable to provide rudimentary command line access. 


- [Building](#building)
- [Usage](#usage)
- [Command line executable jar](#cmdline)
- [Examples](#examples)
- [Issues](#issues)
        

### <a name="building"></a> Building 

This is a .NET project, so the usual `dotnet` commands apply.

Clone the [repository](https://github.com/input-output-hk/psg-cardano-wallet-api) 

To build and publish the project to your local repository use 

`dotnet publish`

To build the command line executable jar use

`dotnet publish`  

To build the command line executable jar skipping tests, use

`sbt 'set test in assembly := {}' assembly`

This will create a jar in the `target/scala-2.13` folder. 

#### Implementation

The jar is part of an Akka streaming ecosystem and unsurprisingly uses [Akka Http](https://doc.akka.io/docs/akka-http/current/introduction.html) to make the http requests, 
it also uses [circe](https://circe.github.io/circe/) to marshal and unmarshal the json.

### <a name="usage"></a>Usage 

The jar is published in Maven Central, the command line executable jar can be downloaded from the releases section 
of the [github repository](https://github.com/input-output-hk/psg-cardano-wallet-api)


Before you can use this API you need a cardano wallet backend to contact, you can set one up following the instructions 
[here](https://github.com/input-output-hk/cardano-wallet). The docker setup is recommended.
 
Alternatively, for 'tire kicking' purposes you may try  `http://cardano-wallet-testnet.iog.solutions:8090/v2/`    

First, add the library to your dependencies, 
```
<dependency>
  <groupId>solutions.iog</groupId>
  <artifactId>psg-cardano-wallet-api_2.13</artifactId>
  <version>x.x.x</version>
</dependency>
```

Then, using `getWallet` as an example...

```
import iog.psg.cardano.jpi.*;

ActorSystem as = ActorSystem.create();
ExecutorService es = Executors.newFixedThreadPool(10);
CardanoApiBuilder builder =
        CardanoApiBuilder.create("http://localhost:8090/v2/")
                .withActorSystem(as) // <- ActorSystem optional
                .withExecutorService(es); // <- ExecutorService optional

CardanoApi api = builder.build();

String walletId = "<PUT WALLET ID HERE>";
CardanoApiCodec.Wallet  wallet =
            api.getWallet(walletId).toCompletableFuture().get();

```

#### <a name="cmdline"></a>Command Line 

To see the usage instructions, use    

`java -jar psg-cardano-wallet-api-assembly-x.x.x-SNAPSHOT.jar`

For example, to see the [network information](https://input-output-hk.github.io/cardano-wallet/api/edge/#tag/Network) use 

`java -jar psg-cardano-wallet-api-assembly-x.x.x-SNAPSHOT.jar -baseUrl http://localhost:8090/v2/ -netInfo`
  
#### <a name="examples"></a> Examples

The best place to find working examples is in the [test](https://github.com/input-output-hk/psg-cardano-wallet-api/tree/develop/src/test) folder 

#### <a name="issues"></a> Issues

This release does *not* cover the entire cardano-wallet API, it focuses on getting the shelley core functionality into the hands of developers, if you need another call covered please log 
an [issue (or make a PR!)](https://github.com/input-output-hk/psg-cardano-wallet-api/issues)    
