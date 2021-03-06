mongo.Guid-vs-ObjectId-performance
==================================
Comparing index performance on using Guids and ObjectIds with Mongo

I have always been told that non-sequential ids lead to drop in index performance, therefore I decided to test it. 
It's interesting to understand what percent of perfomance drop we are talking about. So I've chosen `ObjectId` as a highly sequential id and `Guid` as non-sequential one. 

**Update:** According to my tests I can't notice any significant perfomance difference in reads, there is some on the write side, could you please advice what I am doing wrong? What test cases should I try to execute and compare?



Each test was performed at least 3 times.

**Tip:** There are several branches, each for each test-set (guid, objectId, etc).


|                                                         | ObjectId      | Guid            | Sequential Guid|% perf drop<sup>1</sup>|
| --------------------------------------------------------|:-------------:|:----------------:|:---:|:--:|
| 1M inserts batched                                      | 13,401ms       |   37,138ms       |39,291ms|177%|
| 1M inserts                                              | 133,255ms      |   160,199ms      |159,393ms|20%|
| 10m inserts batched, with 10M documents already present | 152,426ms      |    470,489ms     |482,354ms|200%|
| 10m inserts, with 10M documents already present         | 1,337,894ms     |    4,921,991ms    ||268%|
| Find document by id (24M docs)                          | 25ms          |     25ms        |22ms|0%|
| Skip 10M docs, take 1 (24M docs)                        | 1,401ms        |     1,454ms      |1,449ms|3.8%|
| Count docs where doc.id > randomId  (24M docs)          | 48,363ms       |not applicable (since operator ">") doesn't work for Guid||
|Index size for 24M docs                                  | 819MB         |       1225MB    |50%|

<sup>1</sup> percent of performance drop is calculated `guid / objectid - 100` since objectId is a baseline


-----------------------------------
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
