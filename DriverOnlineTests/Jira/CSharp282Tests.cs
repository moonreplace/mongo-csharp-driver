﻿/* Copyright 2010-2011 10gen Inc.
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
* http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace MongoDB.DriverOnlineTests.Jira
{
    [TestFixture]
    public class CSharp282Tests
    {
        private MongoServer _server;
        private MongoDatabase _database;
        private MongoCollection<BsonDocument> _collection;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            _server = MongoServer.Create("mongodb://localhost/?safe=true");
            _database = _server["onlinetests"];
            _collection = _database["testcollection"];
            _collection.Drop();
        }

        [Test]
        public void TestEmptyUpdateBuilder()
        {
            var document = new BsonDocument("x", 1);
            _collection.Insert(document);

            var query = Query.EQ("_id", document["_id"]);
            var update = new UpdateBuilder();
            Assert.Throws<ArgumentException>(() => _collection.Update(query, update));
        }
    }
}
