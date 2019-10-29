using NLog;
using BlogsConsole.Models;
using System;
using System.Linq;

namespace BlogsConsole
{
    class MainClass
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public static void Main(string[] args)


        {

            logger.Info("Program started");
            var choice = "";
            do
            {
                Console.WriteLine("Enter your selection:");
                Console.WriteLine("1. Display all blogs");
                Console.WriteLine("2. Add blog");
                Console.WriteLine("3. Create Post");
                Console.WriteLine("4. Display Posts");
                Console.WriteLine("Enter q to quit");
                var choice1 = Console.ReadLine();

                if (choice1 == "1")
                {

                    var db = new BloggingContext();

                    // Display all Blogs from the database
                    var query = db.Blogs.OrderBy(b => b.Name);

                    Console.WriteLine("All blogs in the database:");
                    foreach (var item in query)
                    {
                        Console.WriteLine(item.Name);
                    }

                }

                else if (choice1 == "2")
                {

                    // Create and save a new Blog
                    Console.Write("Enter a name for a new Blog: ");
                    var name = Console.ReadLine();

                    if (name == "")
                    {

                        Console.WriteLine("Blog name cannot be null");

                    }

                    else
                    {

                        var blog = new Blog { Name = name };

                        var db = new BloggingContext();
                        db.AddBlog(blog);
                        logger.Info("Blog added - {name}", name);
                    }

                }

                else if (choice1 == "3")

                {

                    // Display all blogs in the database
                    var db = new BloggingContext();

                    // Display all Blogs from the database
                    var query = db.Blogs.OrderBy(b => b.Name);

                    Console.WriteLine("Select a blog you would like to post to:");
                    foreach (var item in query)
                    {
                        Console.WriteLine(item.BlogId + " " + item.Name);
                    }

                    // get the user input for choosing a blog id
                    var blogChoice = Console.ReadLine();
                    var blogChoiceParsed = Int32.Parse(blogChoice);

                    bool isValid = db.Blogs.Any(b => b.BlogId == blogChoiceParsed);

                    // check to see if the number the user entered is in the table
                    if (isValid)
                    {

                        // Create Post title
                        Console.WriteLine("Enter the post title");
                        var title = Console.ReadLine();

                        var post = new Post { Title = title, BlogId = blogChoiceParsed };

                        //db.AddPost(post);
                        logger.Info("Title added - {title}", title);

                        // Create Post content
                        Console.WriteLine("Enter the post content");
                        var content = Console.ReadLine();

                        var post1 = new Post { Content = content };

                        db.AddPost(post);
                        logger.Info("Content added - {content}", content);

                    }
                    else
                    {

                        Console.WriteLine("Please enter a number on the list");
                    }


                }

                else if (choice1 == "4")
                {
                    var db = new BloggingContext();

                    // Display all posts from the database
                    var query = db.Posts.OrderBy(b => b.PostId);

                    Console.WriteLine("Select the blogs post to display:");
                    foreach (var item in query)
                    {
                        Console.WriteLine(item.PostId + " " + item.Title);
                    }

                    // get the user input for choosing a blog id
                    var postChoice = Console.ReadLine();
                    var postChoiceParsed = Int32.Parse(postChoice);

                    bool isValid = db.Blogs.Any(b => b.BlogId == postChoiceParsed);

                    // check to see if the number the user entered is in the table
                    if (isValid)
                    {

                        // show the title and content
                        var blog = db.Blogs.FirstOrDefault(b => b.BlogId.Equals(postChoiceParsed));
                        var post = db.Posts.FirstOrDefault(b => b.PostId == postChoiceParsed);
                        Console.WriteLine("Blog: " + blog.Name);
                        Console.WriteLine("Title: " + post.Title);
                        Console.WriteLine("Content: " + post.Content);

                    }

                    else
                    {

                        Console.WriteLine("Please enter a number on the list");
                    }

                }







            }

            while (choice == "1" || choice == "2" || choice == "3" || choice == "4");

                logger.Info("Program ended");
            }

        }
    }
