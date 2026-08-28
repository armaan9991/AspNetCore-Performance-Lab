using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BenchmarkDotNet.Running;
using MyBenchmarks.Data;

using MyBenchmarks;
using Microsoft.EntityFrameworkCore;

//var connectionString = "Server=(localdb)\\ProjectModels;Database=ApiBenchmarkDb;Integrated Security=True;TrustServerCertificate=True;"; // point to DB , name of DB. , using window user to sigin instead of sql server user. trust server certificate true means we trust the certificate of the server.

//var options = new DbContextOptionsBuilder<BenchmarkDbContext>()              // create options for dbcontext to connect to database.
//    .UseSqlServer(connectionString)                                          // use sql server as database provider. communicate with sql server using connection string.
//    .Options;                                                               // build the options object with the specified configuration

//await using (var context = new BenchmarkDbContext(options))                // create instance of dbcontext with the options we created above. await using means it will dispose the context after use.
//{                                                                           
//    var seeder = new ProductSeeder(context);                               // create instance of productseeder with the context we created above.
//    await seeder.SeedAsync(10000);                                         // call the seedasync method to seed 10000 products into the database.
//}
//Console.WriteLine("Database seeding completed.");
//return;

//BenchmarkRunner.Run<ApiBenchmark>();
global::BenchmarkDotNet.Running.BenchmarkRunner.Run<ApiBenchmark>();