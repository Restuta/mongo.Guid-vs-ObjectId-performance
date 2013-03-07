mongo.Guid-vs-ObjectId-performance
==================================

Comparing index performance on using Guids and ObjectIds with Mongo

Each test was performed at least 3 times.


|                                                         | ObjectId      | Guid            |
| --------------------------------------------------------|:-------------:|:----------------:|
| 1M inserts batched                                      | 13401ms       |   37138ms       |
| 1M inserts                                              | 133255ms      |   160199ms      |
| 10m inserts batched, with 10M documents already present | 152426ms      |    470489ms     |
| 10m inserts, with 10M documents already present         | 1337894ms     |    4921991ms     |
| Find document by id                                     | 25ms          |                 |
| Skip 10M docs, take 1                                   | 1401ms        |                 |
| Count docs where doc.id > randomId                      | 48363ms       |not applicable (since operator ">") doesn't work for Guid|
