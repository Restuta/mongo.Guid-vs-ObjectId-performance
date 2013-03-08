mongo.Guid-vs-ObjectId-performance
==================================

Comparing index performance on using Guids and ObjectIds with Mongo

Each test was performed at least 3 times.

There are several branches, each for each test-set (guid, objectId, etc).


|                                                         | ObjectId      | Guid            | Sequential Guid|
| --------------------------------------------------------|:-------------:|:----------------:|:---:|
| 1M inserts batched                                      | 13401ms       |   37138ms       |39291ms|
| 1M inserts                                              | 133255ms      |   160199ms      |159393ms|
| 10m inserts batched, with 10M documents already present | 152426ms      |    470489ms     |482354ms|
| 10m inserts, with 10M documents already present         | 1337894ms     |    4921991ms    ||
| Find document by id (24M docs)                          | 25ms          |     25ms        |22ms|
| Skip 10M docs, take 1 (24M docs)                        | 1401ms        |     1454ms      |1449ms|
| Count docs where doc.id > randomId  (24M docs)          | 48363ms       |not applicable (since operator ">") doesn't work for Guid||
|Index size for 24M docs                                  | 819MB         |       1225MB    ||




Raw results to be analysed later
================================
ObjectId
```
docs inserted during past 60 seconds:0
docs inserted during past 60 seconds:3813220
docs inserted during past 60 seconds:3813220
docs inserted during past 60 seconds:4575864
docs inserted during past 60 seconds:3813220
docs inserted during past 60 seconds:4575864
docs inserted during past 60 seconds:3813220
docs inserted during past 60 seconds:3050576
docs inserted during past 60 seconds:4575864
docs inserted during past 60 seconds:3813220
docs inserted during past 60 seconds:4575864
docs inserted during past 60 seconds:3813220
docs inserted during past 60 seconds:3813220
docs inserted during past 60 seconds:3813220
docs inserted during past 60 seconds:3813220
docs inserted during past 60 seconds:3050576
docs inserted during past 60 seconds:3813220
docs inserted during past 60 seconds:3813220
docs inserted during past 60 seconds:4575864
docs inserted during past 60 seconds:3050576
docs inserted during past 60 seconds:3050576
docs inserted during past 60 seconds:3813220
docs inserted during past 60 seconds:3813220
docs inserted during past 60 seconds:3813220
docs inserted during past 60 seconds:1525288
docs inserted during past 60 seconds:3050576
docs inserted during past 60 seconds:3813220
docs inserted during past 60 seconds:3050576
100000000 BATCH documents insertion with time: 1628639ms
0.01628639ms per document
docs inserted during past 60 seconds:93636
5139193d932bdc5e346ff3a7
Skip 10 000 000 docs and take one time: 11075ms
Reading one document by id time: 421ms //not so true, other test proved that avg is ~42ms
```
